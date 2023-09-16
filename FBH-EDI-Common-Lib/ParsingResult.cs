using FBH.EDI.Common.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common
{
    public class ParsingResult
    {
        private EdiDocumentNo ediDocumentNumber;
        private List<EdiDocument> list;

        public EdiDocumentNo EdiDocumentNumber { get { return this.ediDocumentNumber; } set { this.ediDocumentNumber = value; } }

        public List<EdiDocument> EdiDocumentList { get { return this.list; } }

        public ParsingResult()
        {
            this.ediDocumentNumber = EdiDocumentNo.Unknown;
            this.list = new List<EdiDocument>();
        }
        public void Add(EdiDocument doc)
        {
            list.Add(doc);
        }

    }
}
