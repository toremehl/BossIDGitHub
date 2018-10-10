using System;
using System.Collections.Generic;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
	/// <summary>
	/// 2018.04.13 - TJM - Updated for V43 - from GitHub
	/// </summary>
	[Serializable]
	public class AccessPointDetail
	{
		/// <summary>
		/// 
		/// </summary>
		public string installationid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string accesspointid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string accesspointguid { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string pointtype { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string filllevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string batterylevel { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string temperature { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string batterytype { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string voltage { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string volume { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string weight { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string length { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string width { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string height { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string serial { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string tag { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string barcode { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string warning { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string error { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string pictureurl { get; set; }

		/// <summary>
		/// 
		/// </summary>
		public string cameraurl { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	[XmlRoot("accesspointdetaillist")]
	public class AccessPointDetailList
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("accesspointdetail")]
		public List<AccessPointDetail> AccessPoints { get; set; }
	}
}