namespace BossIDWS.Vendor.REST.InputObjects
{
	/// <summary>
	/// 2018.04.13 - TJM - Added for V43 - from GitHub
	/// </summary>
	public class MovedClientData : CurrentClientData
	{
		/// <summary>
		/// 
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string PropertyUnit { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string StreetAddress { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Description { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Primary { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Secondary1 { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string Secondary2 { get; set; }
	}
}