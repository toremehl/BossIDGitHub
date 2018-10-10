using System;
using System.Xml.Serialization;

// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot("customer")]
    public class BossIDCustomer
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
        public AccessPoint[] AccessPoints { get; set; }
    }
}