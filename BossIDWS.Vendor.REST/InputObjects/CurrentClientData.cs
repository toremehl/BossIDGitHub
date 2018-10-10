using System.ComponentModel.DataAnnotations;


namespace BossIDWS.Vendor.REST.InputObjects
{
    /// <summary>
    /// 
    /// </summary>
    public class CurrentClientData : ClientData
    {
		/// <summary>
		/// 
		/// </summary>
		[Required]
		public string CustomerKey { get; set; }
    }
}