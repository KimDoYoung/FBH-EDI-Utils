using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FBH.EDI.Common.Model
{
    public class HubSystemData : EdiDocument
    {
        public string FileId { get; set; }
        public int Seq { get; set; }
        public string WhActivityDate { get; set; }
        public string ReqDeliveryDate { get; set; }
        public string ActualDeliveryDate { get; set; }
        public string Warehouse { get; set; }
        public string Customer { get; set; }
        public string OrderId { get; set; }
        public string PoNo { get; set; }
        public string Sku { get; set; }
        public string LotCode { get; set; }
        public int? Received { get; set; }
        public int? Shipped { get; set; }
        public int? Damaged { get; set; }
        public int? Hold { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }

        public HubSystemData()
        {
          
        }
        override public string ToString()
        {
            string s = $"FileId: {FileId}\r\n" +
            $"Seq: {Seq}\r\n" +
            $"WhActivityDate: {WhActivityDate}\r\n" +
            $"ReqDeliverDate: {ReqDeliveryDate}\r\n" +
            $"ActualDeliveryDate: {ActualDeliveryDate}\r\n" +
            $"Warehouse: {Warehouse}\r\n" +
            $"Customer: {Customer}\r\n" +
            $"OrderId: {OrderId}\r\n" +
            $"PoNo: {PoNo}\r\n" +
            $"Sku: {Sku}\r\n" +
            $"LotCode: {LotCode}\r\n" +
            $"Received: {Received}\r\n" +
            $"Shipped: {Shipped}\r\n" +
            $"Damaged: {Damaged}\r\n" +
            $"Hold: {Hold}\r\n" +
            $"Memo: {Memo}\r\n" +
            $"CreatedBy: {CreatedBy}\r\n" +
            $"CreatedOn: {CreatedOn}\r\n" +
            $"FileName: {FileName}\r\n";
            return s.Trim();
        }
    }
}
