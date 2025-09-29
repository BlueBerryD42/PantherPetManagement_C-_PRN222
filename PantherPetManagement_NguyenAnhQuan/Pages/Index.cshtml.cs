using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BLL.Dtos;
using BLL.Service;

namespace PantherPetManagement_NguyenAnhQuan.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAccountService _account;

        public IndexModel(IAccountService account)
        {
            _account = account;
        }

        [BindProperty]
        public LoginDto Input { get; set; } = new LoginDto();

        public string? ErrorMessage { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            var user = await _account.LoginAsync(Input.Email, Input.Password);
            if (user == null)
            {
                ErrorMessage = "Invalid Email or Password!";
                return Page();
            }

            HttpContext.Session.SetInt32("AccountId", user.AccountId);
            HttpContext.Session.SetString("UserName", user.UserName ?? string.Empty);
            HttpContext.Session.SetInt32("RoleId", user.RoleId);

            return RedirectToPage("/PantherProfile/Index");
        }
    }
}