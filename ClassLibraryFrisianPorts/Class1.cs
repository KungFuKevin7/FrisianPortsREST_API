using System.Security.Cryptography;
using System.Text;

namespace ClassLibraryFrisianPorts
{
    public class Class1
    {
        public string HashPassword(string input)
        {
            var key = "79b1171071079911b";
            HMACSHA512 HMAC = new HMACSHA512(Encoding.UTF8.GetBytes(key));
            var encodedPassword = HMAC.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(encodedPassword);
        }
    }
}
