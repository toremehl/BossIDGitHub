using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// 2018.04.13 - TJM - Added for V43
	/// </summary>
	public class ExtendedEventPollData : ClientData
	{
		/// <summary>
		/// 
		/// </summary>
		[Required]
		public string FromDateTime { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		public string EventType { get; set; }
	}
}