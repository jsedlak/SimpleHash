using System;
using System.Text;
using System.Security.Cryptography;

namespace JSSC.SimpleHash
{
    public static class Hasher
    {
        public static string GeneratePassword(string input, ref string salt, Encoding encoding = null)
        {
            return Md5Hash(Sha1Hash(input, ref salt, encoding), ref salt, encoding);
        }

        /// <summary>
        /// Creates an MD5 hash of a string using a salt. If no salt is specified, one is created.
        /// </summary>
        /// <param name="input">The plain text input to hash.</param>
        /// <param name="salt">The salt used to create hashed string.</param>
        /// <param name="encoding">The encoding used to gather the bytes. Defaults to UTF8.</param>
        /// <returns>A hashed version of the input string.</returns>
        public static string Md5Hash(string input, ref string salt, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = System.Text.Encoding.UTF8;

            if (string.IsNullOrWhiteSpace(salt))
                salt = KeyGen.Generate(15, KeyGen.AllCharacters);

            byte[]
                saltBytes = encoding.GetBytes(salt),
                inputBytes = encoding.GetBytes(input);

            var hmacMd5 = new HMACMD5(saltBytes);

            byte[] hash = hmacMd5.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash); //encoding.GetString(hash);
        }

        /// <summary>
        /// Creates a SHA1 hash of a string using a salt. If no salt is specified, one is created.
        /// </summary>
        /// <param name="input">The plain text input to hash.</param>
        /// <param name="salt">The salt used to create hashed string.</param>
        /// <param name="encoding">The encoding used to gather the bytes. Defaults to UTF8.</param>
        /// <returns>A hashed version of the input string.</returns>
        public static string Sha1Hash(string input, ref string salt, Encoding encoding = null)
        {
            if (encoding == null)
                encoding = System.Text.Encoding.UTF8;

            if (string.IsNullOrWhiteSpace(salt))
                salt = KeyGen.Generate(15, KeyGen.AllCharacters);

            byte[]
                saltBytes = encoding.GetBytes(salt),
                inputBytes = encoding.GetBytes(input);

            var hmacSha1 = new HMACSHA1(saltBytes);

            byte[] hash = hmacSha1.ComputeHash(inputBytes);

            return Convert.ToBase64String(hash);// encoding.GetString(hash);
        }
    }
}
