using System;
using System.Xml.Serialization;

namespace BossIDWS.Vendor.REST.ReturnObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    [XmlRoot("cardlist")]
    public class CardList
    {
        /// <summary>
        /// List of cards
        /// </summary>
        [XmlElement("card")]
        public Card[] Cards { get; set; }
    }
}