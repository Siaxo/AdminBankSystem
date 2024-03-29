using AdminBankSystem.Data;
using AdminBankSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using static AdminBankSystem.Pages.IndexModel;

namespace AdminBankSystem.Pages
{
    [Authorize(Roles = "Admin, Cashier")]
    public class CustomerModel : PageModel
    {
        private readonly BankContext _context;
        private readonly ICustomerService _customerService;

        public CustomerModel(BankContext context, ICustomerService customerService)
        {
            _context = context;
            _customerService = customerService;
        }

        public decimal Balance { get; set; }
        public int Account { get; set; }


        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string SocialSecurity { get; set; }
        public string City { get; set; }

        public class AccountViewModel
        {
            public int AccountId { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }



        public List<AccountViewModel> Accounts { get; set; } = new List<AccountViewModel>();


        public void OnGet(int id)
        {

            

            var x = _context.Customers.First(x => x.CustomerId == id);

            Id = x.CustomerId;
            Name = x.Givenname;
            LastName = x.Surname;
            Address = x.Streetaddress;
            SocialSecurity = x.NationalId;
            City = x.City;




            var customerResult = _customerService.GetCustomer(id);
            foreach (var disp in customerResult.Dispositions)
            {
                Accounts.Add(new AccountViewModel
                {
                    AccountId = disp.AccountId,
                    Created = disp.Account.Created,
                    Balance = disp.Account.Balance
                });
            }

            
            
        }

        
    }
}
