using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Security
{
    /// <summary>
    /// This class is used to Encryption/Decryption string.
    /// </summary>
    public class EncryptionDecryption
    {
        /// <summary>
        /// This method is used to decript RSA algoritm encrypted string.
        /// </summary>
        /// <param name="cipherText">(string) encrypted string.</param>
        /// <param name="RSAPrivateKey">(string) RSA private key.</param>
        /// <param name="RSAPrivateKeySize">(int) RSA private key size.</param>
        /// <returns>(string) decrypted string.</returns>

        public static string RSADecrypt(string cipherText,string RSAPrivateKey,int RSAPrivateKeySize)
        {
            if (cipherText == null)
                throw new ArgumentException("cipherText");
            if (RSAPrivateKey == null)
                throw new ArgumentException("RSAPrivateKey");
            if (RSAPrivateKeySize <=0)
                throw new ArgumentOutOfRangeException("RSAPrivateKeySize");

            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(RSAPrivateKeySize);
            cryptoServiceProvider.FromXmlString(RSAPrivateKey);
            int length = RSAPrivateKeySize / 8 % 3 != 0 ? RSAPrivateKeySize / 8 / 3 * 4 + 4 : RSAPrivateKeySize / 8 / 3 * 4;
            int num = cipherText.Length / length;
            ArrayList arrayList = new ArrayList();
            for (int index = 0; index < num; ++index)
            {
                byte[] rgb = Convert.FromBase64String(cipherText.Substring(length * index, length));
                Array.Reverse((Array)rgb);
                arrayList.AddRange((ICollection)cryptoServiceProvider.Decrypt(rgb, true));
            }
            return Encoding.UTF8.GetString(arrayList.ToArray(Type.GetType("System.Byte")) as byte[]);
        }
        /// <summary>
        /// This method is used to encrypte a string using RSA algoritm.
        /// </summary>
        /// <param name="cipherText">(string) message data.</param>
        /// <param name="RSAPublicKey">(string) RSA public key.</param>
        /// <param name="RSAPublicKeySize">(int) RSA public key size.</param>
        /// <returns>(string) encrypted string.</returns>
        public static string RSAEncrypt(string cipherText, string RSAPublicKey,int RSAPublicKeySize)
        {
            if (cipherText == null)
                throw new ArgumentException("cipherText");
            if (RSAPublicKey == null)
                throw new ArgumentException("RSAPublicKey");
            if (RSAPublicKeySize <= 0)
                throw new ArgumentOutOfRangeException("RSAPublicKeySize");

            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(RSAPublicKeySize);
            StringBuilder stringBuilder = new StringBuilder();
            cryptoServiceProvider.FromXmlString(RSAPublicKey);
            byte[] bytes = Encoding.UTF8.GetBytes(cipherText);
            int num1 = RSAPublicKeySize - 42;
            int length = bytes.Length;
            int num2 = length / num1;
            for (int index = 0; index <= num2; ++index)
            {
                byte[] rgb = new byte[length - num1 * index > num1 ? num1 : length - num1 * index];
                Buffer.BlockCopy((Array)bytes, num1 * index, (Array)rgb, 0, rgb.Length);
                byte[] inArray = cryptoServiceProvider.Encrypt(rgb, true);
                Array.Reverse((Array)inArray);
                stringBuilder.Append(Convert.ToBase64String(inArray));
            }
            return stringBuilder.ToString();
        }
        /// <summary>
        /// This method is used to encrypte file using Rijndael algorithm.
        /// </summary>
        /// <param name="inputFile">non-encrypted file path.</param>
        /// <param name="outputFile">encrypted file saving path.</param>
        /// <param name="RijndaelManagedKey">RijndaelManagedKey.</param>
        public static void EncryptFile(string inputFile, string outputFile,string RijndaelManagedKey)
        {
            if (inputFile == null)
                throw new ArgumentException("inputFile");
            if (outputFile == null)
                throw new ArgumentException("outputFile");
            if (RijndaelManagedKey == null)
                throw new ArgumentException("RijndaelManagedKey");

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(RijndaelManagedKey, new byte[13]
      {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
      });
            FileStream fileStream1 = new FileStream(outputFile, FileMode.Create);
            RijndaelManaged rijndaelManaged = new RijndaelManaged();
            CryptoStream cryptoStream = new CryptoStream((Stream)fileStream1, rijndaelManaged.CreateEncryptor(passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)), CryptoStreamMode.Write);
            FileStream fileStream2 = new FileStream(inputFile, FileMode.Open);
            int num;
            while ((num = fileStream2.ReadByte()) != -1)
                cryptoStream.WriteByte((byte)num);
            fileStream2.Close();
            cryptoStream.Close();
            fileStream1.Close();
        }
        /// <summary>
        /// This method is used to decrypte file encrypted using Rijndael algorithm.
        /// </summary>
        /// <param name="inputFile">encrypted file path</param>
        /// <param name="outputFile">decrypted file saving path</param>
        /// <param name="RijndaelManagedKey">RijndaelManagedKey.</param>
        public static void DecryptFile(string inputFile, string outputFile,string RijndaelManagedKey)
        {
            if (inputFile == null)
                throw new ArgumentException("inputFile");
            if (outputFile == null)
                throw new ArgumentException("outputFile");
            if (RijndaelManagedKey == null)
                throw new ArgumentException("RijndaelManagedKey");

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(RijndaelManagedKey, new byte[13]
      {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
      });
            CryptoStream cryptoStream = new CryptoStream((Stream)new FileStream(inputFile, FileMode.Open), new RijndaelManaged().CreateDecryptor(passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)), CryptoStreamMode.Read);

            FileStream fileStream = new FileStream(outputFile, FileMode.Create);
            int num;
            while ((num = cryptoStream.ReadByte()) != -1)
                fileStream.WriteByte((byte)num);
            fileStream.Close();
            cryptoStream.Close();
        }
        /// <summary>
        /// This method is used to decrypte file encrypted using Rijndael algorithm.
        /// </summary>
        /// <param name="inputFile">encrypted file path</param>
        /// <param name="fileStream">destination file FileStream object. </param>
        /// <param name="RijndaelManagedKey">RijndaelManagedKey.</param>
        public static void DecryptFile(string inputFile, ref FileStream fileStream,string RijndaelManagedKey)
        {
            if (inputFile == null)
                throw new ArgumentException("inputFile");
            if (RijndaelManagedKey == null)
                throw new ArgumentException("RijndaelManagedKey");

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(RijndaelManagedKey, new byte[13]
      {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
      });
            CryptoStream cryptoStream = new CryptoStream((Stream)new FileStream(inputFile, FileMode.Open), new RijndaelManaged().CreateDecryptor(passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)), CryptoStreamMode.Read);
            int num;
            while ((num = cryptoStream.ReadByte()) != -1)
                fileStream.WriteByte((byte)num);
            fileStream.Close();
            cryptoStream.Close();

        }

        /// <summary>
        /// This method is used to encrypte the byte array using Rijndael algorithm.
        /// </summary>
        /// <param name="clearData">byte array of data.</param>
        /// <param name="RijndaelManagedKey">RijndaelManagedKey.</param>
        /// <returns>encrypted data byte array.</returns>
        public static byte[] Encrypt(byte[] clearData,string RijndaelManagedKey)
        {
            if (clearData == null)
                throw new ArgumentException("clearData");
              if (RijndaelManagedKey == null)
                throw new ArgumentException("RijndaelManagedKey");

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(RijndaelManagedKey, new byte[13]
      {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
      });
            return Encrypt(clearData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
        }


        /// <summary>
        /// This method is used to decrypte byte array encrypted using Rijndael algorithm.
        /// </summary>
        /// <param name="cipherData">encrypted byte array data.</param>
        /// <param name="RijndaelManagedKey">RijndaelManagedKey.</param>
        /// <returns>decrypted byte array data.</returns>
        public static byte[] Decrypt(byte[] cipherData,string RijndaelManagedKey)
        {
            if (cipherData == null)
                throw new ArgumentException("cipherData");
            if (RijndaelManagedKey == null)
                throw new ArgumentException("RijndaelManagedKey");

            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(RijndaelManagedKey, new byte[13]
      {
        (byte) 73,
        (byte) 118,
        (byte) 97,
        (byte) 110,
        (byte) 32,
        (byte) 77,
        (byte) 101,
        (byte) 100,
        (byte) 118,
        (byte) 101,
        (byte) 100,
        (byte) 101,
        (byte) 118
      });
            return Decrypt(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
        }
        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = Key;
            rijndael.IV = IV;
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipherData, 0, cipherData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }
        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream memoryStream = new MemoryStream();
            Rijndael rijndael = Rijndael.Create();
            rijndael.Key = Key;
            rijndael.IV = IV;
            CryptoStream cryptoStream = new CryptoStream((Stream)memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(clearData, 0, clearData.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

        public static string EncryptString(string text, string password,string salt)
        {
            if (text == null)
                throw new ArgumentException("text");
            if (password == null)
                throw new ArgumentException("password");
            if (salt == null)
                throw new ArgumentException("salt");

            byte[] baPwd = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Encoding.UTF8.GetBytes(text);

            byte[] baSalt = Encoding.UTF8.GetBytes(salt);
            byte[] baEncrypted = new byte[baSalt.Length + baText.Length];

            // Combine Salt + Text
            for (int i = 0; i < baSalt.Length; i++)
                baEncrypted[i] = baSalt[i];
            for (int i = 0; i < baText.Length; i++)
                baEncrypted[i + baSalt.Length] = baText[i];

            baEncrypted = Encrypt(baEncrypted, baPwdHash);

            string result = Convert.ToBase64String(baEncrypted);
            return result;
        }
        public static string DecryptString(string text, string password, string salt)
        {
            if (text == null)
                throw new ArgumentException("text");
            if (password == null)
                throw new ArgumentException("password");
            if (salt == null)
                throw new ArgumentException("salt");

            byte[] baPwd = Encoding.UTF8.GetBytes(password);

            // Hash the password with SHA256
            byte[] baPwdHash = SHA256Managed.Create().ComputeHash(baPwd);

            byte[] baText = Convert.FromBase64String(text);

            byte[] baDecrypted = Decrypt(baText, baPwdHash);

            // Remove salt
            int saltLength = salt.Length;
            byte[] baResult = new byte[baDecrypted.Length - saltLength];
            for (int i = 0; i < baResult.Length; i++)
                baResult[i] = baDecrypted[i + saltLength];

            string result = Encoding.UTF8.GetString(baResult);
            return result;
        }
        /// <summary>
        /// This method is used to encrypte a string using AES algoritm.
        /// </summary>
        /// <param name="bytesToBeEncrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns></returns>
        public static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            if (bytesToBeEncrypted == null)
                throw new ArgumentException("bytesToBeEncrypted");
            if (passwordBytes == null)
                throw new ArgumentException("passwordBytes");
      
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        /// <summary>
        /// This method is used to decript AES algoritm encrypted string.
        /// </summary>
        /// <param name="bytesToBeDecrypted"></param>
        /// <param name="passwordBytes"></param>
        /// <returns></returns>
        public static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            if (bytesToBeDecrypted == null)
                throw new ArgumentException("bytesToBeDecrypted");
            if (passwordBytes == null)
                throw new ArgumentException("passwordBytes");
            
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

    }
}
