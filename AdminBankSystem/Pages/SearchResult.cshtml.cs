using AdminBankSystem.Data;
using AdminBankSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminBankSystem.Pages
{
    public class SearchResultModel : PageModel
    {
        private readonly BankContext _context;
        private readonly ISearchService _searchServiceService;

        public SearchResultModel(BankContext context, ISearchService searchService)
        {
            _context = context;
            _searchServiceService = searchService;
        }

        
            public int Id { get; set; }
            public string Name { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string SocialSecurity { get; set; }
            public string City { get; set; }
        

       


        public void OnGet(int searchQuery)
        {
            var searchResult = _searchServiceService.SearchCustomer(searchQuery);


            var x = _context.Customers.FirstOrDefault(x => x.CustomerId == searchQuery);

            Id = x.CustomerId;
            Name = x.Givenname;
            LastName = x.Surname;
            Address = x.Streetaddress;
            SocialSecurity = x.NationalId;
            City = x.City;

        }
    }
}
