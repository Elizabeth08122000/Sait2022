using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sait2022;
using Sait2022.Controllers;

namespace AccountTests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly ILogger<HomeController> _logger;

        [TestMethod]
        public void TestIndexView()
        {
            var controller = new HomeController(_logger);
            Assert.IsNotNull(controller.Index());
        }

        [TestMethod]
        public void TestPrivacyView()
        {
            var controller = new HomeController(_logger);
            Assert.IsNotNull(controller.Privacy());
        }
    }
}