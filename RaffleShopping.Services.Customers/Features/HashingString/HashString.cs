using System.Security.Cryptography;
using System.Text;

namespace RaffleShopping.Services.Customers.Features.HashingString
{
    public static class HashString
    {
        public static string Hash(string password)
        {
            // ComputeHash - returns byte array
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));

            // Convert byte array to a string
            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
