using System;
// ReSharper disable InconsistentNaming - naming style needed for easier parsing to BossID XML objects

namespace BossIDWS.Vendor.REST.ReturnObjects
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class Card
    {
        /// <summary>
        /// The RFID of this card
        /// </summary>
        public string rfid { get; set; }
		/// <summary>
		/// Card status. A=The card is active, B=The card is blocked, D=The card is deleted
		/// </summary>
		public string status { get; set; }
    }
}