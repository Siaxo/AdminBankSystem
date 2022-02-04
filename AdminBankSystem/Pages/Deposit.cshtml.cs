using AdminBankSystem.Data;
using AdminBankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AdminBankSystem.Pages
{
    [BindProperties]
    public class DepositModel : PageModel
    {
        private readonly ITransactionService _transactionService;

        public DepositModel(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        [Range(10, 10000)]
        public decimal Amount { get; set; }

        public DateTime DateWhen { get; set; }

        [MinLength(5)]
        [MaxLength(100)]
        public string Comment { get; set; }

        public void OnGet(int accountId, int customerId)
        {
            DateWhen = DateTime.Now.AddDays(1).Date;
            Amount = 0;
        }

        public IActionResult OnPost(int accountId, int customerId)
        {
            if (DateWhen < DateTime.Now.AddDays(1).Date) 
            {
                ModelState.AddModelError("DateWhen", "Date needs to be atleast one day ahead of current date ");
            }
            if (ModelState.IsValid)
            {
                var account = _transactionService.GetAccount(accountId);
                account.Balance += Amount;
                var transaction = new Transaction { Amount = Amount, Operation = Comment, Date = DateWhen, Type = "Credit", Balance = account.Balance };
                account.Transactions.Add(transaction);

                _transactionService.Update(account);
                return RedirectToPage("Customer", new {Id = customerId });
            }

            return Page();
        }
    }
}
