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
            if(doc.DocumentNo == EdiDocumentType.Freight_Invoice_210)
            {
                return new EdiUploader210();
            }
            else if(doc.DocumentNo == EdiDocumentType.Purchase_Order_850)
            {
                return new EdiUploader850();
            }
            else if (doc.DocumentNo == EdiDocumentType.Invoice_810)
            {
                return new EdiUploade810();
            }
            else if (doc.DocumentNo == EdiDocumentType.Invoice_810)
            {
                return new EdiUploade810();
            }
            else
            {
                return null;
            }


        }
    }
}