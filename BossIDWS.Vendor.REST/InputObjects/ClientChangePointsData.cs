using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// 
	/// </summary>
	public class ClientChangePointsData : ClientData
	{
		/// <summary>
		/// The customer key/id in the vendor system
		/// </summary>
		[Required]
		public string CustomerKey { get; set; }
		/// <summary>
		/// Either an XML with access points to be allocated to the customer, HTML encoded, or a list semicolon separated list of access points
		/// Using a semicolon separated list may only be in collaboration with BossID and where the vendor only supports primary inlets.
		/// 
		/// The parameter will contain all access points that the customer will be allocated to after invokation. The vendor must thereby implement this method as a “replace all access points”.
		/// </summary>
		[Required]
		public AccessPointListCustomer AccessPoints { get; set; }
	}
}