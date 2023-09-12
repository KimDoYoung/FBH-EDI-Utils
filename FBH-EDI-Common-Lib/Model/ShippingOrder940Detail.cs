using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class ShippingOrder940Detail
    {
        public string OrderId { get; set; }
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
        public string Misc1NumberOfPack { get; set; }
        public string Misc1SizeOfUnits { get; set; }
        public string Misc1SizeUnit { get; set; }
        public string Misc1ColorDescription { get; set; }
        public string Misc2NumberOfPack { get; set; }
        public string Misc2SizeOfUnits { get; set; }
        public string Misc2SizeUnit { get; set; }
        public string Misc2ColorDescription { get; set; }
        public string Misc3NumberOfPack { get; set; }
        public string Misc3SizeOfUnits { get; set; }
        public string Misc3SizeUnit { get; set; }
        public string Misc3ColorDescription { get; set; }
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
             + $"misc1NumberOfPack : {Misc1NumberOfPack}, "
             + $"misc1SizeOfUnits : {Misc1SizeOfUnits}, "
             + $"misc1SizeUnit : {Misc1SizeUnit}, "
             + $"misc1ColorDescription : {Misc1ColorDescription}, "
             + $"misc2NumberOfPack : {Misc2NumberOfPack}, "
             + $"misc2SizeOfUnits : {Misc2SizeOfUnits}, "
             + $"misc2SizeUnit : {Misc2SizeUnit}, "
             + $"misc2ColorDescription : {Misc2ColorDescription}, "
             + $"misc3NumberOfPack : {Misc3NumberOfPack}, "
             + $"misc3SizeOfUnits : {Misc3SizeOfUnits}, "
             + $"misc3SizeUnit : {Misc3SizeUnit}, "
             + $"misc3ColorDescription : {Misc3ColorDescription}";
        }
    }
}
