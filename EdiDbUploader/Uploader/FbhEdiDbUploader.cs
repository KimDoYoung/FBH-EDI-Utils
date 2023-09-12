using FBH.EDI.Common;
using FBH.EDI.Common.ExcelPdfUtils;
using FBH.EDI.Common.Model;
using System;
using System.Collections.Generic;

namespace EdiDbUploader
{
    /// <summary>
    /// 각 EDI 파일을 읽어서 DB에 저장한다.
    /// 
    /// </summary>
    internal class FbhEdiDbUploader
    {
        public static event EventHandler<MessageEventArgs> MessageEventHandler;
        private string connectString;

        public FbhEdiDbUploader(string connectString)
        {
            this.connectString = connectString;
        }

        internal void insert(string ediFile)
        {
            MessageEventHandler?.Invoke(null, new MessageEventArgs($"{ediFile} upload start "));
            if (ediFile.ToLower().EndsWith(".xlsx"))
            {
                EdiDocument doc = EdiUtil.EdiDocumentFromFile(ediFile);
                EdiUploader uploader = EdiFactory.GetUploader(doc);
                uploader.Insert(doc);
            }
            else if(ediFile.ToLower().EndsWith(".pdf"))
            {
                List<FreightInvoice210> list = PdfUtil.Freight210ListFromPdf(ediFile);
                EdiUploader210 uploader210 = new EdiUploader210(); ;
                foreach (FreightInvoice210 freightInvoice210 in list)
                {
                    uploader210.Insert(freightInvoice210);
                }
            }
        }
    }
}