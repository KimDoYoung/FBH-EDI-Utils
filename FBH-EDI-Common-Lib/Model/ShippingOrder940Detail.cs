using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class ShippingOrder940Detail
    {

        public int Seq { get; set; }
        public string QuantityOrdered { get; set; }
        public string UnitOfMeasure { get; set; }
        public string UpcCode { get; set; }
        public string Sku { get; set; }
        public string RetailersItemCode { get; set; }
        public string LotNumber { get; set; }
        public string Scc14 { get; set; }
        public string FreeFormDescription { get; set; }
        public string RetailPrice { get; set; }
        public string CostPrice { get; set; }
        public string misc1NumberOfPack { get; set; }
        public string misc1SizeOfUnits { get; set; }
        public string misc1SizeUnit { get; set; }
        public string misc1ColorDescription { get; set; }
        public string misc2NumberOfPack { get; set; }
        public string misc2SizeOfUnits { get; set; }
        public string misc2SizeUnit { get; set; }
        public string misc2ColorDescription { get; set; }
        public string misc3NumberOfPack { get; set; }
        public string misc3SizeOfUnits { get; set; }
        public string misc3SizeUnit { get; set; }
        public string misc3ColorDescription { get; set; }
        override public string ToString()
        {
            return $"Seq : {Seq}, "
             + $"QuantityOrdered : {QuantityOrdered}, "
             + $"UnitOfMeasure : {UnitOfMeasure}, "
             + $"UpcCode : {UpcCode}, "
             + $"Sku : {Sku}, "
             + $"RetailersItemCode : {RetailersItemCode}, "
             + $"LotNumber : {LotNumber}, "
             + $"Scc14 : {Scc14}, "
             + $"FreeFormDescription : {FreeFormDescription}, "
             + $"RetailPrice : {RetailPrice}, "
             + $"CostPrice : {CostPrice}, "
             + $"misc1NumberOfPack : {misc1NumberOfPack}, "
             + $"misc1SizeOfUnits : {misc1SizeOfUnits}, "
             + $"misc1SizeUnit : {misc1SizeUnit}, "
             + $"misc1ColorDescription : {misc1ColorDescription}, "
             + $"misc2NumberOfPack : {misc2NumberOfPack}, "
             + $"misc2SizeOfUnits : {misc2SizeOfUnits}, "
             + $"misc2SizeUnit : {misc2SizeUnit}, "
             + $"misc2ColorDescription : {misc2ColorDescription}, "
             + $"misc3NumberOfPack : {misc3NumberOfPack}, "
             + $"misc3SizeOfUnits : {misc3SizeOfUnits}, "
             + $"misc3SizeUnit : {misc3SizeUnit}, "
             + $"misc3ColorDescription : {misc3ColorDescription}";
        }
    }
}
