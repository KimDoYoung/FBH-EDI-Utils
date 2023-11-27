using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public enum EdiDocumentNo
    {
        Unknown = 0,
        Freight_Invoice_210 = 210,
        Invoice_810 = 810,
        Inventory_Inquiry_Advice_846 = 846,
        Purchase_Order_850 = 850,
        Warehouse_Shipping_Order_940 = 940,
        Warehouse_Stock_Transfer_Receipt_Advice_944 = 944,
        Warehouse_Shipping_Advice_945 = 945,
        WALMART_810_PAYMENT = 999,
        Delivery_Appointment = 998,
        Aging_Origin = 1000,
        Payment_820 = 1001,
        Hub_System_Data = 1002,
    }
}
