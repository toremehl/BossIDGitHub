using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects
#pragma warning disable IDE1006 // Naming Styles

namespace BossIDWS.Vendor.REST.ReturnObjects
{
	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	[XmlRoot("customer")]
	public class Client
	{
		/// <summary>
		/// 
		/// </summary>
		public string installationid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string customerkey { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[XmlElement("accesspointlist")]
		public List<AccessPoint> AccessPoints { get; set; }
	}
}