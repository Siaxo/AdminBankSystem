using AdminBankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminBankSystem.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly BankContext _context;

        public CustomerService(BankContext context)
        {
            _context = context;
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Include(x => x.Dispositions).ThenInclude(a => a.Account).First(s => s.CustomerId == id);
        }
    }
}
