using AdminBankSystem.Data;

namespace AdminBankSystem.Services
{
    public interface ICustomerService
    {

        public Customer GetCustomer(int id);
    }
}
