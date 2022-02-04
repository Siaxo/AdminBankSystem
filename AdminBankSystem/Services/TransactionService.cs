using AdminBankSystem.Data;
using Microsoft.EntityFrameworkCore;

namespace AdminBankSystem.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankContext _context;

        public TransactionService(BankContext context)
        {
            _context = context;
        }

        public List<Account> GetAll()
        {
            return _context.Accounts.ToList();
        }

        public void Update(Account account)
        {
            _context.SaveChanges();
        }

        public Account GetAccount(int id)
        {
            return _context.Accounts.Include(a => a.Transactions).First(e => e.AccountId == id);
        }
    }
}
