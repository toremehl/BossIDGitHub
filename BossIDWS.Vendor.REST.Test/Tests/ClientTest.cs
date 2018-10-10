using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BossIDWS.Vendor.REST;
using BossIDWS.Vendor.REST.Controllers;
using BossIDWS.Vendor.REST.ReturnObjects;
using BossIDWS.Vendor.REST.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using static BossIDWS.Vendor.REST.Test.TestSetup.TestType;

namespace BossIDWS.Vendor.REST.Test.Tests
{
    [TestClass]
    public class ClientTest
    {
        readonly ClientController _controller;

        public ClientTest()
        {
            _controller = new ClientController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [TestMethod]
        public void CustomerNewHousehold()
        {
            var type = TestSetup.Type;

            // Setup test params
            const string url = "/api/client/household/new";

            // Act on Test - add a client
            var ro = TestSetup.Type == LiteDB 
                ? AsyncHelper.RunSync(() => _controller.CustomerNewHousehold(TestSetup.HouseHoldClient)) 
                : TestSetup.GetData<Client>(url, JsonConvert.SerializeObject(TestSetup.HouseHoldClient));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
            TestSetup.Client1 = ro.ReturnValue;
            Assert.IsNotNull(TestSetup.Client1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(TestSetup.Client1.customerkey));
            Assert.IsTrue(TestSetup.Client1.AccessPoints.Count > 0);

            // Act on Test - add another client
            ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerNewHousehold(TestSetup.HouseHoldClient2))
                : TestSetup.GetData<Client>(url, JsonConvert.SerializeObject(TestSetup.HouseHoldClient2));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
            TestSetup.Client2 = ro.ReturnValue;
            Assert.IsNotNull(TestSetup.Client2);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(TestSetup.Client2.customerkey));
            Assert.IsTrue(TestSetup.Client2.AccessPoints.Count > 0);
        }

        [TestMethod]
        public void CustomerNewCommercial()
        {
            // Setup test params
            const string url = "/api/client/commercial/new";

            // Act on Test  
            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerNewCommercial(TestSetup.CommercialClient))
                : TestSetup.GetData<Client>(url, JsonConvert.SerializeObject(TestSetup.CommercialClient));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
            TestSetup.Client1 = ro.ReturnValue;
            Assert.IsNotNull(TestSetup.Client1);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(TestSetup.Client1.customerkey));
            Assert.IsTrue(TestSetup.Client1.AccessPoints.Count > 0);
        }

        [TestMethod]
        public void CustomerMove()
        {
            // Setup test params
            const string url = "/api/client/move";

            // Act on Test  
            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerMove(TestSetup.MovedHouseHoldClient))
                : TestSetup.GetData<Client>(url, JsonConvert.SerializeObject(TestSetup.MovedHouseHoldClient));

            // Assert the result - checking that new Secondary accesspoint has been added 
            Assert.AreEqual(ro.ReturnCode, 0);

            var singleOrDefault = ro.ReturnValue.AccessPoints.SingleOrDefault(a => a.role.Equals("S2") || a.role.Equals("NA"));
            if (singleOrDefault != null)
            {
                var s2 = singleOrDefault.accesspointid;

                var testS2 = TestSetup.AvailableAccessPoints[2].accesspointid;
                Assert.AreEqual(s2, testS2);
            }
            else
            {
                Assert.Fail("Did not find S2 accesspoint....");
            }
        }

        [TestMethod]
        public void CustomerActivate()
        {
            // Setup test params
            const string url = "/api/client/activate";

            // Act on Test  
            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerActivate(TestSetup.CurrentClient2))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.CurrentClient2));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
        }

        [TestMethod]
        public void CustomerDeactivate()
        {
            // Setup test params
            const string url = "/api/client/deactivate";

            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerDeactivate(TestSetup.CurrentClient2))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.CurrentClient2));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
        }

        [TestMethod]
        public void CustomerDelete()
        {
            // Setup test params
            const string url = "/api/client/delete";

            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerDelete(TestSetup.CurrentClient2))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.CurrentClient2));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
        }

        [TestMethod]
        public void CustomerAccessPoints()
        {
            // Setup test params
            const string url = "/api/client/accesspoints";

            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerAccessPoints(TestSetup.CurrentClient1))
                : TestSetup.GetData<AccessPointDetailList>(url, JsonConvert.SerializeObject(TestSetup.CurrentClient1));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            var accesspoints = ro.ReturnValue;
            Assert.IsTrue(accesspoints.AccessPoints.Count > 0);
        }

        [TestMethod]
        public void CustomerEvents()
        {
            // Setup test params
            const string url = "/api/client/events";

            // Act on Test  

            var ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerEvents(TestSetup.RequestAllEvents))
                : TestSetup.GetData<CustomerEventList>(url, JsonConvert.SerializeObject(TestSetup.RequestAllEvents));
            
            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            var events = ro.ReturnValue;
            Assert.IsTrue(events.EventList.Count > 0);

            ro = TestSetup.Type == LiteDB
                ? AsyncHelper.RunSync(() => _controller.CustomerEvents(TestSetup.RequestClientEvents))
                : TestSetup.GetData<CustomerEventList>(url, JsonConvert.SerializeObject(TestSetup.RequestClientEvents));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            events = ro.ReturnValue;
            Assert.IsTrue(events.EventList.Count > 0);
        }
    }
}
