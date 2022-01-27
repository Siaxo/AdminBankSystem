using AdminBankSystem.Infastructure.Paging;
using AdminBankSystem.Data;

namespace AdminBankSystem.Services
{
    public interface IPageService
    {
        PagedResult<Customer> GetPages(int pageno);
    }
}
