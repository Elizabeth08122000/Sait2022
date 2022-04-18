using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sait2022;
using Sait2022.Controllers;
using Sait2022.Domain.Model;

namespace AccountTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestDetailsView()
        {
            int id = 1;
            var controller = new RangsController();
            controller.Details(id);
            Assert.IsNotNull(controller.Details(id));
        }
    }
}