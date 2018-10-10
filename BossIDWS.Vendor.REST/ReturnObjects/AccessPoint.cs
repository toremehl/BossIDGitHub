using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects
#pragma warning disable IDE1006 // Naming Styles
namespace BossIDWS.Vendor.REST.ReturnObjects
{
	/// <summary>
	/// List of accesspoints
	/// 2018.04.13 - TJM - Updated for V43 - from GitHub
	/// </summary>
	[Serializable]
	public class AccessPoint
	{
		/// <summary>
		/// The installation ID. Set to “NA” if not appropriate.
		/// </summary>
		public string installationid { get; set; }

		/// <summary>
		/// Access point id in vendor system
		/// </summary>
		public string accesspointid { get; set; }

		/// <summary>
		/// The id of the parent access point in an access point hierarchy. Empty if no parent.
		/// </summary>
		public string parentid { get; set; }
		//	public string accesspointguid { get; set; }

		/// <summary>
		/// The type of this point (category)
		/// </summary>
		public string category { get; set; }

		/// <summary>
		/// When customer request – the actual size allocated to the customer. When synchronization – The actual size types may be allocated to a customer.
		/// </summary>
		public string size { get; set; }

		/// <summary>
		/// Estimated household usage where appropriate. For synchronization only
		/// </summary>
		public string capacity { get; set; }
		/// <summary>
		/// The name of the access point
		/// </summary>
		public string name { get; set; }

		/// <summary>
		/// The «tag» belonging to the access point
		/// </summary>
		public string tag { get; set; }

		/// <summary>
		/// A description of the point, if any
		/// </summary>
		public string description { get; set; }

		/// <summary>
		/// The role or type of this access point. PR=Primary inlet, S1 = First alternative inlet or S2 = Secondary alternative inlet.
		/// The role shall have a value of «NA» if the access point list is not associated with a customer related request
		/// </summary>
		public string role { get; set; }

		/// <summary>
		/// Waste product number in vendor system
		/// </summary>
		public string wpn { get; set; }
		/// <summary>
		/// Measurement unit for the point. For synchronization only
		/// </summary>
		/// 
		public string unit { get; set; }
		/// <summary>
		/// GPS position of point, e.g. “6025.532N00518.864E”
		/// </summary>
		
		public string gps { get; set; }
		/// <summary>
		/// The gps position in decimal degrees, e.g. “60.3890;5.3187”. Note: Maximum eigth decimals
		/// </summary>
		public string decimaldegrees { get; set; }

		/// <summary>
		/// UTM zone. UTM zone may be empty if zone 32V. Otherwise the zone must be specified
		/// </summary>
		public string zone { get; set; }

		/// <summary>
		/// UTM x-value
		/// </summary>
		public string x { get; set; }

		/// <summary>
		/// UTM y-value
		/// </summary>
		public string y { get; set; }

		/// <summary>
		/// State of this point, A=Available, W=Warning, U=Unavailable. When synchronizing D=Point is deleted
		/// </summary>
		public string state { get; set; }
	}

	/// <summary>
	/// List of access points
	/// </summary>
	[Serializable]
	[XmlRoot("accesspointlist")]
	public class AccessPointList
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("accesspoint")]
		public AccessPoint[] AccessPoints { get; set; }

	}
}
