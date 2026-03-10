using ConciliadorApp.Models;

namespace ConciliadorApp.Services
{
    public class ReconciliationService
    {
        public ReconciliationResult Compare(List<TransferRecord> banRedRecords, List<TransferRecord> proCreditRecords)
        {
            var banRedCount = banRedRecords.Count;
            var proCreditCount = proCreditRecords.Count;

            var banRedTotal = banRedRecords.Sum(x => x.Monto);
            var proCreditTotal = proCreditRecords.Sum(x => x.Monto);

            return new ReconciliationResult
            {
                BanRedCount = banRedCount,
                ProCreditCount = proCreditCount,
                BanRedTotalAmount = banRedTotal,
                ProCreditTotalAmount = proCreditTotal,
                IsRecordCountEqual = banRedCount == proCreditCount,
                IsTotalAmountEqual = banRedTotal == proCreditTotal
            };
        }
    }
}