using System;
using System.Security.Cryptography;
using System.Text;

namespace Leaf.Tools
{
    public class MD5Tools
    {
        /// <summary>
        ///     Returns a MD Hash from a string
        /// </summary>
        /// <param name="input"></param>
        /// <returns>
        ///     MD5 hash of Input
        /// </returns>
        public static string GetMd5Hash(string input)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }

        /// <summary>
        ///     Returns a MD5 Hash using random number and Now() DateTime
        /// </summary>
        /// <returns>
        ///     String with MD5 Generated hash
        /// </returns>
        public static string GetRequestHash()
        {
            // Get Current Datetime
            DateTime now = DateTime.Now;

            // Generate random number
            Random rand = new Random();
            string random = rand.Next().ToString();

            // We use now datetime + random number to generate a MD5 hash
            string input = now.ToString("dd/MM/yyyy HH:mm:ss-") + random;

            using (MD5 md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sBuilder.Append(data[i].ToString("x2"));
                }

                // Return the hexadecimal string.
                return sBuilder.ToString();
            }
        }
    }
}

