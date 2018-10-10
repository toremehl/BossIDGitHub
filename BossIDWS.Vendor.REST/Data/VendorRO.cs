using System;

namespace BossIDWS.Vendor.REST.Data
{
	[Serializable]
	public class VendorRO
	{
        public int ReturnCode = 0;						// ReturnCode/feilkode
		public string Message = string.Empty;			// Eventuell feilMessage
        public string ReturnValue = string.Empty;       // Eventuel ReturnValue

        /// <summary>
        /// 
        /// </summary>
        public VendorRO()
		{
			ReturnCode = 0;
			Message = string.Empty;
			ReturnValue = string.Empty;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Message"></param>
		public VendorRO(int code, string Message)
        {
			ReturnCode = code;
			Message = (Message != null ? Message : string.Empty);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="code"></param>
        /// <param name="Message"></param>
        /// <param name="ReturnValue"></param>
		public VendorRO(int code, string Message, string ReturnValue)
		{
			ReturnCode = code;
			Message = (Message != null ? Message : string.Empty); 
			ReturnValue = (ReturnValue != null ? ReturnValue : string.Empty);
		}

		//-------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// </summary>
		/// <param name="ReturnValue">ReturnValue</param>
		//-------------------------------------------------------------------------------------------------------------------
		public VendorRO(string ReturnValue)
		{
			ReturnCode = 0;
			ReturnValue = (ReturnValue != null ? ReturnValue : string.Empty);
			Message = string.Empty;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ro1"></param>
        /// <param name="ro2"></param>
		public VendorRO(VendorRO ro1, VendorRO ro2)
		{
			this.Clone(ro1, ro2);
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ro"></param>
		public void Clone(VendorRO ro)
		{
			ReturnCode = ro.ReturnCode;
			ReturnValue = ro.ReturnValue;
			Message = ro.Message;
		}
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ro1"></param>
        /// <param name="ro2"></param>
        public void Clone(VendorRO ro1, VendorRO ro2)
		{
			if (ro1.ReturnCode == 0 && ro2.ReturnCode == 0)
			{
				this.Clone(ro1);
				ReturnValue += ro2.ReturnValue;
			}
			else
			{
				if (ro1.ReturnCode > 0) this.Clone(ro1);
				else this.Clone(ro2);
			}
		}

		//-------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 
		/// </summary>
		/// <returns></returns>
		//-------------------------------------------------------------------------------------------------------------------
		public string[] ToArray()
		{
			return new string[] { ReturnCode.ToString(), Message, ReturnValue };
		}
    }
}