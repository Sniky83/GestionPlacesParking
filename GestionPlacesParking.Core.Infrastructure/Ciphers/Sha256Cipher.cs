using System.Security.Cryptography;
using System.Text;

namespace GestionPlacesParking.Core.Infrastructure.Web.Cipher
{
    public static class Sha256Cipher
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
