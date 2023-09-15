using EdiDbUploader.Uploader;
using FBH.EDI.Common;
using FBH.EDI.Common.Model;

namespace EdiDbUploader
{
    internal class EdiFactory
    {
        internal static EdiUploader GetUploader(EdiDocument doc)
        {
            if(doc.DocumentNo == EdiDocumentNo.Freight_Invoice_210)
            {
                return new EdiUploader210();
            }
            else if(doc.DocumentNo == EdiDocumentNo.Purchase_Order_850)
            {
                return new EdiUploader850();
            }
            else if (doc.DocumentNo == EdiDocumentNo.Invoice_810)
            {
                return new EdiUploader810();
            }
            else if (doc.DocumentNo == EdiDocumentNo.Warehouse_Shipping_Advice_945)
            {
                return new EdiUploader945();
            }
            else if (doc.DocumentNo == EdiDocumentNo.Warehouse_Shipping_Order_940)
            {
                return new EdiUploader940();
            }
            else if (doc.DocumentNo == EdiDocumentNo.Warehouse_Stock_Transfer_Receipt_Advice_944)
            {
                return new EdiUploader944();
            }
            else
            {
                return null;
            }
        }
    }
}