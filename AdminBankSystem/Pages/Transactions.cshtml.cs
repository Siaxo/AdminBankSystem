using AdminBankSystem.Data;
using AdminBankSystem.Infastructure.Paging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminBankSystem.Pages
{
    public class TransactionsModel : PageModel
    {
        private readonly BankContext _context;

        public TransactionsModel(BankContext context)
        {
            _context = context;
            
        }

        public int AccountId { get; set; }

        public class TransactionViewModel
        {
            public int TransactionId { get; set; }
            public DateTime Date { get; set; }
            public string Type { get; set; } = null!;
            public string Operation { get; set; } = null!;
            public decimal Amount { get; set; }
            public decimal Balance { get; set; }
            public string? Bank { get; set; }
            public string? Account { get; set; }
        }

        public List<TransactionViewModel> Transactions { get; set; }


        public void OnGet(int accountId)
        {
            AccountId = accountId;

            var transaction = _context.Transactions.FirstOrDefault(account => account.AccountId == accountId);
        }

        public IActionResult OnGetFetchMore(int accountId, int pageNo)
        {

            var list = _context.Transactions
                .Where(e => e.AccountId == accountId)
                .OrderByDescending(e => e.Date)
                .GetPaged(pageNo, 20    ).Results
                .Select(e => new TransactionViewModel
                {
                    Date = e.Date,
                    TransactionId = e.TransactionId,
                    Type = e.Type,
                    Operation = e.Operation,
                    Amount = e.Amount,
                    Balance = e.Balance,
                    Bank = e.Bank,
                    Account = e.Account


                }).ToList();

            return new JsonResult(new { Items = list });
        }
    }
}
