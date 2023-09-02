using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiRename.Common
{
    internal class NameProperties
    {


        public string EdiTypeNo { get; set; }   
        public string Invoice_Po_No { get;set; }

        public NameProperties(string ediTypeNo, string in_or_po)
        {
            this.EdiTypeNo = ediTypeNo;
            this.Invoice_Po_No = in_or_po;
        }

        internal string GetFilename(string ext)
        {
            if(ext.StartsWith(".") == false)
            {
                ext = "." + ext;
            }
            return $"{EdiTypeNo}_{Invoice_Po_No}{ext}";
        }
    }
}
