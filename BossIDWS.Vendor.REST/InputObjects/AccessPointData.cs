using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// </summary>
	public class AccessPointData : ClientData
	{
		/// <summary>
		/// 
		/// </summary>
		public enum AccessPointType
		{
			/// <summary>
			/// Access point ID in the vendor system
			/// </summary>
			ID = 1,
			/// <summary>
			/// The tag of the access point
			/// </summary>
			TAG = 2,
			/// <summary>
			/// Name or address of access point
			/// </summary>
			NAME = 3,
			/// <summary>
			/// All access points. The AccessPoint parameter is empty
			/// </summary>
			ALL = 4
		}

		/// <summary>
		/// </summary>
		[Range(1, 5, ErrorMessage = "Allowed values are ID|TAG|NAME|ALL")]
		[Required]
		public AccessPointType Type { get; set; }

		/// <summary>
		/// Value according to AccessPointType
		/// </summary>
		public string AccessPoint { get; set; }
	}
}