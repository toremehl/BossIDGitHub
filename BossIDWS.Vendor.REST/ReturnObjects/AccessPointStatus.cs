using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
	/// <summary>
	/// Status of accesspoint
	/// </summary>
	[Serializable]
	public class AccessPointStatus
	{
		/// <summary>
		/// The installation ID
		/// </summary>
		public string installationid { get; set; }
		/// <summary>
		/// Access point id in vendor system
		/// </summary>
		public string accesspointid { get; set; }
		/// <summary>
		/// State of this point, A=Available, W=Warning, U=Unavailable
		/// </summary>
		public string state { get; set; }

		/// <summary>
		/// An integer identifying the actual status - an error or warning code. Value of zero shall always indicate that a point is fully operable. Any other values will be vendor specific and will be used as reference.
		/// </summary>
		public string statuscode { get; set; }

		/// <summary>
		/// A text or message following the status code. Only when status code other than zero.
		/// </summary>
		public string statustext { get; set; }
	}

	/// <summary>
	/// List of access point status
	/// </summary>
	[Serializable]
	[XmlRoot("accesspointstatus")]
	public class AccessPointStatusList
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("accesspoint")]
		public List<AccessPointStatus> AccessPoints { get; set; }
	}
}