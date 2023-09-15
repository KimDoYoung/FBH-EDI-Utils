using FBH.EDI.Common;
using FBH.EDI.Common.ExcelPdfUtils;
using FBH.EDI.Common.Model;
using System;
using System.Collections.Generic;
using System.IO;

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

        internal string insert(string ediFile)
        {
            MessageEventHandler?.Invoke(null, new MessageEventArgs($"{ediFile} upload start "));
            if (ediFile.ToLower().EndsWith(".xlsx"))
            {
                EdiDocument doc = EdiUtil.EdiDocumentFromFile(ediFile);
                doc.FileName = Path.GetFileName( ediFile ); //filename setting

                EdiUploader uploader = EdiFactory.GetUploader(doc);
                uploader.SetConnectionString(connectString);

                var result = uploader.Insert(doc);
                var msg = $"{doc} insert result : {result}";
                MessageEventHandler?.Invoke(null, new MessageEventArgs(msg));
                return result;
            }
            else if(ediFile.ToLower().EndsWith(".pdf"))
            {
                List<FreightInvoice210> list = PdfUtil.Freight210ListFromPdf(ediFile);
                EdiUploader210 uploader210 = new EdiUploader210(); ;
                uploader210.SetConnectionString(connectString);

                string result = "OK";
                foreach (FreightInvoice210 freightInvoice210 in list)
                {
                    freightInvoice210.FileName = Path.GetFileName(ediFile);

                    var r = uploader210.Insert(freightInvoice210);
                    if (r.StartsWith("NK")) result = r;
                    var msg = $"insert result : {r}";
                    MessageEventHandler?.Invoke(null, new MessageEventArgs(msg));
                }
                return result;
            }else
            {
                return "NK:지원하지 않는 파일형식입니다";
            }
        }
    }
}