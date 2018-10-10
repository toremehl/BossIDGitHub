using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BossIDWS.Vendor.REST.Data;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;


namespace BossIDWS.Vendor.REST.Controllers
{
	//--------------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Methods related to cards
	/// </summary>
	//--------------------------------------------------------------------------------------------------------------------------------
	[RoutePrefix("api/card")]
	public class CardController : ApiController
	{
		//--------------------------------------------------------------------------------------------------------------------------------
		// POST: api/card/new
		/// <summary>
		/// Chapter 7.6.1 CardNew
        /// One or more new RFID cards are issued to a specific customer.
		/// When the operation is complete, the customer may use all new cards on all access points assigned to the customer.
		/// </summary>
		/// <param name="newCard"></param>
		/// <returns>Return object with result of operation</returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		[Route("new")]
		[HttpPost]
		public async Task<RO<string>> CardNew([FromBody] CardData newCard)
		{
			var ro = new RO<string>();

			//#if SESAM
			//#else
			if (ModelState.IsValid)
			{
				//ro = await VendorDL.CardNew(newCard);
				VendorInterface vendor = new VendorInterface();
				ro = await vendor.CardNew(newCard);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CardNew - Incorrect parameters: {message}";
			}
			//#endif
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/card/replace
        /// <summary>
        ///     Chapter 7.6.2 CardReplace
        ///     The customer has misplaced or lost a card. A new card is issued and sent to the customer. The misplaced/lost card
        ///     is removed from the customer.
        ///     Note: In BossID, the requirement is that no card shall ever be reused. Once a card is removed/deleted, the card is
        ///     unavailable for future use by any customer.
        ///     Note: The vendor system must implement the necessary logic to keep the history of all deleted cards.
        ///     That is, a card is not actually “deleted” but marked as deleted/removed and unusable.
        ///     Operation: The card to be replaced is deactivated and removed from the customer. Adds the new card and binds it to
        ///     the customer.
        ///     When the operation is complete, the customer may use the new card on all access points assigned to the
        ///     customer
        /// </summary>
        /// <param name="cards"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("replace")]
		[HttpPost]
		public async Task<RO<string>> CardReplace([FromBody] ReplacementCardData cards)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				//ro = await VendorDL.CardReplace(cards);
				VendorInterface vendor = new VendorInterface();
				ro = await vendor.CardReplace(cards);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CardReplace - Incorrect parameters: {message}";
			}
			//#endif
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/card/activate
        /// <summary>
        ///     Chapter 7.6.4 CardActivate
        ///     The method activates all specified cards or all cards for a specified customer
        ///     The customer may use all activated card(s) for all assigned access points when the operation is complete-
        /// </summary>
        /// <param name="card"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("activate")]
		[HttpPost]
		public async Task<RO<string>> CardActivate([FromBody] CardData card)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				//	ro = await VendorDL.CardStatusChanged(card, false, false);
				VendorInterface vendor = new VendorInterface();
				ro = await vendor.CardActivate(card);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CardActivate - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/card/deactivate
        /// <summary>
        ///     Chapter 7.6.3 CardDeactivate
        ///     The method shall deactivate all specified RFID cards for the specified customer.
        ///     The customer cannot use any of these deactivated cards on any assigned access point when the operation is complete.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("deactivate")]
		[HttpPost]
		public async Task<RO<string>> CardDeactivate([FromBody] CardData card)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				//	ro = await VendorDL.CardStatusChanged(card, true, false);
				VendorInterface vendor = new VendorInterface();
				ro = await vendor.CardDeactivate(card);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CardDeactivate - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/card/delete
        /// <summary>
        ///     Chapter 7.6.5 CardDelete
        ///     The specified card(s) shall be deleted and removed from the specified customer.
        ///     Note: In BossID, the requirement is that no card shall ever be reused.Once a card is removed/deleted the card is
        ///     unavailable for future use by any customer.
        ///     Note: The vendor system must implement the necessary logic to keep the history of all deleted cards.
        ///     That is, a card is not actually “deleted” but marked as deleted/removed and unusable.
        ///     Operation: Deactivate and delete the specified card(s) for the specified customer.
        ///     Result: When the operation has completed, no one shall ever be able to use the specified card(s) on any access
        ///     point ever.
        /// </summary>
        /// <param name="card"></param>
        /// <returns>Return object with result of operation</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("delete")]
		[HttpPost]
		[HttpDelete]
		public async Task<RO<string>> CardDelete([FromBody] CardData card)
		{
			var ro = new RO<string>();
			if (ModelState.IsValid)
			{
				//	ro = await VendorDL.CardStatusChanged(card, true, true);
				VendorInterface vendor = new VendorInterface();
				ro = await vendor.CardDelete(card);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CardDelete - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // POST: api/card/list
        /// <summary>
        ///     Chapter 7.6.6 CardDelete
        ///     The method shall return all cards bound to a specific customer. The list shall include all active and deactivated
        ///     cards. In addition, the list shall include all cards that have been removed/deleted from the customer.
        /// </summary>
        /// <param name="client"></param>
        /// <returns>Return object with list of cards</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("list")]
		[HttpPost]
		public async Task<RO<CardList>> CardList([FromBody] CurrentClientData client)
		{
			var ro = new RO<CardList>();
			if (ModelState.IsValid)
			{
				// ro = await VendorDL.CardList(client);
				VendorInterface vendor = new VendorInterface();
				ro = await vendor.CardList(client);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

				ro.ReturnCode = 100;
				ro.Message = $"Bad request - CardList - Incorrect parameters: {message}";
			}
			return ro;
		}
	}
}