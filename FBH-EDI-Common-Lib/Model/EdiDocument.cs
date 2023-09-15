using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    /// <summary>
    /// Edi 문서들 즉 EdiModel들의 부모
    /// </summary>
    public class EdiDocument
    {
        public EdiDocumentNo DocumentNo { get; set; }
        public string FileName { get;  set; }
        public string Memo { get; set; }
    }
}
