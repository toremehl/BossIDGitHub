#define SESAM
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BossIDWS.Vendor.REST.Data;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;

namespace BossIDWS.Vendor.REST.Controllers
{
	/// <summary>
	///     Methods related to clients
	/// </summary>
	[RoutePrefix("api/client")]
	public class ClientController : ApiController
	{
        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/new
        /// <summary>
        ///     Chapter 7.5.1 CustomerNew
        ///     New customers are added to the vendor system using the CustomerNew method, including one or more RFID cards.
        ///     Typical operation: Add the customer to the vendor system, bind the RFID-card(s) to the customer, bind/allocate access points to the customer.
        ///     When the operation is successful, the customer will be able to use the given card(s) on all allocated access points.
        ///     The CustomerNew is a combined method for the deprecated CustomerNewHousehold and CustomerNewCommercial and with removal of some parameters.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Return object with customer key in the vendor system</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("new")]
		[HttpPost]
		public async Task<RO<string>> CustomerNew([FromBody] ClientNewData client)
		{
			var ro = new RO<string>();

			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.AddClientNew(client);
				#else
					var result = await VendorDL.AddClientNew(client);
					if (result != null)
					{
						ro.ReturnCode = 0;
						ro.ReturnValue = result;
					}
					else
					{
						ro.ReturnCode = 100;
					}
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerNew - Incorrect parameters: {message}";
				//ro.Message = client.ToString();
			}
			return ro;
		}
        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/changepoints
        /// <summary>
        /// Chapter 7.5.8 CustomerChangePoints
        /// The method will be used to change access points for a customer. A change may be: Add one or more new points,
        /// replace all points, remove one or more points, remove all points
        /// The accesspoint listform BossID will contain all access points that the customer will be allocated to after invokation. The vendor must thereby implement this method as a “replace all access points”.
        /// Note: If all accesspoints are removed from a customer, the customer shall still exist in the vendor system with assigned RFID-cards, but with no accesspoints.
        /// </summary>
        /// <param name="clientdata"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("changepoints")]
		[HttpPost]
		public async Task<RO<string>> CustomerChangePoints([FromBody] ClientChangePointsData clientdata)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerChangePoints(clientdata);
				#else
					var result = await VendorDL.CustomerChangePoints(clientdata);
					if (result != null)
					{
						ro.ReturnCode = 0;
						ro.ReturnValue = result;
					}
					else
					{
						ro.ReturnCode = 100;
					}
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerChangePoints - Incorrect parameters: {message}";
			}
			return ro;
		}
        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/changeproperties
        /// <summary>
        ///     Chapter 7.5.9 CustomerChangeProperties 
        ///     Occasionally, there may be a change in cadastral unit, customer name, etc., where it is necessary to change the properties of a customer
        ///     but preserving the cards and access points associated with the customer. The vendor must implement the method, but may choose to “do nothing”. However, a name
        ///     change should always be performed to keep the vendor system consistent with BossID.
        /// </summary>
        /// <param name="clientdata"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("changeproperties")]
		[HttpPost]
		public async Task<RO<string>> CustomerChangeProperties([FromBody] ClientChangePropertiesData clientdata)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerChangeProperties(clientdata);
				#else
					var result = await VendorDL.CustomerChangeProperties(clientdata);
					if (result != null)
					{
						ro.ReturnCode = 0;
						ro.ReturnValue = result;
					}
					else
					{
						ro.ReturnCode = 100;
					}
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerChangeProperties - Incorrect parameters: {message}";
			}
			return ro;
		}
        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/household/new
        /// <summary>
        ///     Chapter 7.5.2 CustomerNewHousehold
        ///     OBSOLETE - Use api/client/new instead  (CustomerNew).
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Return object with customer key in the vendor system</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("household/new")]
		[HttpPost]
		public async Task<RO<Client>> CustomerNewHousehold([FromBody] HouseHoldClientData client)
		{
			var ro = new RO<Client>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.AddNewClient(client);
				#else
					var result = await VendorDL.AddNewClient(client);
					if (result != null)
					{
						ro.ReturnCode = 0;
						ro.ReturnValue = result;
					}
					else
					{
						ro.ReturnCode = -1;
					}
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = -100;
				ro.Message = $"Bad request - CustomerNewHousehold - Incorrect parameters: {message}";
			}
			return ro;
		}
        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/new/commercial
        /// <summary>
        ///     Chapter 7.5.3 CustomerNewCommercial
        ///     OBSOLETE - Use api/client/new instead  (CustomerNew).
        /// </summary>
        /// <param name="commercialClient"></param>
        /// <returns>Return object with customer key in the vendor system</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("commercial/new")]
		[HttpPost]
		public async Task<RO<Client>> CustomerNewCommercial([FromBody] CommercialClientData commercialClient)
		{
			var ro = new RO<Client>(Constants.NotImplementedReturnCode, Constants.NotImplementedMessage,
				 new Client());
			return await Task.FromResult(ro);
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/move
        /// <summary>
        ///     Chapter 7.5.4 CustomerMove
        ///     OBSOLETE - Use api/client/changepoints (CustomerChangePoints) or api/client/changeproperties instead (CustomerChangeProperties)
        /// </summary>
        /// <param name="movedClient"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("move")]
		[HttpPost]
		public async Task<RO<Client>> CustomerMove([FromBody] MovedClientData movedClient)
		{
			var ro = new RO<Client>();

			var result = await VendorDL.CustomerMove(movedClient);

			if (result != null)
			{
				ro.ReturnCode = 0;
				ro.ReturnValue = result;
			}
			else
			{
				ro.ReturnCode = 100;
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/deactivate
        /// <summary>
        ///     Chapter 7.5.5 CustomerDeactivate  
        ///     The service will deactivate all cards belonging to the specified customer.
        ///     Deactivates all cards for the specified customer such that the customer cannot use the bound cards at any access point.
        /// </summary>
        /// <param name="currentClient"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("deactivate")]
		[HttpPost]
		public async Task<RO<string>> CustomerDeactivate([FromBody] CurrentClientData currentClient)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerDeactivate(currentClient);
				#else
					var returnCode = await VendorDL.CustomerStatusChanged(true, false, currentClient.CustomerKey);
					var ro = new RO<string> { ReturnCode = returnCode };
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerDeactivate - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/activate
        /// <summary>
        ///     Chapter 7.5.6 CustomerActivate
        ///     The service activates all card bound to a specific customer.
        ///     Activates all cards for a customer. When the operation is complete, the customer may use all cards for all assigned access points.
        /// </summary>
        /// <param name="currentClient"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("activate")]
		[HttpPost]
		public async Task<RO<string>> CustomerActivate([FromBody] CurrentClientData currentClient)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerActivate(currentClient);
				#else
					var returnCode = await VendorDL.CustomerStatusChanged(false, false, currentClient.CustomerKey);
					var ro = new RO<string> { ReturnCode = returnCode };
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerActivate - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/client/delete
        /// <summary>
        ///     Chapter 7.5.7 CustomerDelete
        ///     The service “deletes” the specified customer and all bound cards.
        ///     Note: With delete means that both the customer and all the bound cards are marked as deleted.
        ///     The vendor system must implement such deletion mechanism to preserve both customer, card and event history.
        ///     Note: In BossID, the requirement is that no card shall ever be reused. Once a card is removed/deleted the card is
        ///     unavailable for future use by any customer.
        ///     Note: For household customers, this method will be used when e.g. an existing property is split into two or more
        ///     separate sections.
        ///     Operation Deletes all cards for a specific customer
        ///     Deactivates all access points for a specific customer
        ///     Deletes all other customer information.
        ///     Result: When the operation has completed, no one shall ever be able to use the cards on any access point ever.
        /// </summary>
        /// <param name="currentClient"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("delete")]
		[HttpPost]
		public async Task<RO<string>> CustomerDelete([FromBody] CurrentClientData currentClient)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerDelete(currentClient);
				#else
					var returnCode = await VendorDL.CustomerStatusChanged(true, true, currentClient.CustomerKey);
					var ro = new RO<string> { ReturnCode = returnCode };
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerDelete - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // GET: api/client/accesspoints
        /// <summary>
        ///     Chapter 7.7.2 CustomerAccessPoints
        ///     The method shall return information and status of all access points allocated to a specific client.
        /// </summary>
        /// <param name="currentClient"></param>
        /// <returns>Return object with customer assigned accesspoints</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("accesspoints")]
		[HttpPost]
		public async Task<RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>> CustomerAccessPoints([FromBody] CurrentClientData currentClient)
		{
			var ro = new RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerAccessPoints(currentClient);
				#else
					var detailsList = await VendorDL.CustomerAccessPoints(currentClient.CustomerKey);
					var ro = new RO<AccessPointDetailList>
					{
						ReturnCode = 0,
						ReturnValue = detailsList
					};
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CustomerAccessPoints - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // GET: api/client/events
        /// <summary>
        ///     Chapter 7.8.2 CustomerEvents
        ///     The method will be used to collect access point events for a specific customer or all customers for a specific
        ///     period in time.
        ///     Note: For a single customer query with no dates specified, the method should return the last 25 real-time
        ///     events, if any
        ///     This method will be used, on demand by BIR call center, to collect the latest access point events for a specific
        ///     customer.
        ///     Note: “All customers” option will only be used nightly to collect daily activity data.
        ///     Note: “All customers” option shall return a compressed XML as defined in section 7.4.7.3.
        ///     Note: A customer event is a successful operation, e.g. a successful waste disposal.
        ///     The method shall have three options:
        ///     * The last 25 events in real-time. Both FromDate and ToDate are empty.
        ///     * All events from a specific date for one customer
        ///     * All events from and including date to and including date for one customer or all
        ///     Operation: Collect access point events for a customer or all events for all customers for a specific period in
        ///     time.
        /// </summary>
        /// <param name="events"></param>
        /// <returns>Return object with customer events - compressed if the customer key is set to ALL</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("events")]
		[HttpPost]
		public async Task<RO<object>> CustomerEvents([FromBody] CustomerEventsData events)
		{
			// if events.Client.Customerkey = "ALL" - return all clients - all events
			var ro = new RO<object>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.CustomerEvents(events);
				#else
					var eventsList = await VendorDL.GetCustomerEvents(events);
					var ro = new RO<CustomerEventList>
					{
						ReturnCode = 0,
						ReturnValue = eventsList
					};
				return ro;
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = "Bad request - CustomerEvents - Incorrect parameters:" + " - " + events.CustomerKey + "/" + events.FromDate + "/" + events.ToDate;
			}
			return ro;
		}
	}
}