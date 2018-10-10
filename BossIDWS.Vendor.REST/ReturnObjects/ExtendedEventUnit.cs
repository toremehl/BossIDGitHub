using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects
#pragma warning disable IDE1006 // Naming Styles
namespace BossIDWS.Vendor.REST.ReturnObjects
{
	/// <summary>
	/// List of extended units
	/// </summary>
	[Serializable]
	public class ExtendedEventUnit
	{
		/// <summary>
		/// 
		/// </summary>
		public string vendorid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string installationid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string unitid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string category { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string isaccesspoint { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string name { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string description { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string tag { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string rfid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string wpn { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string status { get; set; }
	}
	/// <summary>
	/// 
	/// </summary>

	[Serializable]
	[XmlRoot("extendedeventunits")]
	public class ExtendedEventUnitList
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("unit")]
		public ExtendedEventUnit[] ExtendedUnitList { get; set; }
	}

}