using AdminBankSystem.Data;
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
        public string Query { get; set; }
        

        public class CustomerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string SocialSecurity { get; set; }
            public string City { get; set; }
        }

        public List<CustomerViewModel> Customers { get; set; }






        public void OnGet(string query, int pageno = 1 )
        {
            CustomerAmount = _context.Customers.Count();
            AccountAmount = _context.Accounts.Count();
            BalanceAmount = (int)_context.Transactions.Sum(t => t.Amount);



            CurrentPage = pageno;
            Query = query;

            var pageresult = _pageService.GetPages(CurrentPage, query);
            Customers = pageresult.Results.Select(x => new CustomerViewModel
            {
                Id = x.CustomerId,
                Name = x.Givenname,
                LastName = x.Surname,
                Address = x.Streetaddress,
                SocialSecurity = x.NationalId,
                City = x.City
            }).ToList();
            PageCount = pageresult.PageCount;
            
            
        }

    }
}