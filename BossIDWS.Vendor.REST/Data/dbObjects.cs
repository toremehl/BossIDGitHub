using System;
using System.Collections.Generic;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;
using LiteDB;

namespace BossIDWS.Vendor.REST.Data
{
	public class Dbobjects
	{
		private readonly LiteDatabase _db;
		//private LiteDatabase _db;
		public Dbobjects()
		{
			var fullPath = System.Web.Hosting.HostingEnvironment.MapPath(@"~/Data/TestData.db");
			if (string.IsNullOrWhiteSpace(fullPath))
				fullPath = @"E:\src\BossIDWS.Vendor\BossIDWS.Vendor.REST\Data\TestData.db";

			_db = new LiteDatabase(fullPath);
			_db.Log.Level = Logger.FULL;

			BsonMapper.Global.Entity<DBClient>().DbRef(x => x.Cards, "cards");
			BsonMapper.Global.Entity<DBClient>().DbRef(x => x.PrimaryAccessPoints, "accesspoints");
			BsonMapper.Global.Entity<DBClient>().DbRef(x => x.Secondary1AccessPoints, "accesspoints");
			BsonMapper.Global.Entity<DBClient>().DbRef(x => x.Secondary2AccessPoints, "accesspoints");
			BsonMapper.Global.Entity<DBAccessPoint>().DbRef(x => x.Details, "accesspointDetails");
		}

		public LiteDatabase LiteDb => _db;
	}
	public class DBClient : CommercialClientData
	{
		public DBClient()
		{
			ClientType = "H";
			Blocked = false;
			Deleted = false;
			Cards = new List<DBCard>();
			PrimaryAccessPoints = new List<DBAccessPoint>();
			Secondary1AccessPoints = new List<DBAccessPoint>();
			Secondary2AccessPoints = new List<DBAccessPoint>();
		}

		[BsonId]
		public int Id { get; set; }
		public string ClientType { get; set; }
		public bool Blocked { get; set; }
		public bool Deleted { get; set; }

		public List<DBAccessPoint> PrimaryAccessPoints { get; set; }
		public List<DBAccessPoint> Secondary1AccessPoints { get; set; }
		public List<DBAccessPoint> Secondary2AccessPoints { get; set; }
		public List<DBCard> Cards { get; set; }
	}

	public class DBCard
	{
		public DBCard()
		{
			Blocked = false;
			Deleted = false;
			RFID = string.Empty;
		}

		[BsonId]
		public int Id { get; set; }
		public string RFID { get; set; }
		public bool Blocked { get; set; }
		public bool Deleted { get; set; }
	}

	public class DBAccessPoint : BossIDWS.Vendor.REST.ReturnObjects.AccessPoint
	{
		[BsonId]
		public int Id { get; set; }
		public AccessPointDetail Details { get; set; }
	}

	public class DBAccessPointDetail : AccessPointDetail
	{
		[BsonId]
		public int Id { get; set; }
	}

	public class DBEvents : CustomerEvent
	{
		[BsonId]
		public int Id { get; set; }

		public DateTime EventDate { get; set; }
	}
}