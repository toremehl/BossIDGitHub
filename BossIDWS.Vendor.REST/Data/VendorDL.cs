using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;
using LiteDB;
using Card = BossIDWS.Vendor.REST.ReturnObjects.Card;

#pragma warning disable 1591

namespace BossIDWS.Vendor.REST.Data
{
	/// <summary>
	/// 
	/// </summary>
	public static class VendorDL
	{
		private static LiteDatabase _db;

		static VendorDL()
		{
			var data = new Dbobjects();
			_db = data.LiteDb;
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public static async Task<Client> AddClientNew(ClientNewData client)
		{
			return null;
		}

		public static async Task<Client> AddNewClient(HouseHoldClientData client)
		{
			// Get card collection
			var allCards = _db.GetCollection<DBCard>("cards");

			// Get customer collection
			var allClients = _db.GetCollection<DBClient>("clients");

			// Get accesspoint collection
			var allAccesspoints = _db.GetCollection<DBAccessPoint>("accesspoints");

			// Get accesspointdetail collection
			var allAccesspointDetails = _db.GetCollection<DBAccessPointDetail>("accesspointDetails");

			var cards = client.RFID.Split(';').Select(rfid => allCards.FindOne(x => x.RFID.Equals(rfid))).ToList();
			var primaryPoints = client.Primary.Split(';').Select(nameOfPoint => allAccesspoints.FindOne(a => a.name.Equals(nameOfPoint))).ToList();
			var secondary1Points = client.Secondary1.Split(';').Select(nameOfPoint => allAccesspoints.FindOne(a => a.name.Equals(nameOfPoint))).ToList();
			var secondary2Points = client.Secondary2.Split(';').Select(nameOfPoint => allAccesspoints.FindOne(a => a.name.Equals(nameOfPoint))).ToList();

			// Insert new client and cards assigned to client
			var dbclient = new DBClient
			{
				InstallationID = client.InstallationID,
				ClientType = "H",
				Customerid = client.Customerid,
				Customerguid = client.Customerguid,
				Propertyunit = client.Propertyunit,
				Streetaddress = client.Streetaddress,
				Description = client.Description,
				Primary = client.Primary,
				Secondary1 = client.Secondary1,
				Secondary2 = client.Secondary2,
				Cards = cards,
				PrimaryAccessPoints = primaryPoints,
				Secondary1AccessPoints = secondary1Points,
				Secondary2AccessPoints = secondary2Points
			};

			allClients.Insert(dbclient);

			// Verify
			var fullClient = allClients.Include(x => x.Cards).Include(x => x.PrimaryAccessPoints).Include(x => x.Secondary1AccessPoints).Include(x => x.Secondary2AccessPoints).Find(x => x.Customerid.Equals(client.Customerid)).FirstOrDefault();

			var bossIDCustomer = new Client();

			if (fullClient != null)
			{
				var accesspoints =
					fullClient.PrimaryAccessPoints.Concat(fullClient.Secondary1AccessPoints)
						.Concat(fullClient.Secondary2AccessPoints)
						.Select(x => new BossIDWS.Vendor.REST.ReturnObjects.AccessPoint
						{
							name = x.name
						}).ToList();

				bossIDCustomer = new Client
				{
					customerkey = fullClient.Id.ToString(),
					AccessPoints = accesspoints,
					installationid = client.InstallationID
				};
			}

			return await Task.FromResult(bossIDCustomer);
		}

		public static async Task<Client> CustomerMove(MovedClientData movedClient)
		{
			// Get customer collection
			var clients = _db.GetCollection<DBClient>("clients");
			var client = clients.FindOne(x => x.Id.Equals(int.Parse(movedClient.CustomerKey)));

			client.Name = movedClient.Name;
			client.Propertyunit = movedClient.PropertyUnit;
			client.Streetaddress = movedClient.StreetAddress;
			client.Description = movedClient.Description;
			client.Primary = movedClient.Primary;
			client.Secondary1 = movedClient.Secondary1;
			client.Secondary2 = movedClient.Secondary2;

			var bossIDCustomer = new Client();
			if (clients.Update(client))
			{
				var allAccesspoints =
					client.PrimaryAccessPoints.Concat(client.Secondary1AccessPoints)
						.Concat(client.Secondary2AccessPoints)
						.Select(x => new BossIDWS.Vendor.REST.ReturnObjects.AccessPoint { name = x.name }).ToList();

				bossIDCustomer = new Client
				{
					customerkey = client.Id.ToString(),
					AccessPoints = allAccesspoints,
					installationid = client.InstallationID
				};
			}

			return await Task.FromResult(bossIDCustomer);
		}

		public static async Task<int> CustomerStatusChanged(bool blocked, bool deleted, string customerKey)
		{
			var clients = _db.GetCollection<DBClient>("clients");
			var client = clients.FindOne(x => x.Id.Equals(int.Parse(customerKey)));

			client.Blocked = blocked;
			client.Deleted = deleted;
			clients.Update(client);

			return await Task.FromResult(0);
		}

		public static async Task<AccessPointDetailList> CustomerAccessPoints(string customerKey)
		{

			var clients = _db.GetCollection<DBClient>("clients");
			var details = _db.GetCollection<DBAccessPointDetail>("accesspointDetails");

			var client = clients.Include(x => x.PrimaryAccessPoints).Include(x => x.Secondary1AccessPoints).Include(x => x.Secondary2AccessPoints).FindOne(x => x.Id.Equals(int.Parse(customerKey)));
			var allAccesspoints =
				client.PrimaryAccessPoints.Concat(client.Secondary1AccessPoints)
					.Concat(client.Secondary2AccessPoints).Select(x => x.accesspointid)
					.ToList();

			var allDetails = allAccesspoints.Select(ap => details.FindOne(x => x.accesspointid.Equals(ap))).ToList().Select(dbap => new AccessPointDetail
			{
				installationid = dbap.installationid,
				accesspointid = dbap.accesspointid,
				accesspointguid = dbap.accesspointguid,
				barcode = dbap.barcode,
				batterylevel = dbap.batterylevel,
				batterytype = dbap.batterytype,
				cameraurl = dbap.cameraurl,
				serial = dbap.serial,
				error = dbap.error,
				filllevel = dbap.filllevel,
				height = dbap.height,
				length = dbap.length,
				pictureurl = dbap.pictureurl,
				pointtype = dbap.pointtype,
				tag = dbap.tag,
				temperature = dbap.temperature,
				volume = dbap.volume,
				warning = dbap.warning,
				weight = dbap.weight,
				width = dbap.weight
			}).ToList();

			var detailsList = new AccessPointDetailList { AccessPoints = allDetails };
			return await Task.FromResult(detailsList);
		}

		public static async Task<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList> AccessPoints(AccessPointData accesspointData)
		{
			var accesspointsList = _db.GetCollection<DBAccessPoint>("accesspoints");
			var accessPoints = new List<DBAccessPoint>();

			switch (accesspointData.Type)
			{
				case AccessPointData.AccessPointType.ID:
					accessPoints = accesspointsList.Find(a => a.accesspointid.Equals(accesspointData.AccessPoint)).ToList();
					break;
				case AccessPointData.AccessPointType.TAG:
					accessPoints = accesspointsList.Find(a => a.tag.Equals(accesspointData.AccessPoint)).ToList();
					break;
				case AccessPointData.AccessPointType.NAME:
					accessPoints = accesspointsList.Find(a => a.name.Equals(accesspointData.AccessPoint)).ToList();
					break;
				case AccessPointData.AccessPointType.ALL:
					accessPoints = accesspointsList.FindAll().ToList();
					break;
			}

			var ap = accessPoints.Select(a => new BossIDWS.Vendor.REST.ReturnObjects.AccessPoint
			{
				//accesspointguid = a.accesspointguid,
				accesspointid = a.accesspointid,
				installationid = a.installationid,
				name = a.name
			}).ToList();
			var apList = new BossIDWS.Vendor.REST.ReturnObjects.AccessPointList();
			// TJM fixed from list to array - assign with convert to array
			apList.AccessPoints = ap.ToArray();
			return await Task.FromResult(apList);
		}

		public static async Task<AccessPointDetailList> AccessPointDetails(AccessPointData accesspointData)
		{
			var accesspointsList = _db.GetCollection<DBAccessPoint>("accesspoints");
			var accesspointDetailList = _db.GetCollection<DBAccessPointDetail>("accesspointDetails");
			var accessPoints = new List<string>();

			switch (accesspointData.Type)
			{
				case AccessPointData.AccessPointType.ID:
					accessPoints = accesspointsList.Find(a => a.accesspointid.Equals(accesspointData.AccessPoint)).Select(x => x.accesspointid).ToList();
					break;
				case AccessPointData.AccessPointType.TAG:
					accessPoints = accesspointsList.Find(a => a.tag.Equals(accesspointData.AccessPoint)).Select(x => x.accesspointid).ToList();
					break;
				case AccessPointData.AccessPointType.NAME:
					accessPoints = accesspointsList.Find(a => a.name.Equals(accesspointData.AccessPoint)).Select(x => x.accesspointid).ToList();
					break;
				case AccessPointData.AccessPointType.ALL:
					accessPoints = accesspointsList.FindAll().Select(x => x.accesspointid).ToList();
					break;
			}

			var allDetails = accessPoints.Select(ap => accesspointDetailList.FindOne(x => x.accesspointid.Equals(ap))).ToList().Select(dbap => new AccessPointDetail
			{
				installationid = dbap.installationid,
				accesspointid = dbap.accesspointid,
				accesspointguid = dbap.accesspointguid,
				barcode = dbap.barcode,
				batterylevel = dbap.batterylevel,
				batterytype = dbap.batterytype,
				cameraurl = dbap.cameraurl,
				serial = dbap.serial,
				error = dbap.error,
				filllevel = dbap.filllevel,
				height = dbap.height,
				length = dbap.length,
				pictureurl = dbap.pictureurl,
				pointtype = dbap.pointtype,
				tag = dbap.tag,
				temperature = dbap.temperature,
				volume = dbap.volume,
				warning = dbap.warning,
				weight = dbap.weight,
				width = dbap.weight
			}).ToList();

			var detailsList = new AccessPointDetailList();
			detailsList.AccessPoints = allDetails;
			return await Task.FromResult(detailsList);
		}

		public static async Task<AccessPointStatusList> AccessPointStatus(string installationID)
		{
			var accesspointsList = _db.GetCollection<DBAccessPoint>("accesspoints");
			var accesspointDetailList = _db.GetCollection<DBAccessPointDetail>("accesspointDetails");

			var accessPoints = accesspointsList.FindAll();

			if (installationID != "NA")
				accessPoints = accessPoints.Where(a => a.installationid.Equals(installationID));

			var ap = accessPoints.Select(a => new AccessPointStatus
			{
				accesspointid = a.accesspointid,
				installationid = a.installationid,
				state = a.state,
				statuscode = "0",
				statustext = ""
			}).ToList();

			var statusList = new AccessPointStatusList { AccessPoints = ap };

			return await Task.FromResult(statusList);
		}

		public static async Task<RO<string>> AccessPointOutOfOrder(AccessPointOutOfOrderData accessPointData)
		{
			var accesspointsList = _db.GetCollection<DBAccessPoint>("accesspoints");
			var accessPoints = accesspointsList.FindAll();
			if (accessPointData.InstallationID != "NA")
				accessPoints = accessPoints.Where(a => a.installationid.Equals(accessPointData.InstallationID));

			DBAccessPoint accessPoint = null;

			if (!string.IsNullOrWhiteSpace(accessPointData.AccessPoint))
			{
				switch (accessPointData.Type)
				{
					case AccessPointData.AccessPointType.ID:
						accessPoint = accessPoints.SingleOrDefault(a => a.accesspointid.Equals(accessPointData.AccessPoint));
						break;
					case AccessPointData.AccessPointType.TAG:
						accessPoint = accessPoints.SingleOrDefault(a => a.tag.Equals(accessPointData.AccessPoint));
						break;
					case AccessPointData.AccessPointType.NAME:
						accessPoint = accessPoints.SingleOrDefault(a => a.name.Equals(accessPointData.AccessPoint));
						break;
				}
			}

			var ro = new RO<string> { ReturnValue = string.Empty, ReturnCode = 0, Message = string.Empty };

			if (accessPoint != null)
			{
				accessPoint.state = "U";
				//accessPoint.Details.error = accessPointData.Message;
				accesspointsList.Update(accessPoint);
			}
			else
			{
				ro.ReturnCode = -1;
				ro.Message = "Could not update status...";
			}

			return await Task.FromResult(ro);
		}

		public static async Task<CustomerEventList> GetCustomerEvents(CustomerEventsData eventsToFind)
		{
			var eventsList = _db.GetCollection<DBEvents>("customerEvents");
			//var customerEvents = eventsList.Find(e => e.EventDate >= eventsToFind.FromDate && e.EventDate <= eventsToFind.ToDate);

			//if (eventsToFind.Customerkey.ToUpper() != "ALL")
			//	customerEvents = customerEvents.Where(e => e.customerkey.Equals(eventsToFind.Customerkey));

			//var events = customerEvents.Select(e => new CustomerEvent
			//{
			//	installationid = e.installationid,
			//	accesspointid = e.accesspointid,
			//	rfid = e.rfid,
			//	customerkey = e.customerkey,
			//	size = e.size,
			//	eventtype = e.eventtype,
			//	timestamp = e.timestamp,
			//	vendorid = e.vendorid,
			//	value = e.value,
			//	wpn = e.wpn,
			//	unit = e.unit
			//}
			//).ToList();

			//var eventList = new CustomerEventList { EventList = events };
			var eventList = new CustomerEventList();
			return await Task.FromResult(eventList);
		}

		public static async Task<RO<string>> CardNew(CardData cardNew)
		{
			var ro = new RO<string> { ReturnValue = string.Empty, ReturnCode = 0, Message = string.Empty };

			var cards = _db.GetCollection<DBCard>("cards");
			var clients = _db.GetCollection<DBClient>("clients");

			var card = new DBCard { RFID = cardNew.RFID };
			cards.Insert(card);

			var client = clients.FindById(int.Parse(cardNew.CustomerKey));
			client.Cards.Add(card);
			clients.Update(client);

			return await Task.FromResult(ro);
		}

		public static async Task<RO<string>> CardReplace(ReplacementCardData replacementCards)
		{
			var ro = new RO<string> { ReturnValue = string.Empty, ReturnCode = 0, Message = string.Empty };

			var cards = _db.GetCollection<DBCard>("cards");
			var clients = _db.GetCollection<DBClient>("clients");

			var card = new DBCard { RFID = replacementCards.RFID};
			cards.Insert(card);

			var client = clients.FindById(int.Parse(replacementCards.CustomerKey));
			client.Cards.Add(card);
			clients.Update(client);

			card = cards.FindOne(c => c.RFID.Equals(replacementCards.ReplacementRFID));
			card.Blocked = true;
			card.Deleted = true;
			cards.Update(card);

			card = client.Cards.Find(c => c.RFID == replacementCards.ReplacementRFID);
			client.Cards.Remove(card);
			clients.Update(client);

			return await Task.FromResult(ro);
		}

		public static async Task<RO<string>> CardStatusChanged(CardData changedCard, bool blocked, bool deleted)
		{
			var ro = new RO<string> { ReturnValue = string.Empty, ReturnCode = 0, Message = string.Empty };

			var cards = _db.GetCollection<DBCard>("cards");

			var card = cards.FindOne(c => c.RFID.Equals(changedCard.RFID));

			// In production - if a card has status D = deleted it should NOT be possible to reactivate it......

			card.Blocked = blocked;
			card.Deleted = deleted;
			cards.Update(card);

			return await Task.FromResult(ro);
		}

		public static async Task<RO<CardList>> CardList(CurrentClientData currentClient)
		{
			var ro = new RO<CardList> { ReturnCode = 0, Message = string.Empty };

			_db.GetCollection<DBCard>("cards");
			var clientList = _db.GetCollection<DBClient>("clients").Include(x => x.Cards);

			IEnumerable<DBClient> clients = null;
			DBClient client = null;

			clients = currentClient.InstallationID != "NA" ?
				clientList.Find(a => a.InstallationID.Equals(currentClient.InstallationID) && a.Id.Equals(int.Parse(currentClient.CustomerKey))) :
				clientList.Find(a => a.Id.Equals(int.Parse(currentClient.CustomerKey)));

			if (clients != null)
				client = clients.FirstOrDefault();

			if (client != null)
			{
				var cardsList = client.Cards.Where(c => c.Deleted).Select(c => new BossIDWS.Vendor.REST.ReturnObjects.Card
				{
					rfid = c.RFID,
					status = "D"
				}).Concat(client.Cards.Where(c => c.Blocked && c.Deleted == false).Select(c => new BossIDWS.Vendor.REST.ReturnObjects.Card
				{
					rfid = c.RFID,
					status = "B"
				})).Concat(client.Cards.Where(c => c.Blocked == false && c.Deleted == false).Select(c => new BossIDWS.Vendor.REST.ReturnObjects.Card
				{
					rfid = c.RFID,
					status = "A"
				})).ToList();

				//var cards = new CardList { Cards = cardsList };
				var cards = new CardList();
				ro.ReturnValue = cards;
			}

			return await Task.FromResult(ro);
		}


		#region utils

		public static BossIDWS.Vendor.REST.ReturnObjects.AccessPoint GetRandomAccessPoint()
		{
			var accesspoints = _db.GetCollection<DBAccessPoint>("accesspoints");
			var allAccessPoints = accesspoints.FindAll().ToList();

			var r = new Random();
			var ap = allAccessPoints[r.Next(0, allAccessPoints.Count)];

			return new BossIDWS.Vendor.REST.ReturnObjects.AccessPoint
			{
				accesspointid = ap.accesspointid,
				name = ap.name,
				tag = ap.tag
			};
		}

		public static string GetRandomClient()
		{
			var clients = _db.GetCollection<DBClient>("clients");
			var allClients = clients.FindAll().ToList();

			var r = new Random();
			return allClients[r.Next(0, allClients.Count)].Id.ToString();
		}

		public static string GetRandomCard()
		{
			var cards = _db.GetCollection<DBCard>("cards");
			var allCards = cards.FindAll().ToList();

			var r = new Random();
			return allCards[r.Next(0, allCards.Count)].Id.ToString();
		}

		public static DateTime GetRandomEventDate()
		{
			// Generates a date during the last 14 days
			var r = new Random();
			int subtractDays = r.Next(0, 15) * -1;
			var date =
				DateTime.Now.AddDays(subtractDays)
					.AddHours(-r.Next(0, 24))
					.AddMinutes(-r.Next(0, 60))
					.AddSeconds(-r.Next(0, 60));

			return date;
		}

		#endregion utils
	}
}