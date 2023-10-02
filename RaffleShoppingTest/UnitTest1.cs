using RaffleShopping;

namespace RaffleShoppingTest
{
    public class Tests
    {
        private Class1 testClass;

        [SetUp]
        public void Setup()
        {
            testClass = new Class1();
        }

        [Test]
        public void Get1FromClass1()
        {
            Assert.That(testClass.GetNumber(), Is.EqualTo(1));
        }
    }
}