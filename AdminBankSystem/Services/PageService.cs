using AdminBankSystem.Infastructure.Paging;
using AdminBankSystem.Models;

namespace AdminBankSystem.Services
{
    public class PageService : IPageService
    {
        private readonly BankContext _context;

        public PageService(BankContext context)
        {
            _context = context;
        }

        public PagedResult<Customer> GetPages(int pageno)
        {
            var query = query.GetPaged(pageno, 5);
             
        }
    }
}
