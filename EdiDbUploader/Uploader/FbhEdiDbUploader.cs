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

        internal List<string> Insert(string ediFile)
        {
            var fileNameOnly= Path.GetFileName(ediFile);
            MessageEventHandler?.Invoke(null, new MessageEventArgs($"{fileNameOnly} start "));
            ParsingResult parsingResult = EdiUtil.EdiDocumentParsing(ediFile);

            EdiUploader uploader = EdiFactory.GetUploader(parsingResult.EdiDocumentNumber);
            uploader.SetConnectionString(this.connectString);
            return uploader.Insert(parsingResult.EdiDocumentList);
        }
    }
}