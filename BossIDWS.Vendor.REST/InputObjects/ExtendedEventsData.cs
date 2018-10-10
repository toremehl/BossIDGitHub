using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// 2018.04.13 - TJM - Added for V43
	/// </summary>
	public class ExtendedEventsData : ClientData
	{
		/// <summary>
		/// 
		/// </summary>
		[Required]
		public string FromDate { get; set; }
		/// <summary>
		/// 
		/// </summary>
		[Required]
		public string ToDate { get; set; }
	}
}