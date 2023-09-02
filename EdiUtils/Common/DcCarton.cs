namespace EdiUtils.Common
{
    internal class DcCarton
    {
        public string DC { get; private set; }
        public int OrangeCount { get; set; }
        public int PineappleCount { get; set; }
        public int MangoCount { get;set; }
        public DcCarton(string dc)
        {
            this.DC = dc;
            this.OrangeCount = this.PineappleCount = this.MangoCount = 0;
        }
    }
}