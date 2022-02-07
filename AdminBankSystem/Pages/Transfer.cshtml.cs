using AdminBankSystem.Data;
using AdminBankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AdminBankSystem.Pages
{
    
    public class TransferModel : PageModel
    {
        private readonly BankContext _context;
        private readonly ITransactionService _transactionService;




        public TransferModel(BankContext context, ITransactionService transactionService)
        {
            _context = context;
            _transactionService = transactionService;

        }
        [BindProperty]
        [Range(10, 5000)]
        public decimal Amount { get; set; }
        public DateTime DateWhen { get; set; }
        public string Comment { get; set; }
        [BindProperty]
        public int Receiver { get; set; }
        
        public class AccountViewModel
        {
            public int AccountId { get; set; } 
        }

        public List<AccountViewModel> Accounts { get; set; }

        public void OnGet()
        {
            Accounts = _transactionService.GetAll().Select(x => new AccountViewModel
            {
                AccountId = x.AccountId
            }).ToList();

            
            Amount = 0;
        }

        public IActionResult OnPost(int accountId)
        {

            var balance = _transactionService.GetAccount(accountId);
            if (balance.Balance < Amount || Amount == 0)
            {
                ModelState.AddModelError("Amount", "Amount can't be zero or greater then the accounts balance ");
            }
            if(Receiver == 0 || Receiver == accountId)
            {
                ModelState.AddModelError("Receiver", "Account do not match a known Account ");
            }
            if (ModelState.IsValid)
            {
                var account = _transactionService.GetAccount(accountId);
                account.Balance -= Amount;
                var transaction = new Transaction();
                transaction.Date = DateTime.Now;
                transaction.Amount = Amount * -1;
                transaction.Operation = "Transfer Withdrawal";
                transaction.Type = "Debit";
                account.Transactions.Add(transaction);
                _transactionService.Update(account);

                account = _transactionService.GetAccount(Receiver);
                account.Balance += Amount;
                transaction = new Transaction();
                transaction.Date = DateTime.Now;
                transaction.Amount = Amount;
                transaction.Operation = "Transfer Deposit";
                transaction.Type = "Debit";
                account.Transactions.Add(transaction);
                _transactionService.Update(account);
                return RedirectToPage("Index");
            }

            Accounts = _transactionService.GetAll().Select(x => new AccountViewModel
            {
                AccountId = x.AccountId
            }).ToList();

            return Page();



        }
    }

}
