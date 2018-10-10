using System.ComponentModel.DataAnnotations;

namespace BossIDWS.Vendor.REST.InputObjects
{
    /// <summary>
    /// </summary>
    public class AccessPointOutOfOrderData : AccessPointData
    {
        /// <summary>
        /// </summary>
        [Required(ErrorMessage = "Message describing the discrepancy/anomaly")]
        public string Message { get; set; }
    }
}