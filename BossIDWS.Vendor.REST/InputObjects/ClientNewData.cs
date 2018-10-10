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
	public class ClientNewData : ClientData
	{
		/// <summary>
		/// Required - Valid values are H=Household, C=Commercial
		/// </summary>
		[Required]
		public string CustomerType { get; set; }
		/// <summary>
		/// Required - The CRM Customer ID
		/// </summary>
		[Required]
		public string CustomerID { get; set; }
		/// <summary>
		/// Optional - The CRM Customer GUID
		/// </summary>
		[Required]
		public string CustomerGUID { get; set; }
		/// <summary>
		/// Required - The BossID Property Unit for household customers or company name for commercial customers
		/// </summary>
		[Required]
		public string CustomerName { get; set; }
		/// <summary>
		/// Optional - For reference only. Street name and number for household customers, otherwise “NA”. May be used for organizing household customers in the vendor system
		/// </summary>
		[Required]
		public string StreetAddress { get; set; }
		/// <summary>
		/// Optional - Additional information about the customer. For reference only
		/// </summary>
		[Required]
		public string Description { get; set; }
		/// <summary>
		/// Required - RFID cards. Semicolon separated list. Each RFID will be a valid RFID interpretation as required by the vendor system.
		/// At least one card will be specified.
		/// </summary>
		[Required]
		public string RFID { get; set; }
		/// <summary>
		/// Required - Either an XML with access points to be allocated to the customer, HTML encoded, or a list semicolon separated list of access points
		/// 
		/// Using a semicolon separated list may only be in collaboration with BossID and where the vendor only supports primary inlets.
		/// </summary>
		[Required]
		//public string AccessPoints { get; set; }
		public AccessPointListCustomer AccessPoints { get; set; }
	}
}