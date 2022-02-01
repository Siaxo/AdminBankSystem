using AdminBankSystem.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using static AdminBankSystem.Pages.IndexModel;

namespace AdminBankSystem.Pages
{
    public class CustomerModel : PageModel
    {
        private readonly BankContext _context;

        public CustomerModel(BankContext context)
        {
            _context = context;
        }

        public decimal Balance { get; set; }
        public string Account { get; set; }


        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string SocialSecurity { get; set; }
        public string City { get; set; }

        public List<CustomerViewModel> Customers { get; set; }
        public List<Account> Accounts { get; set; }

        public void OnGet()
        {
            Customers = _context.Customers.Select(x => new CustomerViewModel
            {
                Id = x.CustomerId,
                Name = x.Givenname,
                Address = x.Streetaddress,
                SocialSecurity = x.NationalId,
                City = x.City
            }).ToList();

            
        }
    }
}
