using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
    /// <summary>
	/// Naming is exactly equal to the nameing in the BossID REST interface
    /// </summary>
    public class ReplacementCardData : ClientData
    {
		/// <summary>
		/// Customer key/id in the vendor system
		/// </summary>
		[Required]
		public string CustomerKey { get; set; }
		/// <summary>
		/// RFID cards that are to be replaced, semicolon separated
		/// </summary>
		[Required]
		public string ReplacementRFID { get; set; }
		/// <summary>
		/// New RFID cards, semicolon separated
		/// </summary>
		public string RFID { get; set; }
    }
}