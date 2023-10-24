﻿using Moq;
using RaffleShopping.Services.Customers.Features.HashingString;
using RaffleShopping.Services.Customers.Models;
using RaffleShopping.Services.Customers.Repositories;
using RaffleShopping.Services.Customers.Services;

namespace RaffleShopping.Services.Customers.UnitTests
{
    public class CustomerServiceUnitTests
    {
        private Mock<ICustomerRepository> _customerRepository;
        private CustomerService _customerService;

        public CustomerServiceUnitTests() 
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _customerService = new CustomerService( _customerRepository.Object );
        }

        [Test]
        public void Login()
        {
            //Arrange
            var loginCredentials = new Customer
            {
                Email = "ai.hoshino@email.com",
                Password = HashString.Hash("password")
            };

            _customerRepository.Setup(c => c.GetUserByEmailAsync(loginCredentials.Email)).Returns(loginCredentials);

            //Act
            bool resultWrongEmail = _customerService.Login(new LoginModel
            {
                Email = "ai.hayasaka@email.com",
                Password = "password"
            });
            bool resultWrongPassword = _customerService.Login(new LoginModel
            {
                Email = "ai.hoshino@email.com",
                Password = "wrongpassword"
            });
            bool resultSuccess = _customerService.Login(new LoginModel
            {
                Email = "ai.hoshino@email.com",
                Password = "password"
            });

            //Assert
            Assert.IsFalse(resultWrongEmail);
            Assert.IsFalse(resultWrongPassword);
            Assert.IsTrue(resultSuccess);
        }

        [Test]
        public void Register()
        {
            //Arrange
            var customer = new Customer
            {
                Email = "",
                Password = "password"
            };
            _customerRepository.Setup(c => c.AddUserAsync(customer));

            //Act
            _customerService.RegisterCustomer(customer);

            //Assert
            _customerRepository.Verify(c => c.AddUserAsync(customer), Times.Once);
        }
    }
}