using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sait2022.Domain.Model;

namespace Sait2022.Controllers
{
    [TestClass]
    public class RangsControllerTest
    {
        [TestMethod]
        public async void TestDetailsView()
        {
            var controller = new RangsController();
            var result = controller.Details(2) as ViewResult;
            var rang = (Rangs)result.ViewData.Model;
            Assert.AreEqual("Details", rang.RangQuest);
            Assert.AreEqual("Details", result.ViewName);
          }

    }
}
