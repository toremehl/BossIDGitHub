using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace BossIDWS.Vendor.REST.InputObjects
{

	/// <summary>
	/// 
	/// </summary>
	//[Serializable]
	public class AccessPointCustomer
	{
		/// <summary>
		/// 
		/// </summary>
		public string accesspointid { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string size { get; set; }
		/// <summary>
		/// 
		/// </summary>
		public string role { get; set; }
	}
	/// <summary>
	/// 
	/// </summary>
	//[Serializable]
	//[XmlRoot("accesspointlist")]
	public class AccessPointListCustomer
	{
		/// <summary>
		/// 
		/// </summary>
		//[XmlElement("accesspoint")]
		public AccessPointCustomer[] AccessPoint { get; set; }

	}
}