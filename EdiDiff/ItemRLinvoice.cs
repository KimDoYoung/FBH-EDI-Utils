using static System.Windows.Forms.LinkLabel;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using System.Security.Claims;
using System.Windows.Forms;

namespace EdiDiff
{
    internal class ItemRLinvoice
    {
        public string InvoiceClaimNumber { get; set; }
        public string DivisionNumber { get; set; }
        public string StoreNumber { get; set; }
        public string DateClaimCode { get; set; }
        public string Amount { get; set; }
        public string MicroNumber { get; set; }
        public string CheckNumber { get; set; }
        public string CheckDate { get; set; }
        public string DeductionCode { get; set; }
        public string Status { get; set; }

    }
}