using AdminBankSystem.Infastructure.Paging;
using AdminBankSystem.Models;

namespace AdminBankSystem.Services
{
    public interface IPageService
    {
        PagedResult<Customer> GetPages(int pageno);
    }
}
