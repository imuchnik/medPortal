using System.Web.Mvc;
using MedicationTherapyManagement.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MedicationTherapyManagement.Tests.Controllers
{
    [TestClass]
    public class PortalControllerTest
    {
        [TestMethod]
        public void Index()
        {
            // Arrange
            PortalController controller = new PortalController();

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }

  
    }
}
