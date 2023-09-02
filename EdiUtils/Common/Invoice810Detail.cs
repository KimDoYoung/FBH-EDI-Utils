using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EdiUtils.Common
{
    public class Invoice810Detail
    {
        public string PoNo { get; set; }
        public string InvoiceNo { get; set; }
        public int Seq { get; set; }
        public int Qty { get; set; }
        public string Msrmnt { get; set; }
        public decimal UnitPrice { get; set; }
        public string Gtin13 { get; set; }
        public decimal LineTtl { get; set; }

        override public string ToString()
        {
            return  $"PoNo : {PoNo}, " +
                    $"InvoiceNo : {InvoiceNo}, " +
                    $"Seq : {Seq}, " +
                    $"Qty : {Qty}, " +
                    $"Msrmnt : {Msrmnt}, " +
                    $"UnitPrice : {UnitPrice}, " +
                    $"Gtin13 : {Gtin13}, " +
                    $"LineTtl : {LineTtl}, ";
        }
    }
}
