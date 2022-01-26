using AdminBankSystem.Models;
using AdminBankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace AdminBankSystem.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly BankContext _context;
        private readonly IPageService _pageService;

        public IndexModel(ILogger<IndexModel> logger, BankContext context, IPageService pageService)
        {
            _logger = logger;
            _context = context;
            _pageService = pageService;
        }

        public int CustomerAmount { get; set; }
        public int AccountAmount { get; set; }
        public int BalanceAmount { get; set; }
        public int CurrentPage { get; set; } = 1;
        public int PageCount { get; set; }
        

        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Address { get; set; }
            public string SocialSecurity { get; set; }
            public string City { get; set; }
        }

        public List<CustomerViewModel> Customers { get; set; }





        public void OnGet(int pageno = 1)
        {
            CustomerAmount = _context.Customers.Count();
            AccountAmount = _context.Accounts.Count();
            BalanceAmount = (int)_context.Transactions.Sum(t => t.Amount);

            //Customers = _context.Customers
            //    .Take(50)
            //    .Select(r => new CustomerViewModel
            //{
            //    Id = r.CustomerId,
            //    Name = r.Givenname,
            //    Address = r.Streetaddress,
            //    SocialSecurity = r.NationalId,
            //    City = r.City
            //}).ToList();

            CurrentPage = pageno;

            var pageresult = _pageService.GetPages(CurrentPage);
            Customers = pageresult.Results.Select(x => new CustomerViewModel
            {
                Id = x.CustomerId,
                Name = x.Givenname,
                Address = x.Streetaddress,
                SocialSecurity = x.NationalId,
                City = x.City
            }).ToList();
            PageCount = pageresult.PageCount;
        }
    }
}