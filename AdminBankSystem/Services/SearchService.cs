using AdminBankSystem.Data;

namespace AdminBankSystem.Services
{
    public class SearchService : ISearchService
    {

        private readonly BankContext _context;

        public SearchService(BankContext context)
        {
            _context = context;
        }

        public Customer SearchCustomer(int searchQuery)
        {
            var query = _context.Customers.FirstOrDefault(s => searchQuery == null || (s.CustomerId == searchQuery));

            return query;
        }
    }
}
