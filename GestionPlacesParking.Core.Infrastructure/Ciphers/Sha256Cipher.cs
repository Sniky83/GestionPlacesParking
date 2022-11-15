using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace GestionPlacesParking.Core.Infrastructure.Web.Cipher
{
    public class Sha256Cipher
    {
        public static string Hash(string randomString)
        {
            SHA256 mySHA256 = SHA256.Create();

            var hash = new StringBuilder();
            byte[] crypto = mySHA256.ComputeHash(Encoding.UTF8.GetBytes(randomString));

            foreach (byte theByte in crypto)
            {
                hash.Append(theByte.ToString("x2"));
            }

            return hash.ToString();
        }
    }
}
