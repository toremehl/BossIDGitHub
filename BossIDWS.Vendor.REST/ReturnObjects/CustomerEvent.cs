using System;
using System.Collections.Generic;
using System.Xml.Serialization;
// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class CustomerEvent
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
        public string eventtype { get; set; }


        /// <summary>
        /// 
        /// </summary>
        public string customerkey { get; set; }

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
        public string rfid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string wpn { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string unit { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string value { get; set; }
	}

	/// <summary>
	/// 
	/// </summary>
	[Serializable]
	[XmlRoot("customerevents")]
	public class CustomerEventList
	{
		/// <summary>
		/// 
		/// </summary>
		[XmlElement("event")]
		public CustomerEvent[] EventList { get; set; }
	}
}