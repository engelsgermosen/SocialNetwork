using System.Security.Cryptography;
using System.Text;

namespace SocialNetwork.Core.Application.Helpers
{
    public class PasswordEncrypter
    {
        public static string ComputeHash(string password)
        {
            using(var hash = SHA256.Create())
            {
                byte[] bytes = hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                var sb = new StringBuilder();

                for(int i = 0; i < bytes.Length; i++)
                {
                    sb.Append(bytes[i].ToString("x2"));
                }
                return sb.ToString();
            }
        }
    }
}
