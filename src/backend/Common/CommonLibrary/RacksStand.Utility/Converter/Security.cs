using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace RacksStand.Utility.Converter
{
    /// <summary>
    /// This class is used to Encryption/Decryption related task.
    /// </summary>
    public class Security
    {
        /// <summary>
        /// This method is used to decript RSA algoritm encrypted string.
        /// </summary>
        /// <param name="cipherText">encrypted string.</param>
        /// <returns>decrypted string.</returns>
        public static string RSADecrypt(string cipherText)
        {
            string rsaPrivateKey = "RSAPrivateKey";
            int dwKeySize = Convert.ToInt32("RsaPrivateKeySize");
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            cryptoServiceProvider.FromXmlString(rsaPrivateKey);
            int length = dwKeySize / 8 % 3 != 0 ? dwKeySize / 8 / 3 * 4 + 4 : dwKeySize / 8 / 3 * 4;
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
        /// <param name="data">string data</param>
        /// <returns>encrypted string</returns>
        public static string RSAEncrypt(string data)
        {
            string rsaPublicKey = "RSAPublicKey";
            int dwKeySize = Convert.ToInt32("RsaPublicKeySize");
            RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider(dwKeySize);
            StringBuilder stringBuilder = new StringBuilder();
            cryptoServiceProvider.FromXmlString(rsaPublicKey);
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            int num1 = dwKeySize - 42;
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
        public static void EncryptFile(string inputFile, string outputFile)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes("RijndaelManagedKey", new byte[13]
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
        public static void DecryptFile(string inputFile, string outputFile)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes("RijndaelManagedKey", new byte[13]
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
        public static void DecryptFile(string inputFile, ref FileStream fileStream)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes("RijndaelManagedKey", new byte[13]
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
        /// <returns>encrypted data byte array.</returns>
        public static byte[] Encrypt(byte[] clearData)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes("RijndaelManagedKey", new byte[13]
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
            return Security.Encrypt(clearData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
        }


        /// <summary>
        /// This method is used to decrypte byte array encrypted using Rijndael algorithm.
        /// </summary>
        /// <param name="cipherData">encrypted byte array data.</param>
        /// <returns>decrypted byte array data.</returns>
        public static byte[] Decrypt(byte[] cipherData)
        {
            PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes("RijndaelManagedKey", new byte[13]
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
            return Security.Decrypt(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
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
    }
}
