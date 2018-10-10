#define SESAM

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
	///     Methods related to accesspoints
	/// </summary>
	//--------------------------------------------------------------------------------------------------------------------------------
	[RoutePrefix("api/accesspoint")]
    public class AccessPointController : ApiController
    {
        //--------------------------------------------------------------------------------------------------------------------------------
        // GET: api/accesspoint/
        /// <summary>
        ///     Chapter 7.7.3 AccessPoints
        ///     Get vendor access points. The method shall return information about one single access point or all access points in the vendor system.
        ///     The method will be used on a regular basis to keep BossID up to date.
        /// </summary>
        /// <param name="accessPoint"></param>
        /// <returns>Return object with access point information</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("")]
		[HttpPost]
		public async Task<RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>> AccessPoints(AccessPointData accessPoint)
		{
			var ro = new RO<BossIDWS.Vendor.REST.ReturnObjects.AccessPointList>();
			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.AccessPoints(accessPoint);
				#else
					var accesspoints = await VendorDL.AccessPoints(accessPoint);
					ro.ReturnCode = 0;
					ro.ReturnValue = accesspoints;
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
				ro.ReturnCode = 100;
				ro.Message = $"Bad request - AccessPoints - Incorrect parameters: {message}";
			}
			return ro;
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        // GET: api/accesspoint/details
        /// <summary>
        ///     Chapter 7.7.3 AccessPointDetails
        ///     The method shall return detailed information about one single access point or all access points in the vendor
        ///     system.
        /// </summary>
        /// <param name="accessPoint"></param>
        /// <returns>Return object with access point detail</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        [Route("details")]
		[HttpPost]
		public async Task<RO<AccessPointDetailList>> AccessPointDetails(AccessPointData accessPoint)
		{
			var ro = new RO<AccessPointDetailList>();

			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.AccessPointDetails(accessPoint);
				#else
					var accesspoints = await VendorDL.AccessPointDetails(accessPoint);
					ro.ReturnCode = 0;
					ro.ReturnValue = accesspoints;
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

				ro.ReturnCode = 100;
				ro.Message = $"Bad request - AccessPointDetails - Incorrect parameters: {message}";
			}
			return ro;
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		// GET: api/accesspoint/status
		/// <summary>
		///     Chapter 7.7.5 AccessPointStatus
        ///     The method shall return status information about all access points in the vendor system.
		///     The method is a “short form” of AccessPoint and shall return only status of each point.
		///     The method will be invoked periodically, from a controlled environment, for displaying real-time access point
		///     status on a map.
		/// </summary>
		/// <param name="clientData"></param>
		/// <returns>Return object with access point status</returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		[Route("status")]
		[HttpPost]
		public async Task<RO<AccessPointStatusList>> AccessPointStatus([FromBody] ClientData clientData)
		{
			var ro = new RO<AccessPointStatusList>();

			if (ModelState.IsValid)
			{
				#if SESAM
					VendorInterface vendor = new VendorInterface();
					ro = await vendor.AccessPointStatus(clientData);
				#else
					var accesspoints = await VendorDL.AccessPointStatus(clientData.InstallationID);
					ro.ReturnCode = 0;
					ro.ReturnValue = accesspoints;
				#endif
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

				ro.ReturnCode = 100;
				ro.Message = $"Bad request - AccessPointStatus - Incorrect parameters: {message}";
			}

			return ro;
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		// POST: api/accesspoint/outoforder
		/// <summary>
		///     Chapter 7.9.2 The customer call center reports that an access point is out of order.
		///     The intention of this method is to report to the vendor all discrepancies/anomalies regarding any access point
		/// </summary>
		/// <param name="accessPoint"></param>
		/// <returns>Return object with reuslt of operation</returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		[Route("outoforder")]
		[HttpPost]
		public async Task<RO<string>> AccessPointOutOfOrder(AccessPointOutOfOrderData accessPoint)
		{
			var ro = new RO<string>();

			if (ModelState.IsValid)
			{
				ro = await VendorDL.AccessPointOutOfOrder(accessPoint);
			}
			else
			{
				var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));

				ro.ReturnCode = 60;
				ro.Message = $"Bad request - AccessPointOutOfOrder - Incorrect parameters: {message}";
			}
			return ro;
		}
	}
}