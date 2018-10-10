#define SESAM

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;
//using LiteDB;
//using Card = BossIDWS.Vendor.REST.ReturnObjects.Card;

#pragma warning disable 1591

namespace BossIDWS.Vendor.REST.Data
{
	public class VendorInterface
	{
		#region CUSTOMER

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Add a new customer
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> AddClientNew(ClientNewData client)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
			try
			{
				VendorEngine engine = new VendorEngine();
                if (client.AccessPoints == null || client.AccessPoints.AccessPoint == null)
				{
					roc.ReturnCode = 1024;
					roc.Message = "AddClientNew failure - NULL accesspoints in parameter";
					return await Task.FromResult(roc);
				}
                // Keep Vendor separate from the REST api
                VendorAccessPointList saplist = new VendorAccessPointList();
				List<VendorAccessPoint> saps = new List<VendorAccessPoint>();

				foreach (AccessPointCustomer ap in client.AccessPoints.AccessPoint)
				{
                    VendorAccessPoint sap = new VendorAccessPoint();
					sap.accesspointid = ap.accesspointid;
					sap.role = ap.role;
					sap.size = ap.size;
					saps.Add(sap);
				}
				saplist.AccessPoints = saps.ToArray();
                VendorRO vendorro = engine.CustomerNew(client.InstallationID, client.CustomerType, client.CustomerID, client.CustomerGUID, client.CustomerName,
														client.StreetAddress, client.Description, client.RFID, saplist);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "AddClientNew failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Changes customer access points
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CustomerChangePoints(ClientChangePointsData client)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
			try
			{
				VendorEngine engine = new VendorEngine();
				if (client.AccessPoints == null || client.AccessPoints.AccessPoint == null)
				{
					roc.ReturnCode = 1024;
					roc.Message = "CustomerChangePoints failure - NULL accesspoints in parameter";
					return await Task.FromResult(roc);
				}
                // Keep Sesam separate from the REST api
                VendorAccessPointList saplist = new VendorAccessPointList();
				List<VendorAccessPoint> saps = new List<VendorAccessPoint>();

				foreach (AccessPointCustomer ap in client.AccessPoints.AccessPoint)
				{
                    VendorAccessPoint sap = new VendorAccessPoint();
					sap.accesspointid = ap.accesspointid;
					sap.role = ap.role;
					sap.size = ap.size;
					saps.Add(sap);
				}
				saplist.AccessPoints = saps.ToArray();
                VendorRO vendorro = engine.CustomerChangePoints(client.InstallationID, client.CustomerKey, saplist);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CustomerChangePoints failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="clientdata"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CustomerChangeProperties(ClientChangePropertiesData clientdata)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {

                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CustomerChangeProperties(clientdata.InstallationID, clientdata.Customerkey, clientdata.Customername, clientdata.Streetaddress, clientdata.Description);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
            {
                roc.ReturnCode = 1024;
                roc.Message = "CustomerChangeProperties failure - " + e.ToString();
            }
			return await Task.FromResult(roc);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<Client>> AddNewClient(HouseHoldClientData client)
		{
			var roc = new BossIDWS.Vendor.REST.ReturnObjects.RO<Client>(100, "Obsolete", new Client());
			return await Task.FromResult(roc);
		}

		/// <summary>
		/// Dactivate a customer
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CustomerDeactivate(CurrentClientData client)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {
                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CustomerDeactivate(client.InstallationID, client.CustomerKey);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
            {
                roc.ReturnCode = 1024;
                roc.Message = "CustomerDeactivate failure - " + e.ToString();
            }
			return await Task.FromResult(roc);
		}

		/// <summary>
		/// No current supported functionality in Sesam
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CustomerActivate(CurrentClientData client)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {

                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CustomerDeactivate(client.InstallationID, client.CustomerKey);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
            {
                roc.ReturnCode = 1024;
                roc.Message = "CustomerActivate failure - " + e.ToString();
            }
            return await Task.FromResult(roc);
		}

		/// <summary>
		/// Delete a customer
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CustomerDelete(CurrentClientData client)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
			try
			{
				VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CustomerDelete(client.InstallationID, client.CustomerKey);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CustomerDelete failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		#endregion

		#region CARD
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CardNew(CardData cards)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {
                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CardNew(cards.InstallationID, cards.CustomerKey, cards.RFID);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CardNew failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CardReplace(ReplacementCardData cards)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {
                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CardReplace(cards.InstallationID, cards.CustomerKey, cards.ReplacementRFID, cards.RFID);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CardReplace failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Activate one or more cards
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CardActivate(CardData cards)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {
                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CardActivate(cards.InstallationID, cards.CustomerKey, cards.RFID);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CardActivate failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Deactivate one or more cards
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CardDeactivate(CardData cards)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {
                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CardDeactivate(cards.InstallationID, cards.CustomerKey, cards.RFID);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CardDeactivate failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="cards"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<string>> CardDelete(CardData cards)
		{
            BossIDWS.Vendor.REST.ReturnObjects.RO<string> roc = new ReturnObjects.RO<string>();
            try
            {
                VendorEngine engine = new VendorEngine();
                VendorRO vendorro = engine.CardDelete(cards.InstallationID, cards.CustomerKey, cards.RFID);
                roc.ReturnCode = vendorro.ReturnCode;
                roc.ReturnValue = vendorro.ReturnValue;
                roc.Message = vendorro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CardDelete failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}
		
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<CardList>> CardList(CurrentClientData client)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<CardList> ror = new ReturnObjects.RO<CardList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List<VendorCard> cards = engine.CardList(client.InstallationID, client.CustomerKey);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToCardList(client.InstallationID, cards);
				ror.Message = string.Empty;
			}
            catch (VendorException se)
            {
                VendorRO ro = se.ReturObjekt;
                ror.ReturnCode = ro.ReturnCode;
                ror.Message = ro.Message;
            }
            catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "CardList failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}

		#endregion

		#region QUERIES
		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <param name="client"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>> CustomerAccessPoints(CurrentClientData client)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList> ror = new ReturnObjects.RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List<VendorCustomerPoint> points = engine.CustomerAccessPoints(client.InstallationID, client.CustomerKey);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToCustomerPointList(client.InstallationID, points);
				ror.Message = string.Empty;
			}
			catch (VendorException se)
			{
				VendorRO ro = se.ReturObjekt;
				ror.ReturnCode = ro.ReturnCode;
				ror.Message = ro.Message;
			}
			catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "CustomerAccessPoints failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// For this one Sesam returns an XML that is transfomed to the REST accesspoint object
		/// </summary>
		/// <param name="accesspointdata"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>> AccessPoints(AccessPointData accesspointdata)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList> ror = new ReturnObjects.RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List<VendorPoint> points = engine.AccessPoints(accesspointdata.InstallationID, accesspointdata.Type.ToString(), accesspointdata.AccessPoint);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToAccessPointList(accesspointdata.InstallationID, points);
				ror.Message = string.Empty;
				ror.Message = "OOPS -" + points.Count.ToString();
			}
			catch (VendorException se)
			{
                VendorRO ro = se.ReturObjekt;
                ror.ReturnCode = ro.ReturnCode;
                ror.Message = ro.Message;
            }
            catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "AccessPoints failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Not supported
		/// </summary>
		/// <param name="accesspointdata"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<AccessPointDetailList>> AccessPointDetails(AccessPointData accesspointdata)
		{
			var roc = new BossIDWS.Vendor.REST.ReturnObjects.RO<AccessPointDetailList>(100, "Not supported", new AccessPointDetailList());
			return await Task.FromResult(roc);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Get status of all Sesam access points (all root inlets) and tranform the status list to the REST equivalent
		/// </summary>
		/// <param name="installation"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<AccessPointStatusList>> AccessPointStatus(ClientData installation)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<AccessPointStatusList> ror = new ReturnObjects.RO<AccessPointStatusList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List<VendorAccessPointStatus> status = engine.AccessPointStatus(installation.InstallationID);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToStatusList(installation.InstallationID, status);
				ror.Message = string.Empty;
			}
            catch (VendorException se)
            {
                VendorRO ro = se.ReturObjekt;
                ror.ReturnCode = ro.ReturnCode;
                ror.Message = ro.Message;
            }
            catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "AccessPointStatus failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Return extended event units form Sesam and transform result to to the REST eqivalent
		/// </summary>
		/// <param name="installation"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<ExtendedEventUnitList>> GetExtendedEventUnits(ClientData installation)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<ExtendedEventUnitList> ror = new ReturnObjects.RO<ExtendedEventUnitList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List<VendorExtendedUnit> units = engine.ExtendedEventUnits(installation.InstallationID);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToExtendedEventUnitList(installation.InstallationID, units);
				ror.Message = string.Empty;
			}
            catch (VendorException se)
            {
                VendorRO ro = se.ReturObjekt;
                ror.ReturnCode = ro.ReturnCode;
                ror.Message = ro.Message;
            }
            catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "GetExtendedEventUnits failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}

		#endregion

		#region REPORTING
		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Return customer events from Sesam. Her we return "any" object to be able to support two kinds of access point list, either the normal list
		/// or the compressed one for "ALL".
		/// </summary>
		/// <param name="events"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<object>> CustomerEvents(CustomerEventsData events)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<object> roc = new ReturnObjects.RO<object>();
			try
			{
				VendorEngine engine = new VendorEngine();
				var anylist = engine.CustomerEvents(events.InstallationID, events.CustomerKey, events.FromDate, events.ToDate);
				roc.ReturnCode = 0;
				roc.ReturnValue = anylist;
				roc.Message = string.Empty;
			}
            catch (VendorException se)
            {
                VendorRO ro = se.ReturObjekt;
                roc.ReturnCode = ro.ReturnCode;
                roc.Message = ro.Message;
            }
            catch (Exception e)
			{
				roc.ReturnCode = 1024;
				roc.Message = "CustomerEvents failure - " + e.ToString();
			}
			return await Task.FromResult(roc);
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Get extended events from Sesam and transform the Sesam events to the REST equivalent
		/// </summary>
		/// <param name="events"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<ExtendedEventList>> GetExtendedEvents(ExtendedEventsData events)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<ExtendedEventList> ror = new ReturnObjects.RO<ExtendedEventList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List <VendorExtendedEvent> exevents = engine.ExtendedEvents(events.InstallationID, events.FromDate, events.ToDate);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToExtendedEventList(events.InstallationID, exevents);
				ror.Message = string.Empty;
			}
            catch (VendorException se)
            {
                VendorRO ro = se.ReturObjekt;
                ror.ReturnCode = ro.ReturnCode;
                ror.Message = ro.Message;
            }
            catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "GetExtendedEvent failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}
		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Poll Sesam for specific events and transform the Sesam events to the REST equivalent
		/// </summary>
		/// <param name="polldata"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public async Task<BossIDWS.Vendor.REST.ReturnObjects.RO<ExtendedEventList>> GetExtendedEventPoll(ExtendedEventPollData polldata)
		{
			BossIDWS.Vendor.REST.ReturnObjects.RO<ExtendedEventList> ror = new ReturnObjects.RO<ExtendedEventList>();
			try
			{
				VendorEngine engine = new VendorEngine();
				List<VendorExtendedEvent> exevents = engine.ExtendedEventPoll(polldata.InstallationID, polldata.FromDateTime, polldata.EventType);
				ror.ReturnCode = 0;
				ror.ReturnValue = TransformToExtendedEventList(polldata.InstallationID, exevents);
				ror.Message = string.Empty;
			}
            catch (VendorException se)
            {
                VendorRO ro = se.ReturObjekt;
                ror.ReturnCode = ro.ReturnCode;
                ror.Message = ro.Message;
            }
            catch (Exception e)
			{
				ror.ReturnCode = 1024;
				ror.Message = "GetExtendedEventPoll failure - " + e.ToString();
			}
			return await Task.FromResult(ror);
		}
		#endregion


		#region TRANSFORMERS
		//--------------------------------------------------------------------------------------------------------------------------------
		// Transform between the vendor internal notation of objects to REST objects
		//--------------------------------------------------------------------------------------------------------------------------------
		private CardList TransformToCardList(string installationid, List<VendorCard> cards)
		{
			CardList list = new CardList();
			List<Card> clist = new List<Card>();
			foreach (VendorCard sc in cards)
			{
                // In this example CardStatus=1 is active and zero is blocked
                Card c = new Card();
				c.rfid = sc.RFID;
				c.status = (sc.Deleted ? "D" : (sc.CardStatus == 1 ? "A" : "B"));
				clist.Add(c);
			}
			list.Cards = clist.ToArray(); ;
			return list;
		}


		private BossIDWS.Vendor.REST.ReturnObjects.AccessPointList TransformToCustomerPointList(string installationid, List<VendorCustomerPoint> points)
		{
			var list = new BossIDWS.Vendor.REST.ReturnObjects.AccessPointList();
			List<BossIDWS.Vendor.REST.ReturnObjects.AccessPoint> plist = new List<BossIDWS.Vendor.REST.ReturnObjects.AccessPoint>();
			foreach (VendorCustomerPoint sp in points)
			{
				BossIDWS.Vendor.REST.ReturnObjects.AccessPoint p = new BossIDWS.Vendor.REST.ReturnObjects.AccessPoint();
				p.installationid = installationid;
				p.accesspointid = sp.AccessPointId.ToString();
				p.parentid = string.Empty;
				p.category = "1";
				p.tag = string.Empty;
				p.size = sp.CustomerType == 3?"L":"N";
				p.capacity = string.Empty;
				p.name = sp.Name;
				p.description = sp.Description;
				p.role = (sp.RedundancyGroup == 1 ? "PR" : (sp.RedundancyGroup == 2 ? "S1" : "S2"));
				p.wpn = sp.Fraction.ToString();
				p.unit = "G";
				p.gps = string.Empty;
				p.decimaldegrees = string.Empty;
				p.zone = string.Empty;
				p.x = string.Empty;
				p.y = string.Empty;
				p.state = sp.Enabled?"A":"W";
				plist.Add(p);
			}
			list.AccessPoints = plist.ToArray();
			return list;
		}

		private BossIDWS.Vendor.REST.ReturnObjects.AccessPointList TransformToAccessPointList(string installationid, List<VendorPoint> points)
		{
			var list = new BossIDWS.Vendor.REST.ReturnObjects.AccessPointList();
			List<BossIDWS.Vendor.REST.ReturnObjects.AccessPoint> plist = new List<BossIDWS.Vendor.REST.ReturnObjects.AccessPoint>();
			foreach (VendorPoint sp in points)
			{
				BossIDWS.Vendor.REST.ReturnObjects.AccessPoint p = new BossIDWS.Vendor.REST.ReturnObjects.AccessPoint();
				p.installationid = installationid;
				p.accesspointid = sp.AccessPointId.ToString();
				p.parentid = (sp.ParentId.Equals(Guid.Empty) ? string.Empty : sp.ParentId.ToString());
				p.category = (sp.Type == 2 ? "2" : "1");
				p.tag = string.Empty;
				p.size = "B";
				p.capacity = string.Empty;
				p.name = sp.Name;
				p.description = sp.Description;
				p.role = "NA";
				p.wpn = sp.Fraction.ToString();
				p.unit = "G";
				p.gps = string.Empty;
				p.decimaldegrees = string.Empty;
				p.zone = string.Empty;
				p.x = string.Empty;
				p.y = string.Empty;
				p.state = (sp.Deleted ? "D" : "A");
				plist.Add(p);
			}
			list.AccessPoints = plist.ToArray();
			return list;
		}
		
    		
		private AccessPointStatusList TransformToStatusList(string installationid, List<VendorAccessPointStatus> status)
		{
			var statusList = new AccessPointStatusList();
			List<AccessPointStatus> eelist = new List<AccessPointStatus>();
			foreach (VendorAccessPointStatus see in status)
			{
				AccessPointStatus ee = new AccessPointStatus();
				ee.installationid = installationid;
				ee.accesspointid = see.AccessPointId.ToString();
				ee.state = (see.Enabled ? "A" : "U");
				ee.statuscode = (see.Enabled ? string.Empty : "9999");
				ee.statustext = (see.Enabled ? string.Empty : "Point out of order");
				eelist.Add(ee);
			}
			statusList.AccessPoints = eelist;
			return statusList;
		}


		private ExtendedEventList TransformToExtendedEventList(string installationid, List<VendorExtendedEvent> events)
		{
			var eventList = new ExtendedEventList();
			List<ExtendedEvent> eelist = new List<ExtendedEvent>();
			foreach (VendorExtendedEvent see in events)
			{
				ExtendedEvent ee = new ExtendedEvent();
				ee.timestamp = see.LogDate.ToString();
				ee.vendorid = string.Empty;
				ee.installationid = installationid;
				ee.unitid = see.UnitId.ToString();
				ee.idtype = see.IDType.ToString();
				ee.eventtype = see.EventType.ToString();
				ee.wpn = see.FractionId.ToString();
				eelist.Add(ee);
			}
			eventList.EventList = eelist.ToArray();
			return eventList;
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Convert list of extended units fetched from Sesam to object to be returned by REST
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="units"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		private ExtendedEventUnitList TransformToExtendedEventUnitList(string installationid, List<VendorExtendedUnit> units)
		{

			var eventList = new ExtendedEventUnitList();
			List<ExtendedEventUnit> eeu = new List<ExtendedEventUnit>();
			foreach (VendorExtendedUnit seu in units)
			{
				ExtendedEventUnit u = new ExtendedEventUnit();
				u.vendorid = string.Empty;
				u.unitid = seu.UnitId.ToString();
				u.installationid = installationid;
				u.category = seu.UnitType.ToString();
				u.isaccesspoint = (seu.IsAccessPoint == 0 ? "N" : "Y");
				u.name = seu.Name;
				u.description = string.IsNullOrEmpty(seu.Description) ? "NA" : seu.Description;
				u.tag = string.IsNullOrEmpty(seu.Tag) ? "NA" : seu.Tag;
				u.rfid = string.IsNullOrEmpty(seu.RFID) ? "NA" : seu.RFID;
				u.wpn = (seu.UnitType == 0 ? "NA" : seu.FractionId.ToString());
				u.status = (seu.Deleted ? "D" : "A");
				eeu.Add(u);
			}
			eventList.ExtendedUnitList = eeu.ToArray();
			return eventList;
		}
		#endregion
	}
}