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
        

        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string SocialSecurtity { get; set; }
            public string City { get; set; }
        }

        public List<CustomerViewModel> Customers { get; set; }





        public void OnGet()
        {
            CustomerAmount = _context.Customers.Count();
            AccountAmount = _context.Accounts.Count();
            BalanceAmount = _context.Transactions.Count();

            Customers = _context.Customers
                .Take(50)
                .Select(r => new CustomerViewModel
            {
                Id = r.CustomerId,
                Name = r.Givenname,
                Address = r.Streetaddress,
                SocialSecurtity = r.NationalId,
                City = r.City
            }).ToList();
        }
    }
}