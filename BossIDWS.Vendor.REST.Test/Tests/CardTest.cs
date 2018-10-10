using System.Linq;
using System.Net.Http;
using System.Web.Http;
using BossIDWS.Vendor.REST.Controllers;
using BossIDWS.Vendor.REST.ReturnObjects;
using BossIDWS.Vendor.REST.Test.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BossIDWS.Vendor.REST.Test.Tests
{
    [TestClass]
    public class CardTest
    {
        private readonly CardController _controller;

        public CardTest()
        {
            _controller = new CardController
            {
                Request = new HttpRequestMessage(),
                Configuration = new HttpConfiguration()
            };
        }

        [TestMethod]
        public void CardNew()
        {
            // Setup test params
            string url = "/api/card/new";

            // Add new card to client  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardNew(TestSetup.NewCard))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.NewCard));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            // Check that the card is actually in the cardlist
            var card = GetCard(TestSetup.NewCard.RFID);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.status, "A");
        }

        [TestMethod]
        public void CardReplace()
        {
            // Setup test params
            string url = "/api/card/replace";

            // Replace card for client  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardReplace(TestSetup.ReplacementCard))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.ReplacementCard));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            // Check that the cards is actually replaced in the cardlist
            var card = GetCard(TestSetup.ReplacementCard.RFID);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.status, "D");

            var replacementCard = GetCard(TestSetup.ReplacementCard.ReplacementRFID);
            Assert.IsNotNull(replacementCard);
            Assert.AreEqual(replacementCard.status, "A");
        }

        [TestMethod]
        public void CardDeactivate()
        {
            // Setup test params
            string url = "/api/card/deactivate";

            // Deactivate card for client  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardDeactivate(TestSetup.NewCard))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.NewCard));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            // Check that the cards is actually deactivated in the cardlist
            var card = GetCard(TestSetup.NewCard.RFID);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.status, "B");
        }

        [TestMethod]
        public void CardActivate()
        {
            // Setup test params
            string url = "/api/card/activate";

            // Activate card for client  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardActivate(TestSetup.NewCard))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.NewCard));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            // Check that the cards is actually activated in the cardlist
            var card = GetCard(TestSetup.NewCard.RFID);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.status, "A");
        }

        [TestMethod]
        public void CardDelete()
        {
            // Setup test params
            const string url = "/api/card/delete";

            // Activate card for client  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardDelete(TestSetup.NewCard))
                : TestSetup.GetData<string>(url, JsonConvert.SerializeObject(TestSetup.NewCard));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);

            // Check that the cards is actually deleted in the cardlist
            var card = GetCard(TestSetup.NewCard.RFID);
            Assert.IsNotNull(card);
            Assert.AreEqual(card.status, "D");
        }

        private Card GetCard(string rfid)
        {
            const string url = "/api/card/list";

            // Get cards list
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardList(TestSetup.CurrentClient1))
                : TestSetup.GetData<CardList>(url, JsonConvert.SerializeObject(TestSetup.CurrentClient1));

            return ro.ReturnValue.Cards.SingleOrDefault(c => c.rfid.Equals(rfid));
        }

        [TestMethod]
        public void CardList()
        {
            // Setup test params
            const string url = "/api/card/list";

            // Act on Test  
            var ro = TestSetup.Type == TestSetup.TestType.LiteDB
                ? AsyncHelper.RunSync(() => _controller.CardList(TestSetup.CurrentClient1))
                : TestSetup.GetData<CardList>(url, JsonConvert.SerializeObject(TestSetup.CurrentClient1));

            // Assert the result  
            Assert.AreEqual(ro.ReturnCode, 0);
            var cardList = ro.ReturnValue;
            Assert.IsTrue(cardList.Cards.Count > 0);
        }
    }
}
