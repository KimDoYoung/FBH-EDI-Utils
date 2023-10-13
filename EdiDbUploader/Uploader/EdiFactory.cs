using System;
using EdiDbUploader.Uploader;
using FBH.EDI.Common;
using FBH.EDI.Common.Model;

namespace EdiDbUploader
{
    internal class EdiFactory
    {
         internal static EdiUploader GetUploader(EdiDocumentNo ediDocumentNumber)
        {
            EdiUploader uploader = null;
            switch (ediDocumentNumber)
            {
                case EdiDocumentNo.Unknown:
                    throw new EdiException("Unknown document type");
                case EdiDocumentNo.Freight_Invoice_210:
                    uploader = new EdiUploader210();
                    break;
                case EdiDocumentNo.Invoice_810:
                    uploader = new EdiUploader810();
                    break;
                case EdiDocumentNo.Inventory_Inquiry_Advice_846:
                    uploader = new EdiUploader846();
                    break;
                case EdiDocumentNo.Purchase_Order_850:
                    uploader = new EdiUploader850();
                    break;
                case EdiDocumentNo.Warehouse_Shipping_Order_940:
                    uploader = new EdiUploader940();
                    break;
                case EdiDocumentNo.Warehouse_Stock_Transfer_Receipt_Advice_944:
                    uploader = new EdiUploader944();
                    break;
                case EdiDocumentNo.Warehouse_Shipping_Advice_945:
                    uploader = new EdiUploader945();
                    break;
                case EdiDocumentNo.WALMART_810_PAYMENT:
                    uploader = new EdiUploaderWalmart810Payment();
                    break;
                case EdiDocumentNo.Delivery_Appointment:
                    uploader = new EdiUploaderDeliveryAppointments();
                    break;
                case EdiDocumentNo.Aging_Origin:
                    uploader = new EdiUploaderAgingOrigin();
                    break;
                default:
                    break;
            }
            return uploader;
        }
    }
}