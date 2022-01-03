using AdminBankSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AdminBankSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BankContext _context;

        public IndexModel(ILogger<IndexModel> logger, BankContext context)
        {
            _logger = logger;
            _context = context;
        }

        public int CustomerAmount { get; set; }
        public int AccountAmount { get; set; }
        public int BalanceAmount { get; set; }


        

        

        public void OnGet()
        {
            CustomerAmount = _context.Customers.Count();
            AccountAmount = _context.Accounts.Count();
        }
    }
}