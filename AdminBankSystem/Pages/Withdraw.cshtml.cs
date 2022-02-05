using AdminBankSystem.Data;
using AdminBankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Transactions;

namespace AdminBankSystem.Pages
{
    [BindProperties]
    public class WithdrawModel : PageModel
    {
        private readonly BankContext _context;
        private readonly ITransactionService _transactionService;
        

            
            
        public WithdrawModel(BankContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;
            
        }

        [Range(10, 10000)]
        public decimal Amount { get; set; }
        public decimal Balance { get; set; }
        public DateTime DateWhen { get; set; }

        public string Comment { get; set; }

        public void OnGet()
        {
            

            Comment = "Withdrawal";
            Amount = 0;
        }

        public IActionResult OnPost(int accountId, int customerId)
        {
            var x = _context.Accounts.FirstOrDefault(x => x.AccountId == accountId);

            Balance = x.Balance;

            if (Amount > Balance )
            {
                ModelState.AddModelError("Amount", "Amount can't be greater then the accounts balance ");
            }
            if (ModelState.IsValid)
            {
                var account = _transactionService.GetAccount(accountId);
                account.Balance -= Amount;
                var transaction = new Data.Transaction { Amount = Amount, Date = DateWhen, Operation = Comment, Type = "Credit", Balance = account.Balance };
                account.Transactions.Add(transaction);

                _transactionService.Update(account);
                return RedirectToPage("Customer", new { Id = customerId });
            }

            return Page();

            

        }
    }
}
