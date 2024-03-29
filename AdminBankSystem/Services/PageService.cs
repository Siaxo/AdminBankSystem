﻿using AdminBankSystem.Infastructure.Paging;
using AdminBankSystem.Data;

namespace AdminBankSystem.Services
{
    public class PageService : IPageService
    {
        private readonly BankContext _context;

        public PageService(BankContext context)
        {
            _context = context;
        }

        public PagedResult<Customer> GetPages(int pageno, string searchQuery)
        {
            var query = _context.Customers.Where(s => searchQuery == null || (s.Givenname.Contains(searchQuery) || (s.City.Contains(searchQuery)))).GetPaged(pageno, 50);

            return query;
             
        }
    }
}
