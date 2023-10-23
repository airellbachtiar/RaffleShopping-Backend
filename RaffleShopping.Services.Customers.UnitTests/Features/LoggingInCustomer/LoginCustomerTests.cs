using RaffleShopping.Services.Customers.Features.LoggingInCustomer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RaffleShopping.Services.Customers.UnitTests.Features.LoggingInCustomer
{
    public class LoginCustomerTests
    {
        LoginCustomer _loginCustomer = new LoginCustomer();
        
        [Test]
        public void CustomerShouldLoginWithValidCredentials()
        {
            string email = "a@b.c";
            string password = "password";

            Assert.IsTrue(_loginCustomer.Login(email, password));
        }
    }
}
