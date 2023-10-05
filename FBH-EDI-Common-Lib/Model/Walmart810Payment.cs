using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Walmart810Payment : EdiDocument
    {
        public String PoNumber { get; set; }
        public String InvoiceNo { get; set; }
        public String DcNo { get; set; }
        public String StoreNo { get; set; }
        public String Division { get; set; }
        public String MicrofilmNo { get; set; }
        public String InvoiceDt { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public String DatePaid { get; set; }
        public decimal? DiscountUsd { get; set; }
        public decimal? AmountPaidUsd { get; set; }
        public String DeductionCode { get; set; }

    }
}
