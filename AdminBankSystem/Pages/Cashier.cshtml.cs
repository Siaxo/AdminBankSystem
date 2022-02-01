using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AdminBankSystem.Pages
{
    [Authorize(Roles = "Cashier")]
    public class CashierModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
