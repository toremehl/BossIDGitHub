using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BossIDWS.Vendor.REST.Data;

namespace BossIDWS.Vendor.REST.Controllers
{
    public class AdminController : ApiController
    {
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		[HttpGet]
		public IHttpActionResult InitializeDB()
		{
			var data = new Dbobjects();
			var db = data.LiteDb;

			var clients = db.GetCollection<DBClient>("clients");
			var clientList = clients.FindAll();

			//db.DropCollection("clients");
			//db.DropCollection("cards");
			//db.DropCollection("accesspoints");
			//db.DropCollection("accesspointDetails");
			db.DropCollection("customerEvents");

			//var accesspointsList = db.GetCollection<DBAccessPoint>("accesspoints");
			//var accesspointDetailsList = db.GetCollection<DBAccessPointDetail>("accesspointDetails");

			//for (var i = 1; i < 21; i++)
			//{
			//    var accesspoint = new DBAccessPoint()
			//    {
			//        accesspointguid = Guid.NewGuid().ToString(),
			//        installationid = "0",
			//        accesspointid = i.ToString(),
			//        name = $"AccessPoint {i}"
			//    };

			//    accesspointsList.Insert(accesspoint);

			//    var accesspointDetail = new DBAccessPointDetail()
			//    {
			//        accesspointguid = accesspoint.accesspointguid,
			//        installationid = "0",
			//        accesspointid = accesspoint.accesspointid,
			//        serial = Guid.NewGuid().ToString()
			//    };

			//    accesspointDetailsList.Insert(accesspointDetail);
			//}

			//// Get card collection
			//var cards = db.GetCollection<DBCard>("cards");

			//for (int i = 1; i < 201; i++)
			//{
			//    var card = new DBCard()
			//    {
			//        RFID = GetRandomHexNumber(14)
			//    };

			//    cards.Insert(card);
			//}

			//var cards = db.GetCollection<DBCard>("cards");

			//var cardsAvaileable = cards.FindAll().Select(x => x.RFID).ToArray();

			var events = db.GetCollection<DBEvents>("customerEvents");


			for (var i = 1; i < 1001; i++)
			{
				var customerEvent = new DBEvents()
				{
					installationid = "0",
					accesspointid = VendorDL.GetRandomAccessPoint().accesspointid,
					customerkey = VendorDL.GetRandomClient(),
					eventtype = new Random().Next(0, 4).ToString(),
					rfid = VendorDL.GetRandomCard(),
					EventDate = VendorDL.GetRandomEventDate(),
					unit = "kilo",
					value = new Random().Next(1, 100).ToString(),
					vendorid = "0",
					size = "N",
					wpn = "Rest"
				};

				events.Insert(customerEvent);
			}


			return Ok();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="digits"></param>
		/// <returns></returns>
		private static string GetRandomHexNumber(int digits)
		{
			var _random = new Random();
			byte[] buffer = new byte[digits / 2];
			_random.NextBytes(buffer);
			string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
			if (digits % 2 == 0)
				return result;
			return result + _random.Next(16).ToString("X");
		}
	}
}
