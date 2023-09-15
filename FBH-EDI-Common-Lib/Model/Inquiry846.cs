using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Inquiry846 : EdiDocument
    {
        public string HubGroupDocumentNumber { get; set; }
        public string DateExpresses { get; set; }
        public string DateTimeQualifier { get; set; }
        public string Date { get; set; }
        public string WarehouseName { get; set; }
        public string WarehouseId { get; set; }
        public string AddressInformation { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }

        public List<Inquiry846Detail> Details = null;
        public Inquiry846()
        {
            DocumentNo = EdiDocumentNo.Inventory_Inquiry_Advice_846;
            Details = new List<Inquiry846Detail>();
        }
        override public string ToString()
        {
            string s = $"HubGroupDocumentNumber : {HubGroupDocumentNumber}\r\n "
                + $"DateExpresses : {DateExpresses}\r\n "
                + $"DateTimeQualifier : {DateTimeQualifier}\r\n "
                + $"Date : {Date}\r\n "
                + $"WarehouseName : {WarehouseName}\r\n "
                + $"WarehouseId : {WarehouseId}\r\n "
                + $"AddressInformation : {AddressInformation}\r\n "
                + $"City : {City}\r\n "
                + $"State : {State}\r\n "
                + $"Zipcode : {Zipcode}\r\n";
            s += "--------------Details----------\r\n";
            foreach (var detail in Details)
            {
                s += detail.ToString() + "\r\n";
            }
            return s;
        }
    }
}
