using System.Security.Cryptography;

namespace RacksStand.Infrastructure.Services;

public static class PasswordHasher
{
    private const int SaltSize = 16;  // 128 bits
    private const int HashSize = 32;  // 256 bits (SHA-256)
    private const int Iterations = 10000;  // Number of iterations for PBKDF2-like security

    public static string HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentException("Password cannot be empty", nameof(password));
        }
        byte[] salt = GenerateSalt();
        byte[] hashedPassword = HashPasswordWithSalt(password, salt);

        byte[] combinedSaltAndHash = new byte[SaltSize + HashSize];
        Array.Copy(salt, 0, combinedSaltAndHash, 0, SaltSize);
        Array.Copy(hashedPassword, 0, combinedSaltAndHash, SaltSize, HashSize);

        return Convert.ToBase64String(combinedSaltAndHash);
    }

    public static bool VerifyPassword(string hashedPassword, string providedPassword)
    {
        if (string.IsNullOrWhiteSpace(hashedPassword) || string.IsNullOrWhiteSpace(providedPassword))
        {
            throw new ArgumentException("Password and hashed password cannot be empty");
        }
        byte[] combinedSaltAndHash = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[SaltSize];
        Array.Copy(combinedSaltAndHash, 0, salt, 0, SaltSize);
        byte[] storedHash = new byte[HashSize];
        Array.Copy(combinedSaltAndHash, SaltSize, storedHash, 0, HashSize);
        byte[] providedHash = HashPasswordWithSalt(providedPassword, salt);

        return CompareHashes(storedHash, providedHash);
    }

    private static byte[] GenerateSalt()
    {
        byte[] salt = new byte[SaltSize];
        RandomNumberGenerator
        .Create()
        .GetBytes(salt);
        return salt;

    }

    private static byte[] HashPasswordWithSalt(string password, byte[] salt)
    {
        return Rfc2898DeriveBytes
            .Pbkdf2(password, salt, Iterations, HashAlgorithmName.SHA256, HashSize);
    }

    private static bool CompareHashes(byte[] hash1, byte[] hash2)
    {
        if (hash1.Length != hash2.Length)
            return false;

        for (int i = 0; i < hash1.Length; i++)
        {
            if (hash1[i] != hash2[i])
                return false;
        }

        return true;
    }
}
