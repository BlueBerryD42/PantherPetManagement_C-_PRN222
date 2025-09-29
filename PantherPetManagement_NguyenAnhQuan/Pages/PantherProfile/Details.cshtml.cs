using DAL.Repositories;
using DAL.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace PantherPetManagement_NguyenAnhQuan.Pages.PantherProfile
{
    public class DetailModel : PageModel
    {
        private readonly IProfileRepository _repo;
        public DetailModel(IProfileRepository repo) => _repo = repo;

        public DAL.Models.PantherProfile? Profile { get; set; }

        // Thay đổi từ OnGetAsync(int id) thành OnGetAsync()
        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            if (!TempData.ContainsKey("SelectedPantherId"))
            {
                return RedirectToPage("./Index");
            }

            int id = (int)TempData["SelectedPantherId"];

            try
            {
                Profile = await _repo.GetByIdAsync(id);
                if (Profile == null)
                    return RedirectToPage("./Index");
            }
            catch (System.Exception)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }
    }
}