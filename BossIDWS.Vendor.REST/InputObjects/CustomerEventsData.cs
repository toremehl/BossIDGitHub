using System;
using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// 
	/// </summary>
	// 2018.04.13 - TJM - Changed DateTime to string
	public class CustomerEventsData : ClientData
	{
        /// <summary>
        /// The customer key/id in the vendor system
        /// If “ALL”, the method shall return activity for all customers for the specified period
        /// </summary>
        [Required]
		public string CustomerKey { get; set; }

		/// <summary>
		/// From date in iso format: YYYY-MM-DD.
		/// </summary>
		//[Required] Cannot be - may be empty
		public string FromDate { get; set; }
		/// <summary>
		/// To date in iso format: YYYY-MM-DD.
		/// </summary>
		//[Required] Cannot be - may be empty
		public string ToDate { get; set; }
    }
}