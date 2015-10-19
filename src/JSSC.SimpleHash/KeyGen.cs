using System.Text;
using System.Security.Cryptography;

namespace JSSC.SimpleHash
{
    public static class KeyGen
    {
        public const string AllCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string UpperCaseCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public const string LowerCaseCharacters = "abcdefghijklmnopqrstuvwxyz1234567890";

        /// <summary>
        /// Generate a crypto-random string of specified length
        /// </summary>
        /// <param name="size">The length of the string</param>
        /// <param name="allowedCharacters">A string containing the allowed characters</param>
        /// <returns>A cryptographic random string of specified length</returns>
        public static string Generate(int size = 15, string allowedCharacters = AllCharacters)
        {
            char[] chars = allowedCharacters.ToCharArray();
            byte[] data = new byte[1];

            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);

            data = new byte[size];
            crypto.GetNonZeroBytes(data);

            StringBuilder result = new StringBuilder(size);

            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }

            return result.ToString();
        }

        /// <summary>
        /// Generate a crypto-random string of specified length with dashes every 5 characters (for size > 10)
        /// </summary>
        /// <param name="size">The length of the string</param>
        /// <param name="allowedCharacters">A string containing the allowed characters</param>
        /// <returns>A cryptographic random string of specified length</returns>
        public static string GenerateKey(int size = 15, string allowedCharacters = AllCharacters)
        {
            string rawKey = Generate(size, allowedCharacters);

            if (size <= 10) return rawKey;

            for (int i = size - 5; i > 0; i -= 5)
                rawKey = rawKey.Insert(i, "-");

            return rawKey;
        }
    }
}
