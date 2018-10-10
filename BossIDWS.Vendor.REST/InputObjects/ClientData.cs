using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// </summary>
	public class ClientData
	{
		/// <summary>
		/// 
		/// </summary>
		public ClientData()
		{
			InstallationID = "NA";
		}
		/// <summary>
		/// If no InstallationID is to be used, the argument will be set by caller to the value NA
		/// </summary>
		[Required(ErrorMessage = "If no InstallationID is to be used, the argument should set by caller to the value \"NA\"")]
		public string InstallationID { get; set; }
	}
}