using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace SprwMusic.Utils
{
    public static class AuthUtils
    {
        public static string GenerateHash(string value)
        {
            var hashedValue = new StringBuilder();

            using (var hash = SHA256.Create())
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (var b in result)
                    hashedValue.Append(b.ToString("x2"));
            }

            return hashedValue.ToString();
        }

        public static int GenerateSalt()
        {
            var rngCsp = new RNGCryptoServiceProvider();
            var randomNumber = new byte[4];
            rngCsp.GetBytes(randomNumber);

            return BitConverter.ToInt32(randomNumber, 0);
        }

        public static string GenerateToken(SPRW_USER user)
        {
            var rawToken = user.SALT + user.PASSWORD + user.EMAIL;
            var token = GenerateHash(rawToken);

            return token;
        }
    }
}