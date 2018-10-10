using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BossIDWS.Vendor.REST;
using BossIDWS.Vendor.REST.Controllers;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;
using BossIDWS.Vendor.REST.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BossIDWS.Vendor.REST.Test.Tests
{
    [TestClass]
    public class AccessPointTest
    {
        private readonly AccessPointController _controller;
        public AccessPointTest()
        {
            _controller = new AccessPointController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [TestMethod]
        public void AccessPoints()
        {
            // Setup test params
            const string url = "/api/accesspoint";

            // Get list of ALL accesspoints  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.AccessPoints(TestSetup.AllAccessPoints))
                : TestSetup.GetData<AccessPointList>(url, JsonConvert.SerializeObject(TestSetup.AllAccessPoints));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
            var accesspoints = ro.ReturnValue;
            Assert.IsTrue(accesspoints.AccessPoints.Count > 0);

            // Get accesspoint by NAME
            //ro = TestSetup.Type == TestSetup.TestType.LiteDB
            //    ? AsyncHelper.RunSync(() => _controller.AccessPoints(TestSetup.AccessPointsByName))
            //    : TestSetup.GetData<AccessPointList>(url, JsonConvert.SerializeObject(TestSetup.AccessPointsByName));

            //// Assert the result  
            //Assert.AreEqual(ro.ReturnCode, 0);
            //accesspoints = ro.ReturnValue;
            //Assert.IsTrue(accesspoints.AccessPoints.Count == 1);

            // Get accesspoint by ID
            ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.AccessPoints(TestSetup.AccessPointsByID))
                : TestSetup.GetData<AccessPointList>(url, JsonConvert.SerializeObject(TestSetup.AccessPointsByID));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
            accesspoints = ro.ReturnValue;
            Assert.IsTrue(accesspoints.AccessPoints.Count == 1);

            // Get accesspoint by TAG
            //ro = TestSetup.Type == TestSetup.TestType.LiteDB
            //    ? AsyncHelper.RunSync(() => _controller.AccessPoints(TestSetup.AccessPointsByTag))
            //    : TestSetup.GetData<AccessPointList>(url, JsonConvert.SerializeObject(TestSetup.AccessPointsByTag));

            //// Assert the result  
            //Assert.AreEqual(ro.ReturnCode, 0);
            //accesspoints = ro.ReturnValue;
            //Assert.IsTrue(accesspoints.AccessPoints.Count == 0);
        }

        [TestMethod]
        public void AccessPointDetails()
        {
            // Setup test params
            const string url = "/api/accesspoint/details";

            // Get list of accesspoints  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.AccessPointDetails(TestSetup.AllAccessPoints))
                : TestSetup.GetData<AccessPointDetailList>(url, JsonConvert.SerializeObject(TestSetup.AllAccessPoints));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            var accesspointDetailsList = ro.ReturnValue;
            Assert.IsTrue(accesspointDetailsList.AccessPoints.Count > 0);
        }

        [TestMethod]
        public void AccessPointStatus()
        {
            // Setup test params
            const string url = "/api/accesspoint/status";

            var controller = new AccessPointController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };

            // Act on Test  
            var clientData = new ClientData {InstallationID = TestSetup.InstallationID};

            // Get list of accesspoints  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.AccessPointStatus(clientData))
                : TestSetup.GetData<AccessPointStatusList>(url, JsonConvert.SerializeObject(clientData));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            var accesspoints = ro.ReturnValue;
            Assert.IsTrue(accesspoints.AccessPoints.Count > 0);
        }

        [TestMethod]
        public void AccessPointOutOfOrder()
        {
            // Setup test params
            const string url = "/api/accesspoint/outoforder";

            // Send notification that an accesspoint is out of order  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.AccessPointOutOfOrder(TestSetup.ApOutOfOrder))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.ApOutOfOrder));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
        }
    }
}
