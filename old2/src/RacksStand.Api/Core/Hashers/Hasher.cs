namespace RacksStand.Api.Core.Hashers;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

public sealed class Hasher : IHasher
{
    private const int _iterCount = 100_000;
    private readonly static RandomNumberGenerator _rng = RandomNumberGenerator.Create();

    public string Hash(string text)
    {
        ArgumentNullException.ThrowIfNull(text);

        return Convert.ToBase64String(HashV3(text, _rng));
    }
    public bool VerifyHashed(string hashedText, string providedText)
    {
        ArgumentNullException.ThrowIfNull(hashedText);
        ArgumentNullException.ThrowIfNull(providedText);

        byte[] decodedHashed = Convert.FromBase64String(hashedText);

        // read the format marker from the hashed
        if (decodedHashed.Length == 0)
        {
            return false;
        }
        switch (decodedHashed[0])
        {

            case 0x01:
                if (VerifyHashedV3(decodedHashed, providedText, out int embeddedIterCount, out KeyDerivationPrf prf))
                {
                    // If this hasher was configured with a higher iteration count, change the entry now.
                    if (embeddedIterCount < _iterCount)
                    {
                        return false;
                    }

                    // V3 now requires SHA512. If the old PRF is SHA1 or SHA256, upgrade to SHA512 and rehash.
                    if (prf == KeyDerivationPrf.HMACSHA1 || prf == KeyDerivationPrf.HMACSHA256)
                    {
                        return false;
                    }

                    return true;
                }
                else
                {
                    return false;
                }

            default:
                return false;
        }
    }
    private static byte[] HashV3(string text, RandomNumberGenerator rng)
    {
        return HashV3(text, rng,
            prf: KeyDerivationPrf.HMACSHA512,
            iterCount: _iterCount,
            saltSize: 128 / 8,
            numBytesRequested: 256 / 8);
    }
    private static byte[] HashV3(string text, RandomNumberGenerator rng, KeyDerivationPrf prf, int iterCount, int saltSize, int numBytesRequested)
    {
        // Produce a version 3 (see comment above) text hash.
        byte[] salt = new byte[saltSize];
        rng.GetBytes(salt);
        byte[] subkey = KeyDerivation.Pbkdf2(text, salt, prf, iterCount, numBytesRequested);

        var outputBytes = new byte[13 + salt.Length + subkey.Length];
        outputBytes[0] = 0x01; // format marker
        WriteNetworkByteOrder(outputBytes, 1, (uint)prf);
        WriteNetworkByteOrder(outputBytes, 5, (uint)iterCount);
        WriteNetworkByteOrder(outputBytes, 9, (uint)saltSize);
        Buffer.BlockCopy(salt, 0, outputBytes, 13, salt.Length);
        Buffer.BlockCopy(subkey, 0, outputBytes, 13 + saltSize, subkey.Length);
        return outputBytes;
    }
    private static bool VerifyHashedV3(byte[] hashedText, string text, out int iterCount, out KeyDerivationPrf prf)
    {
        iterCount = default(int);
        prf = default(KeyDerivationPrf);

        try
        {
            // Read header information
            prf = (KeyDerivationPrf)ReadNetworkByteOrder(hashedText, 1);
            iterCount = (int)ReadNetworkByteOrder(hashedText, 5);
            int saltLength = (int)ReadNetworkByteOrder(hashedText, 9);

            // Read the salt: must be >= 128 bits
            if (saltLength < 128 / 8)
            {
                return false;
            }
            byte[] salt = new byte[saltLength];
            Buffer.BlockCopy(hashedText, 13, salt, 0, salt.Length);

            // Read the subkey (the rest of the payload): must be >= 128 bits
            int subkeyLength = hashedText.Length - 13 - salt.Length;
            if (subkeyLength < 128 / 8)
            {
                return false;
            }
            byte[] expectedSubkey = new byte[subkeyLength];
            Buffer.BlockCopy(hashedText, 13 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            // Hash the incoming text and verify it
            byte[] actualSubkey = KeyDerivation.Pbkdf2(text, salt, prf, iterCount, subkeyLength);
            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
        catch
        {
            // This should never occur except in the case of a malformed payload, where
            // we might go off the end of the array. Regardless, a malformed payload
            // implies verification failed.
            return false;
        }
    }
    private static uint ReadNetworkByteOrder(byte[] buffer, int offset)
    {
        return ((uint)(buffer[offset + 0]) << 24)
            | ((uint)(buffer[offset + 1]) << 16)
            | ((uint)(buffer[offset + 2]) << 8)
            | ((uint)(buffer[offset + 3]));
    }
    private static void WriteNetworkByteOrder(byte[] buffer, int offset, uint value)
    {
        buffer[offset + 0] = (byte)(value >> 24);
        buffer[offset + 1] = (byte)(value >> 16);
        buffer[offset + 2] = (byte)(value >> 8);
        buffer[offset + 3] = (byte)(value >> 0);
    }
}
