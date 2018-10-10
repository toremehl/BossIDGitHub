using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects
#pragma warning disable IDE1006 // Naming Styles
// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
	/// <summary>
	///
	/// </summary>
	[Serializable]
	public class ExtendedEvent
	{
		/// <summary>
		/// 
		/// </summary>
		public string timestamp { get; set; }

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
		public string idtype { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string eventtype { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string wpn { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	[XmlRoot("extendedevents")]
	public class ExtendedEventList
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("event")]
		public ExtendedEvent[] EventList { get; set; }
	}

}