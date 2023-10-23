using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace RaffleShopping.Services.Customers.Features.LoggingInCustomer
{
    public class LoginCustomer
    {
        public bool Login(string email, string password)
        {
            password = HashPassword(password);
            if (email == "a@b.c" && password == "5e884898da28047151d0e56f8dc6292773603d0d6aabbdd62a11ef721d1542d8")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convert byte array to a string
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
