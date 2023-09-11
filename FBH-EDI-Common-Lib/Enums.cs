using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public enum EdiDocumentType
    {
        Unknown = 0,
        Freight_Invoice_210 = 210,
        Invoice_810 = 810,
        Inventory_Inquiry_Advice = 846,
        Purchase_Order_850 = 850,
        Warehouse_Shipping_Order_940 = 940,
        Warehouse_Stock_Transfer_Receipt_Advice_944 = 944,
        Warehouse_Shipping_Advice_945 = 945,
    }
}
