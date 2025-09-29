using BLL.Service;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantherPetManagement_NguyenAnhQuan.Pages.PantherProfile
{
    public class SearchModel : PageModel
    {
        private readonly IProfileService _profileService;
        public SearchModel(IProfileService profileService)
        {
            _profileService = profileService;
        }

        [BindProperty]
        public double? SearchWeight { get; set; }
        [BindProperty]
        public string? SearchTypeName { get; set; }

        public List<DAL.Models.PantherProfile> Profiles { get; set; } = new List<DAL.Models.PantherProfile>();

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            if (SearchWeight.HasValue || !string.IsNullOrWhiteSpace(SearchTypeName))
            {
                Profiles = await _profileService.SearchProfilesAsync(SearchWeight, SearchTypeName, 1, 100);
            }
            else
            {
                Profiles = new List<DAL.Models.PantherProfile>();
            }
            return Page();
        }
    }
}