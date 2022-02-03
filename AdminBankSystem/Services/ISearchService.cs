using AdminBankSystem.Data;

namespace AdminBankSystem.Services
{
    public interface ISearchService
    {
        public Customer SearchCustomer(int searchQuery);
    }
}
