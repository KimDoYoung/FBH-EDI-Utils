using FBH.EDI.Common;
using FBH.EDI.Common.Model;
using System;
using System.Data.Common;

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
            else if (doc.DocumentNo == EdiDocumentNo.Invoice_810)
            {
                return new EdiUploader810();
            }
            else
            {
                return null;
            }


        }
    }
}