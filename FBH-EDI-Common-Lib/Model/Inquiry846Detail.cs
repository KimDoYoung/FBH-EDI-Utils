using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class Inquiry846Detail
    {
        public string HubGroupDocumentNumber { get; set; }
        public int? AssgndNo { get; set; }
        public string Sku { get; set; }
        public string LotCode { get; set; }
        public int? NonCommittedIn { get; set; }
        public int? NonCommittedOut { get; set; }
        public int? OnHandQuantity { get; set; }
        public int? InboundPending { get; set; }
        public int? OutboundPending { get; set; }
        public int? DamagedQuantity { get; set; }
        public int? OnHoldQuantity { get; set; }
        public int? AvailableQuantity { get; set; }
        public int? TotalInventory { get; set; }
        public override string ToString()
        {
            return $"HubGroupDocumentNumber : {HubGroupDocumentNumber}, "
                + $"AssgndNo : {AssgndNo}, "
                + $"Sku : {Sku}, "
                + $"LotCode : {LotCode}, "
                + $"NonCommittedIn : {NonCommittedIn}, "
                + $"NonCommittedOut : {NonCommittedOut}, "
                + $"OnHandQuantity : {OnHandQuantity}, "
                + $"InboundPending : {InboundPending}, "
                + $"OutboundPending : {OutboundPending}, "
                + $"DamagedQuantity : {DamagedQuantity}, "
                + $"OnHoldQuantity : {OnHoldQuantity}, "
                + $"AvailableQuantity : {AvailableQuantity}, "
                + $"TotalInventory : {TotalInventory}";
        }
    }
}
