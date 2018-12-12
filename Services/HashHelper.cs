using System;
using System.Security.Cryptography;
using System.Text;

namespace Services
{
    public static class HashHelper
    {
        public static string GetRandomPassword()
        {
            return Guid.NewGuid().ToString().Substring(0, 8);
        }

        public static string GetPasswordSalt()
        {
            return Convert.ToBase64String(Guid.NewGuid().ToByteArray()).Substring(0, 16);
        }

        public static string GetPasswordHash(string passwordSalt, string passwordPlain)
        {
            using (HashAlgorithm hashAlgorithm = new SHA256Managed())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(passwordPlain);
                return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes));
            }
        }


    }
}
