using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    class AgingOrigin : EdiDocument
    {
        public String BillToCustomer { get; set; }
        public String BillToSite { get; set; }
        public String CustomerAccountNumber { get; set; }
        public String TransactionNumber { get; set; }
        public String TransactionDate { get; set; }
        public String TransactionType { get; set; }
        public String DueDate { get; set; }
        public String AgingDays { get; set; }
        public String DpdBucket { get; set; }
        public String CurrentAmount { get; set; }
        public String OriginalAmount { get; set; }
        public String LineHaul { get; set; }
        public String Fuel { get; set; }
        public String Discount { get; set; }
        public String Accessorial_XXX { get; set; }
        public String ReceiptNumber { get; set; }
        public String ShipDate { get; set; }
        public String Origin { get; set; }
        public String Destination { get; set; }
        public String PoNumber { get; set; }
        public String SalesOrderNumber { get; set; }
        public String ShippingReference { get; set; }
        public String SourceSystemInvoiceNumber { get; set; }
        public String Ref1Ref2Ref3 { get; set; }
        public String InvoiceNotes { get; set; }
        public String ManifestId { get; set; }
        public String ShipReference { get; set; }
        public String Shipped { get; set; }
        public String UnitNoEquipContainerSizeSeal { get; set; }
        public String WeightClassCommodityPieces { get; set; }
        public String ErpEdPegasus { get; set; }
        public String CustomerEmailId { get; set; }
    }
}
