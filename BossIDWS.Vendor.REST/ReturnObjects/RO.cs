namespace BossIDWS.Vendor.REST.ReturnObjects
{
    /// <summary>
    ///  Return object of T
    /// </summary>
    public class RO<T>
    {
        /// <summary>
        ///     RO
        /// </summary>
        /// <param name="returnCode"></param>
        /// <param name="message"></param>
        /// <param name="returnValue"></param>
        public RO(int returnCode, string message, T returnValue)
        {
            ReturnCode = returnCode;
            Message = message;
            ReturnValue = returnValue;
        }

        /// <summary>
        /// 
        /// </summary>
        public RO()
        {
        }

        /// <summary>
        ///     ReturnCode
        /// </summary>
        public int ReturnCode { get; set; }

        /// <summary>
        ///     Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        ///     ReturnValue
        /// </summary>
        public T ReturnValue { get; set; }
    }
}