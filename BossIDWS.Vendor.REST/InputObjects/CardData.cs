// ReSharper disable ConvertToAutoProperty
using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// </summary>
	public class CardData : ClientData
	{

		/// <summary>
		/// Customer key/id in the vendor system
		/// </summary>
		[Required]
		public string CustomerKey { get; set; }
		/// <summary>
		/// RFID card(s). Semicolon separated list. At least one RFID must be accepted.
		/// </summary>
		[Required]
		public string RFID { get; set; }
	}
}