using System;

namespace BossIDWS.Vendor.REST.InputObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class EventsData : ClientData
    {
        /// <summary>
        /// 
        /// </summary>
        public string Customerkey { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime FromDate { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime ToDate { get; set; }
    }
}