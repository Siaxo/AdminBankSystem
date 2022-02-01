using AdminBankSystem.Data;
using AdminBankSystem.Services;
using AdminBankSystem.Infastructure.CustomerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using static AdminBankSystem.Pages.IndexModel;

namespace AdminBankSystem.Pages
{
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
        public string Address { get; set; }
        public string SocialSecurity { get; set; }
        public string City { get; set; }

        public class AccountViewModel
        {
            public int AccountId { get; set; }
            public DateTime Created { get; set; }
            public decimal Balance { get; set; }
        }

        public List<CustomerViewModel> Customers { get; set; }
       
        public List<AccountViewModel> Accounts { get; set; }


        public void OnGet(int id)
        {
            Customers = _context.Customers.Select(x => new CustomerViewModel
            {
                Id = x.CustomerId,
                Name = x.Givenname,
                Address = x.Streetaddress,
                SocialSecurity = x.NationalId,
                City = x.City
            }).ToList();

            AccountId = id;

            var customerResult = _customerService.GetCustomer(Account, id);
            Accounts = customerResult.Results.Select(x => new AccountViewModel
            {
                AccountId = x.AccountId,
                Created = x.Created,
                Balance = x.Balance
                
            }).ToList();
        }
    }
}
