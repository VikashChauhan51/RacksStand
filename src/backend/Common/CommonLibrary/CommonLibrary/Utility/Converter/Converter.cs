using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;

namespace Utility.Converter
{
   public class Converter
    {
        /// <summary>
        /// This method is used to convert a byte array to image.
        /// </summary>
        /// <param name="bytes">(image) byte array.</param>
        /// <returns> image object.</returns>
        public static Image ConvertByteArrayToImage(byte[] bytes)
        {
            Image image;
            using (MemoryStream memoryStream = new MemoryStream(bytes))
                image = Image.FromStream((Stream)memoryStream);
            return image;
        }
        /// <summary>
        /// This method is uesd to convert file to FileStream.
        /// </summary>
        /// <param name="path">file path.</param>
        /// <returns>FileStream object.</returns>
        public static FileStream GetStream(string path)
        {
            return new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read);
        }
        /// <summary>
        /// This method is used to convert base64string to byte array.
        /// </summary>
        /// <param name="base64">base64 string.</param>
        /// <returns>byte array</returns>
        public static byte[] Base64ToByteArray(string base64)
        {
            return !string.IsNullOrEmpty(base64) ? Convert.FromBase64String(base64) : (byte[])null;
        }
        /// <summary>
        /// This method is used to convert stream to byte array.
        /// </summary>
        /// <param name="stream">stream object.</param>
        /// <returns>byte array.</returns>
        public static byte[] ConvertStreamToByteArray(Stream stream)
        {
            byte[] buffer = new byte[stream.Length];
            using (MemoryStream ms = new MemoryStream())
            {
                int read;
                while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
                {
                    ms.Write(buffer, 0, read);
                }
                return ms.ToArray();
            }
        }
        /// <summary>
        /// This method is used to convert stream to byte array with length.
        /// </summary>
        /// <param name="stream">stream object.</param>
        /// <param name="initialLength">max array length.</param>
        /// <returns></returns>
        public static byte[] ConvertStreamToBinaryArrary(Stream stream, int initialLength)
        {
            // If we've been passed an unhelpful initial length, just
            // use 32K.
            if (initialLength < 1)
            {
                initialLength = 32768;
            }

            byte[] buffer = new byte[initialLength];
            int read = 0;

            int chunk;
            while ((chunk = stream.Read(buffer, read, buffer.Length - read)) > 0)
            {
                read += chunk;

                // If we've reached the end of our buffer, check to see if there's
                // any more information
                if (read == buffer.Length)
                {
                    int nextByte = stream.ReadByte();

                    // End of stream? If so, we're done
                    if (nextByte == -1)
                    {
                        return buffer;
                    }

                    // Nope. Resize the buffer, put in the byte we've just
                    // read, and continue
                    byte[] newBuffer = new byte[buffer.Length * 2];
                    Array.Copy(buffer, newBuffer, buffer.Length);
                    newBuffer[read] = (byte)nextByte;
                    buffer = newBuffer;
                    read++;
                }
            }
            // Buffer is now too big. Shrink it.
            byte[] ret = new byte[read];
            Array.Copy(buffer, ret, read);
            return ret;
        }

        /// <summary>
        /// This method is used to convert byte array to base64 string.
        /// </summary>
        /// <param name="bytes">byte array</param>
        /// <returns>base64 string</returns>
        public static string ConvertByteArrayToBase64String(byte[] bytes)
        {
            return bytes != null ? Convert.ToBase64String(bytes) : (string)null;
        }
        /// <summary>
        /// This method is used to create image thumbnail.
        /// The default thumbnail width and height is 100(e.g. 100*100).
        /// </summary>
        /// <param name="image">image object</param>
        /// <param name="width">thumbnail width</param>
        /// <param name="height">thumbnail height</param>
        /// <returns>thumbnail image object</returns>
        public static Image CreateImageThumbnailImage(Image image, int width = 100, int height = 100)
        {
            return image.GetThumbnailImage(width, height, (Image.GetThumbnailImageAbort)(() => false), IntPtr.Zero);
        }
    }
}
