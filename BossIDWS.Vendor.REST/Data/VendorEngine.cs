using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using BossIDWS.Vendor.REST.ReturnObjects;

namespace BossIDWS.Vendor.REST.Data
{
    //--------------------------------------------------------------------------------------------------------------------------------
    /// <summary>
    /// VendorEngine implements a dummy-vendor implementing the REST interface according to the BossID Requirement Specification
    /// 
    /// The "dummy" code below returns valid values for a vendor using GUIDs for both customer identification, accesspoints and
    /// extended event units
    /// </summary>
    //--------------------------------------------------------------------------------------------------------------------------------

    public class VendorEngine
	{
        #region DATA
            
        #endregion


        #region CUSTOMER_METHODS
        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.1
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customertype"></param>
        /// <param name="customerid"></param>
        /// <param name="customerguid"></param>
        /// <param name="customername"></param>
        /// <param name="streetaddress"></param>
        /// <param name="description"></param>
        /// <param name="rfid"></param>
        /// <param name="acplist"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerNew(string installationid, string customertype, string customerid, string customerguid, string customername, string streetaddress, string description,
											string rfid, VendorAccessPointList acplist)
		{
            // Replace with your code - in example a GUID is return as customer ID at vendor site
            return new VendorRO(0, "Customer created", new Guid().ToString());
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.2 Deprecated
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerid"></param>
        /// <param name="customerguid"></param>
        /// <param name="propertyunit"></param>
        /// <param name="streetaddress"></param>
        /// <param name="description"></param>
        /// <param name="rfid"></param>
        /// <param name="primary"></param>
        /// <param name="secondary1"></param>
        /// <param name="secondary2"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerNewHousehold(string installationid, string customerid, string customerguid, string propertyunit, string streetaddress, string description,
											string rfid, string primary, string secondary1, string secondary2)
		{
			return new VendorRO(60, "CustomerNewHousehold - Deprecated", string.Empty);
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.3 Deprecated
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerid"></param>
        /// <param name="customerguid"></param>
        /// <param name="customername"></param>
        /// <param name="businessunit"></param>
        /// <param name="propertyunit"></param>
        /// <param name="streetaddress"></param>
        /// <param name="description"></param>
        /// <param name="rfid"></param>
        /// <param name="inletsize"></param>
        /// <param name="primary"></param>
        /// <param name="secondary1"></param>
        /// <param name="secondary2"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerNewCommercial(string installationid, string customerid, string customerguid, string customername, string businessunit, string propertyunit, string streetaddress, string description,
											string rfid, string inletsize, string primary, string secondary1, string secondary2)
		{
			return new VendorRO(60, "CustomerNewCommercial - Deprecated", string.Empty);
		}

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.4 Deprecated 
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <param name="name"></param>
        /// <param name="propertyunit"></param>
        /// <param name="streetaddress"></param>
        /// <param name="description"></param>
        /// <param name="primary"></param>
        /// <param name="secondary1"></param>
        /// <param name="secondary2"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerMove(string installationid, string customerkey, string name, string propertyunit, string streetaddress, string description, string primary, string secondary1, string secondary2)
		{
			return new VendorRO(60, "CustomerMove - Deprecated", string.Empty);
		}
        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.5
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerDeactivate(string installationid, string customerkey)
		{
            // Replace with your code
            return new VendorRO(0, "Customer deactivated");
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.6
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerActivate(string installationid, string customerkey)
		{
            // Replace with your code
            return new VendorRO(0, "Customer activated");
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.7 
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerDelete(string installationid, string customerkey)
		{
            // Replace with your code
             return  new VendorRO(0, "Customer deleted");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.5.8
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="customerkey"></param>
		/// <param name="acplist"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public VendorRO CustomerChangePoints(string installationid, string customerkey, VendorAccessPointList acplist)
		{
            // Replace with your code
            return new VendorRO(0, "Customer points changed");
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.5.9
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <param name="customername"></param>
        /// <param name="streetaddress"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CustomerChangeProperties(string installationid, string customerkey, string customername, string streetaddress, string description)
		{
            // Replace with your code
            return new VendorRO(0, "Customer properties changed");
        }
        #endregion

        #region CARD_METHODS
        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.6.1 
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <param name="rfid"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO CardNew(string installationid, string customerkey, string rfid)
		{
            // Replace with your code
            return new VendorRO(0, "Card(s) added");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.6.2 
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="customerkey"></param>
		/// <param name="rfidnew"></param>
		/// <param name="rfidreplace"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public VendorRO CardReplace(string installationid, string customerkey, string rfidnew, string rfidreplace)
		{
            // Replace with your code
            return new VendorRO(0, "Card(s) replaced");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.6.3
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="customerkey"></param>
		/// <param name="rfid"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public VendorRO CardDeactivate(string installationid, string customerkey, string rfid)
		{
            // Replace with your code
            return new VendorRO(0, "Card(s) deactivated");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.6.4 
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="customerkey"></param>
		/// <param name="rfid"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public VendorRO CardActivate(string installationid, string customerkey, string rfid)
		{
            // Replace with your code
            return new VendorRO(0, "Card(s) activated");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.6.5
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="customerkey"></param>
		/// <param name="rfid"></param>
		/// <returns></returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public VendorRO CardDelete(string installationid, string customerkey, string rfid)
		{
            // Replace with your code
            return new VendorRO(0, "Card(s) deleted");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.6.6
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="customerkey"></param>
		/// <returns>List of customer cards</returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public List<VendorCard> CardList(string installationid, string customerkey)
		{
            // Replace with your code
            return new List<VendorCard>();

            // Throw VendorException if error
        }
        #endregion

        #region QUERY_METHODS
        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.7.2
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <returns>List of customer access points</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public List<VendorCustomerPoint> CustomerAccessPoints(string installationid, string customerkey)
		{
            // Replace with your code
            return new List<VendorCustomerPoint>();

            // Throw VendorException if error
        }

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.7.3
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="type"></param>
        /// <param name="accesspoint"></param>
        /// <returns>List of access points</returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public List<VendorPoint> AccessPoints(string installationid, string type, string accesspoint)
		{
            // Replace with your code
            return new List<VendorPoint>();

            // Throw VendorException if error

        }

        //--------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.7.4
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="type"></param>
        /// <param name="accesspoint"></param>
        /// <returns></returns>
        //--------------------------------------------------------------------------------------------------------------------------------
        public VendorRO AccessPointDetails(string installationid, string type, string accesspoint)
		{
			return new VendorRO(60, "AccessPointDetails not available");
		}

		//--------------------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.7.5
		/// </summary>
		/// <param name="installationid"></param>
		/// <returns>List of access point status information</returns>
		//--------------------------------------------------------------------------------------------------------------------------------
		public List<VendorAccessPointStatus> AccessPointStatus(string installationid)
		{
            // Replace with your code
            return new List<VendorAccessPointStatus>();

            // Throw VendorException if error

        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.7.6
        /// </summary>
        /// <param name="installationid"></param>
        /// <returns>List of extended event units</returns>
        //-------------------------------------------------------------------------------------------------------------------
        public List<VendorExtendedUnit> ExtendedEventUnits(string installationid)
		{
            // Replace with your code
            return new List<VendorExtendedUnit>();

            // Throw VendorException if error
        }
        #endregion

        #region REPORTING_METHODS
        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.8.2
        /// </summary>
        /// <param name="installationid">Installasjons id</param>
        /// <param name="customerkey">Kundenøkkel</param>
        /// <param name="fromdate">Fra dato</param>
        /// <param name="todate">Til dato</param>
        /// <returns>List of customer events</returns>
        //-------------------------------------------------------------------------------------------------------------------
        public object CustomerEvents(string installationid, string customerkey, string fromdate, string todate)
		{
            // Replace with your code
            if (customerkey.Equals("ALL"))
                return CustomerEventsDaily(installationid, fromdate, todate);
            else
                return CustomerEventsCustomer(installationid, customerkey, fromdate, todate);
		}
        /// <summary>
        /// 7.8.2.CUSTOMER
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="customerkey"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public object CustomerEventsCustomer(string installationid, string customerkey, string fromdate, string todate)
        {
            // Replace with your code
            CustomerEventList events = new CustomerEventList();
            List<CustomerEvent> eventlist = new List<CustomerEvent>();

            //foreach (VendorEvent ve in events)
            //{
            //    eventlist.Add(ve);
            //}

            events.EventList = eventlist.ToArray();
            return events;
        }

        /// <summary>
        /// 7.8.2.CUSTOMER.ALL
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns></returns>
        public object CustomerEventsDaily(string installationid, string fromdate, string todate)
        {
            // Replace with your code
            CompressedEventList compressedevents = new CompressedEventList();
            List<CompressedEvent> eventlist = new List<CompressedEvent>();

            //foreach (VendorEvent ve in events)
            //{
            //    eventlist.Add(ve);
            //}

            compressedevents.EventList = eventlist.ToArray();
            return compressedevents;
        }

        //-------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// 7.8.3
        /// </summary>
        /// <param name="installationid"></param>
        /// <param name="fromdate"></param>
        /// <param name="todate"></param>
        /// <returns>Liste of extended events</returns>
        //-------------------------------------------------------------------------------------------------------------------
        public List<VendorExtendedEvent> ExtendedEvents(string installationid, string fromdate, string todate)
		{
            // Replace with your code
            return new List<VendorExtendedEvent>();
        }

		//-------------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// 7.8.4
		/// </summary>
		/// <param name="installationid"></param>
		/// <param name="fromdatetime"></param>
		/// <param name="eventtype"></param>
		/// <returns>List of polled events</returns>
		//-------------------------------------------------------------------------------------------------------------------
		public List<VendorExtendedEvent> ExtendedEventPoll(string installationid, string fromdatetime, string eventtype)
		{
            // Return dummy
            return new List<VendorExtendedEvent>();
        }
        #endregion

        #region ACTION_METHODS
        /// <summary>
        /// 7.9.2
        /// </summary>
        /// <param name="installationid"></param>
        /// <returns></returns>
        public VendorRO AccessPointOutOfOrder(string installationid)
		{
			return new VendorRO(60, "AccessPointOutOfOrder not implemented");
		}
		#endregion
	}
}
