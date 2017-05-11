using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using WeChat.ServiceInterface;

namespace WeChat.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost _appHost;

        public UnitTests()
        {
            _appHost = new BasicAppHost(typeof(WxServices).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
            .Init();
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            _appHost.Dispose();
        }

        [Test]
        public void Test_Method1()
        {
            //var service = appHost.Container.Resolve<MyServices>();

            //var response = (HelloResponse)service.Any(new Hello { Name = "World" });

            //Assert.That(response.Result, Is.EqualTo("Hello, World!"));
        }
    }
}
