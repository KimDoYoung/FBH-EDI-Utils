namespace EdiDiff
{
    internal class DiffInvoiceItem
    {
        public ItemInvoice invoice { get; set; }
        public ItemRLinvoice RLinvoice{ get; set; }

        public string result { get; set; }
    }
}