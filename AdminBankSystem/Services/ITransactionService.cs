using AdminBankSystem.Data;

namespace AdminBankSystem.Services
{
    public interface ITransactionService
    {
        public List<Account> GetAll();

        void Update(Account account);
        Account GetAccount(int id);
    }
}
