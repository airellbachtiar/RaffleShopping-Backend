using FirebaseAdmin.Auth;
using Moq;
using RaffleShopping.Services.Customers.Features.HashingString;
using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Repositories;
using RaffleShopping.Services.Customers.Services;

namespace RaffleShopping.Services.Customers.UnitTests
{
    public class CustomerServiceUnitTests
    {
        private Mock<ICustomerRepository> _customerRepository;
        private Mock<FirebaseAuth> _firebaseAuth;
        private CustomerService _customerService;

        public CustomerServiceUnitTests() 
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService( _customerRepository.Object );
        }

        public void SetupMockLoginCredentials()
        {
            var loginCredentials = new Customer
            {
                Email = "ai.hoshino@email.com",
                Password = HashString.Hash("password")
            };

            _customerRepository.Setup(c => c.GetUserByEmailAsync(loginCredentials.Email)).Returns(Task.FromResult(loginCredentials));
        }

        [Test]
        public void Login_CorrectCredentials_ReturnsTrue()
        {
            //Arrange
            SetupMockLoginCredentials();

            //Act
            bool resultSuccess = _customerService.LoginAsync(new LoginModel
            {
                Email = "ai.hoshino@email.com",
                Password = "password"
            }).Result;

            //Assert
            Assert.IsTrue(resultSuccess);
        }

        [Test]
        public void Login_IncorrectEmail_ReturnsFalse()
        {
            //Arrange
            SetupMockLoginCredentials();

            //Act
            bool resultWrongEmail = _customerService.LoginAsync(new LoginModel
            {
                Email = "ai.hayasaka@email.com",
                Password = "password"
            }).Result;

            //Assert
            Assert.IsFalse(resultWrongEmail);
        }

        /*[Test]
        public void Login_IncorrectPassword_ReturnsFalse()
        {
            //Arrange
            SetupMockLoginCredentials();

            //Act
            bool resultWrongPassword = _customerService.LoginAsync(new LoginModel
            {
                Email = "ai.hoshino@email.com",
                Password = "wrongpassword"
            }).Result;

            //Assert
            Assert.IsFalse(resultWrongPassword);
        }*/

        /*[Test]
        public void Register_NewCustomerShouldBeAddedToDatabase_ReturnsTrue()
        {
            //Arrange
            var customer = new Customer
            {
                Email = "",
                Password = "password"
            };
            UserRecordArgs args = new UserRecordArgs()
            {
                Email = customer.Email,
                Password = customer.Password
            };
            _customerRepository.Setup(c => c.AddUserAsync(customer));
            _firebaseAuth.Setup(f => FirebaseAuth.DefaultInstance.CreateUserAsync(args));

            //Act
            _customerService.RegisterCustomerAsync(customer);

            //Assert
            _customerRepository.Verify(c => c.AddUserAsync(customer), Times.Once);
        }*/
    }
}
