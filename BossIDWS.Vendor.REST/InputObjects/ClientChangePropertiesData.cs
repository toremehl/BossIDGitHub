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
	public class ClientChangePropertiesData : ClientData
	{
		/// <summary>
		/// The customer key/id in the vendor system
		/// </summary>
		[Required]
		public string Customerkey { get; set; }
		/// <summary>
		/// Customer name. EMPTY if no change
		/// </summary>
		[Required]
		public string Customername { get; set; }
		/// <summary>
		/// Street name and number. EMPTY if no change
		/// </summary>
		[Required]
		public string Streetaddress { get; set; }
		/// <summary>
		/// Additional information about the customer. EMPTY if no change
		/// </summary>
		[Required]
		public string Description { get; set; }
	}
}