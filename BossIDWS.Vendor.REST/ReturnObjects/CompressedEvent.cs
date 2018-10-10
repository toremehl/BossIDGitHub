using System;
using System.Xml.Serialization;
// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
    //-------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// Compressed list of client events
    /// </summary>
    //-------------------------------------------------------------------------------------------------------------------
    [Serializable]
    //-------------------------------------------------------------------------------------------------------------------
    public class CompressedEvent
    {
        /// <summary>
        /// 
        /// </summary>
        public string t { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string vi { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string i { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string et { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string c { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string a { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string s { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string r { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string w { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string u { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string v { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot("customerevents")]
    public class CompressedEventList
    {
        [XmlElement("e")]
        public CompressedEvent[] EventList { get; set; }
    }
}