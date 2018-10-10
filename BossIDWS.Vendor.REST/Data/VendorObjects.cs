using System;
using System.Xml.Serialization;

namespace BossIDWS.Vendor.REST.Data
{
    public class VendorCard
    {
        public string RFID { get; set; }
        public int CardStatus { get; set; }
        public bool Deleted { get; set; }
    }

    [Serializable]
    public class VendorAccessPoint
    {
        public string accesspointid { get; set; }
        public string size { get; set; }
        public string role { get; set; }
    }
    [Serializable]
    [XmlRoot("accesspointlist")]
    public class VendorAccessPointList
    {
        [XmlElement("accesspoint")]
        public VendorAccessPoint[] AccessPoints { get; set; }
    }

    public class VendorExtendedUnit
    {
        public Guid UnitId { get; set; }
        public int UnitType { get; set; }
        public int IsAccessPoint { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Tag { get; set; }
        public string RFID { get; set; }
        public int FractionId { get; set; }
        public bool Deleted { get; set; }
        public bool Enabled { get; set; }
    }

    public class VendorExtendedEvent
    {
        public DateTime LogDate { get; set; }
        public Guid UnitId { get; set; }
        public int IDType { get; set; }
        public int EventType { get; set; }
        public int FractionId { get; set; }

    }
    public class VendorCustomerPoint
    {
        public int CustomerType { get; set; }
        public Guid AccessPointId { get; set; }
        public int RedundancyGroup { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Fraction { get; set; }
        public bool Enabled { get; set; }
    }
    public class VendorPoint
    {
        public int InstallationID { get; set; }
        public Guid AccessPointId { get; set; }
        public Guid ParentId { get; set; }
        public int Type { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public bool Deleted { get; set; }
        public int Fraction { get; set; }
        public bool Enabled { get; set; }
        public int Status { get; set; }
    }
    public class VendorAccessPointStatus
    { 
        public Guid AccessPointId { get; set; }
        public bool Enabled { get; set; }

    }
    public class VendorException : Exception
    {
        protected int _returkode = 1024;        // Default returkode

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// GET - Returobjekt basert på unntaket. Hvis InnerException tas denne med som ToString()
        /// </summary>
        //-------------------------------------------------------------------------------------------------------------------
        public VendorRO ReturObjekt
        {
            get
            {
                if (InnerException != null) return new VendorRO(_returkode, Message + " - " + InnerException.ToString());
                else return new VendorRO(_returkode, Message);
            }
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Standard constructor - Sett standard systemfeil-melding
        /// </summary>
        //-------------------------------------------------------------------------------------------------------------------
        public VendorException() : base("Systemfeil. Kontakt BossID ansvarlig")
        {
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor med returmelding - feilkode blir 1024
        /// </summary>
        /// <param name="returmelding">Returmelding</param>
        //-------------------------------------------------------------------------------------------------------------------
        public VendorException(string returmelding) : base(returmelding)
        {
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor med returmelding og inner Exception - feilkode blir 1024
        /// </summary>
        /// <param name="returmelding"></param>
        /// <param name="inner"></param>
        //-------------------------------------------------------------------------------------------------------------------
        public VendorException(string returmelding, Exception inner) : base(returmelding, inner)
        {
        }
        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor med returkode og returmelding 
        /// </summary>
        /// <param name="returkode"></param>
        /// <param name="returmelding"></param>
        //-------------------------------------------------------------------------------------------------------------------
        public VendorException(int returkode, string returmelding) : base(returmelding)
        {
            _returkode = returkode;
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Constructor med returkode, returmelding og InnerException
        /// </summary>
        /// <param name="returkode"></param>
        /// <param name="returmelding"></param>
        /// <param name="inner"></param>
        //-------------------------------------------------------------------------------------------------------------------
        public VendorException(int returkode, string returmelding, Exception inner) : base(returmelding, inner)
        {
            _returkode = returkode;
        }
    }
}