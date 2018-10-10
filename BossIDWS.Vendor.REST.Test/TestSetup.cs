using System;
using System.Collections.Generic;
using BossIDWS.Vendor.REST;
using BossIDWS.Vendor.REST.Data;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;
using Newtonsoft.Json;


namespace BossIDWS.Vendor.REST.Test
{
    public static class TestSetup
    {
        public enum TestType
        {
            LiteDB = 1,
            REST = 2,
            BOSSID = 3
        }

        private static readonly string ServiceUrl;
        private static readonly string APIKey;

        private static string _installationID;
        private static Stack<string> _availableCards;
        private static CardData _newCard;
        private static ReplacementCardData _replacementCard;
        private static AccessPointData _allAccessPoints;
        private static AccessPointData _accessPointsByID;
        private static AccessPointData _accessPointsByName;
        private static AccessPointData _accessPointsByTag;
        private static Client _client1;
        private static Client _client2;
        private static AccessPointOutOfOrderData _apOutOfOrder;

        public static AccessPointOutOfOrderData ApOutOfOrder => _apOutOfOrder;
        public static string InstallationID => _installationID;
        public static TestType Type = TestType.LiteDB;
        public static HouseHoldClientData HouseHoldClient;
        public static HouseHoldClientData HouseHoldClient2;
        public static MovedClientData MovedHouseHoldClient;
        public static CommercialClientData CommercialClient;
        public static List<AccessPoint> AvailableAccessPoints;
        public static CurrentClientData CurrentClient1;
        public static CurrentClientData CurrentClient2;
        public static EventsData RequestAllEvents;
        public static EventsData RequestClientEvents;

        public static Client Client1
        {
            get { return _client1; }
            set
            {
                _client1 = value;
                CurrentClient1 = new CurrentClientData
                {
                    InstallationID = value.installationid,
                    Customerkey = value.customerkey
                };

                RequestClientEvents = new EventsData
                {
                    Customerkey = CurrentClient1.Customerkey,
                    FromDate = DateTime.Now.AddDays(-100),
                    ToDate = DateTime.Now,
                    InstallationID = InstallationID
                };

                _newCard = new CardData
                {
                    RFID = _availableCards.Pop(),
                    InstallationID = CurrentClient1.InstallationID,
                    Customerkey = CurrentClient1.Customerkey
                };

                _replacementCard = new ReplacementCardData()
                {
                    RFID = _newCard.RFID,
                    ReplacementRFID = _availableCards.Pop(),
                    InstallationID = CurrentClient1.InstallationID,
                    Customerkey = CurrentClient1.Customerkey
                };

                Utils.ApplySettings("Client1", JsonConvert.SerializeObject(Client1));
            }
        }

        public static Client Client2
        {
            get { return _client2; }
            set
            {
                _client2 = value;
                CurrentClient2 = new CurrentClientData
                {
                    InstallationID = value.installationid,
                    Customerkey = value.customerkey
                };

                MovedHouseHoldClient = new MovedClientData
                {
                    Customerkey = _client2.customerkey,
                    InstallationID = InstallationID,
                    StreetAddress = "new address",
                    Secondary2 = AvailableAccessPoints[2].accesspointid
                };

                Utils.ApplySettings("Client2", JsonConvert.SerializeObject(Client2));
            }
        }

        public static ReplacementCardData ReplacementCard => _replacementCard;
        public static CardData NewCard => _newCard;
        public static AccessPointData AllAccessPoints => _allAccessPoints;
        public static AccessPointData AccessPointsByID => _accessPointsByID;
        public static AccessPointData AccessPointsByName => _accessPointsByName;
        public static AccessPointData AccessPointsByTag => _accessPointsByTag;


        static TestSetup()
        {
            ServiceUrl = Utils.GetSetting("ServiceUrl");
            APIKey = Utils.GetSetting("apikey");;

            InitializeTestData();
        }

        private static void InitializeTestData()
        {
            /**************************************************
            Testparameters spesific to current test
            **************************************************/

            Type = TestType.REST;
            _installationID = "123";

            //Add accesspoints - at least 3
            AvailableAccessPoints = new List<AccessPoint>();

            var ap1 = new AccessPoint {accesspointid = "1718", tag = "", name = "1"};
            AvailableAccessPoints.Add(ap1);

            var ap2 = new AccessPoint {accesspointid = "1719", tag = "", name = "2"};
            AvailableAccessPoints.Add(ap2);

            var ap3 = new AccessPoint {accesspointid = "1720", tag = "", name = "3"};
            AvailableAccessPoints.Add(ap3);

            /**************************************************
            Generic testparameters - no need to touch.....
            **************************************************/
            _allAccessPoints = new AccessPointData { InstallationID = InstallationID, Type = AccessPointData.AccessPointType.ALL };
            _accessPointsByID = new AccessPointData { InstallationID = InstallationID, Type = AccessPointData.AccessPointType.ID, AccessPoint = ap1.accesspointid };
            _accessPointsByName = new AccessPointData { InstallationID = InstallationID, Type = AccessPointData.AccessPointType.NAME, AccessPoint = ap2.name };
            _accessPointsByTag = new AccessPointData { InstallationID = InstallationID, Type = AccessPointData.AccessPointType.TAG, AccessPoint = ap3.tag };
            _apOutOfOrder = new AccessPointOutOfOrderData { InstallationID = TestSetup.InstallationID, AccessPoint = ap1.name, Message = "Out of Order", Type = AccessPointData.AccessPointType.NAME };

            if (Type == TestType.LiteDB)
            {
                AvailableAccessPoints.Add(VendorDL.GetRandomAccessPoint());
                AvailableAccessPoints.Add(VendorDL.GetRandomAccessPoint());
                AvailableAccessPoints.Add(VendorDL.GetRandomAccessPoint());
            }

            _availableCards = new Stack<string>();

            // Generate RFID cards
            for (var i = 0; i < 50; i++)
            {
                _availableCards.Push(Utils.GetRandomHexString());
            }

            HouseHoldClient = new HouseHoldClientData
            {               
                Customerguid = Guid.NewGuid().ToString(),
                Customerid = Utils.GetRandomNumber(1001, 1000000).ToString(),
                Description = "sample client description",
                Primary = AvailableAccessPoints[0].accesspointid,
                Secondary1 = AvailableAccessPoints[1].accesspointid,
                Propertyunit = "Propertyunitstring",
                Streetaddress = "Customer address",
                RFID = _availableCards.Pop() + ";" + _availableCards.Pop(),
                InstallationID = InstallationID
            };

            HouseHoldClient2 = new HouseHoldClientData
            {
                Customerguid = Guid.NewGuid().ToString(),
                Customerid = Utils.GetRandomNumber(1001, 1000000).ToString(),
                Description = "testclient - to be deleted during test",
                Primary = AvailableAccessPoints[0].accesspointid,
                Secondary1 = AvailableAccessPoints[1].accesspointid,
                Propertyunit = "Propertyunitstring",
                Streetaddress = "Customer address",
                RFID = _availableCards.Pop() + ";" + _availableCards.Pop(),
                InstallationID = InstallationID
            };

            CommercialClient = new CommercialClientData
            {
                Name = "Commercial name",
                Customerguid = Guid.NewGuid().ToString(),
                Customerid = new Random().Next(1001, 1000000).ToString(),
                Description = "sample client description",
                Primary = AvailableAccessPoints[0].accesspointid,
                Secondary1 = AvailableAccessPoints[1].accesspointid,
                Propertyunit = "Propertyunitstring",
                Streetaddress = "Customer address",
                RFID = _availableCards.Pop() + ";" + _availableCards.Pop(),
                InstallationID = InstallationID
            };

            RequestAllEvents = new EventsData
            {
                Customerkey = "ALL",
                FromDate = DateTime.Now.AddDays(-100),
                ToDate = DateTime.Now,
                InstallationID = InstallationID
            };

            if (Client1 == null)
            {
                var c1 = JsonConvert.DeserializeObject<Client>(Utils.GetSetting("Client1"));
                if(c1 != null)
                    Client1 = c1;

                var c2 = JsonConvert.DeserializeObject<Client>(Utils.GetSetting("Client2"));
                if (c2 != null)
                    Client2 = c2;
            }
        }

        public static RO<T> GetData<T>(string url, string json)
        {

            var ro = new RO<T>();

            switch (Type)
            {
                case TestType.REST:
                    ro = Utils.RequestRestData<T>(ServiceUrl, url, json, "POST", APIKey);
                    break;
                case TestType.BOSSID:
                    var xml = Utils.SerializeObjectToXml(HouseHoldClient);
                    break;
            }

            return ro;
        }
    }
}
