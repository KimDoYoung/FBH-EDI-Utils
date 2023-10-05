using Microsoft.Office.Interop.Excel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class DeliveryAppointments : EdiDocument
    {
        public String BolNo { get; set; }
        public String PoNumber { get; set; }
        public String PoNoOnly { get; set; }
        public String ActShipDt { get; set; }
        public String ReqDelivery { get; set; }
        public String ActDeliviery { get; set; }
        public String DeliveryAppt { get; set; }
        public String Comments { get; set; }

    }
}
