namespace ConciliadorApp.Models
{
    public class ReconciliationResult
    {
        public int BanRedCount { get; set; }
        public int ProCreditCount { get; set; }
        public decimal BanRedTotalAmount { get; set; }
        public decimal ProCreditTotalAmount { get; set; }
        public bool IsRecordCountEqual { get; set; }
        public bool IsTotalAmountEqual { get; set; }
        public bool IsSuccessful => IsRecordCountEqual && IsTotalAmountEqual;
    }
}