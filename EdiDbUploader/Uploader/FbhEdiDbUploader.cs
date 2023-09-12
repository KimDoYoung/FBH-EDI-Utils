using FBH.EDI.Common;
using FBH.EDI.Common.Model;
using System;

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
            EdiDocument doc = EdiUtil.EdiDocumentFromFile(ediFile);
            EdiUploader uploader = EdiFactory.GetUploader(doc);
            uploader.Insert(doc);
        }
    }
}