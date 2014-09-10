using System;
using System.Web.Mvc;
using MedicationTherapyManagement.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MedicationTherapyManagement.Tests.Controllers
{
    [TestClass]
    public class ReportsControllerTest
    {
        [TestMethod]
        public void shouldReturnStoredProcResult()
        {
            ReportsController controller = new ReportsController();
            
            var result = controller.runTriggerReport();
           
            Console.WriteLine(result);
            Assert.IsNotNull(result);
        }
    }
}
