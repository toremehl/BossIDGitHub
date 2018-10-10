
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using BossIDWS.Vendor.REST.Data;
using BossIDWS.Vendor.REST.InputObjects;
using BossIDWS.Vendor.REST.ReturnObjects;

namespace BossIDWS.Vendor.REST.Controllers
{
	/// <summary>
	///     Methods related to extended event units
	/// </summary>
	[RoutePrefix("api/extended")]
	public class ExtendedEventController : ApiController
	{
        // GET: api/extended/units
        /// <summary>
        ///     Chapter 7.7.6 ExtendedEventUnits
        ///     The method shall return information about all extended event units in the vendor system. Which units
        ///     that are to conisdered as "extended" must be according to agreement between BossID and vendor.
        ///     The method shall return information about all extended event units available in the vendor system. 
        ///     The method will be used on a regular basis to keep BossID up to date.
        /// </summary>
        /// <param name="eventdata"></param>
        /// <returns>Return object with extended unit information</returns>
        [Route("units")]
		[HttpPost]
		public async Task<RO<ExtendedEventUnitList>> ExtendedEventUnits(ExtendedEventUnitData eventdata)
		{
			var ro = new RO<ExtendedEventUnitList>();

			if (ModelState.IsValid)
			{
                VendorInterface vendor = new VendorInterface();
				ro = await vendor.GetExtendedEventUnits(eventdata);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - ExtendedEventUnits - Incorrect parameters: {message}";
			}
			return ro;
		}
        
        // GET: api/extended/events
        /// <summary>
        ///     Chapter 7.8.3 ExtendedEvents
        ///     The method will be used to collect extended unit events for a specific installation for a specific period in time.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>Return object with extended unit events</returns>
        [Route("events")]
		[HttpPost]
		public async Task<RO<ExtendedEventList>> ExtendedEvents(ExtendedEventsData parameters)
		{
			var ro = new RO<ExtendedEventList>();

			if (ModelState.IsValid)
			{
                VendorInterface vendor = new VendorInterface();
				ro = await vendor.GetExtendedEvents(parameters);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - ExtendedEvents - Incorrect parameters: {message}";
			}
			return ro;
		}
        
        // GET: api/extended/eventpoll
        /// <summary>
        ///     Chapter 7.8.4 ExtendedEventPoll
        ///     The method will be used to poll for specific extended unit events - equal to or after the given point in time.
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns>XML with access point information</returns>
        [Route("eventpoll")]
		[HttpPost]
		public async Task<RO<ExtendedEventList>> ExtendedEventPoll(ExtendedEventPollData parameters)
		{
			var ro = new RO<ExtendedEventList>();

			if (ModelState.IsValid)
			{
                VendorInterface vendor = new VendorInterface();
				ro = await vendor.GetExtendedEventPoll(parameters);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - ExtendedEventPoll - Incorrect parameters: {message}";
			}
			return ro;
		}
	}
}
