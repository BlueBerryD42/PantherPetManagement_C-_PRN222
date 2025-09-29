using BLL.Service;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PantherPetManagement_NguyenAnhQuan.Pages.PantherProfile
{
    public class IndexModel : PageModel
    {
        private readonly IProfileService _profile;
        private const int PageSize = 3;

        public IndexModel(IProfileService profile)
            => _profile = profile;

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public double? SearchWeight { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTypeName { get; set; }

        public List<DAL.Models.PantherProfile> Profiles { get; set; } = new List<DAL.Models.PantherProfile>();

        public int TotalPages { get; set; }

        public bool IsManager => HttpContext.Session.GetInt32("RoleId") == 2;

        public IActionResult OnPostViewDetail(int pantherId)
        {
            TempData["SelectedpantherId"] = pantherId;
            return RedirectToPage("/PantherProfile/Details");
        }

        public IActionResult OnPostEditRedirect(int pantherId)
        {
            TempData["SelectedpantherId"] = pantherId;
            return RedirectToPage("/PantherProfile/Update");
        }

        public IActionResult OnPostDeleteRedirect(int pantherId)
        {
            TempData["SelectedpantherId"] = pantherId;
            return RedirectToPage("/PantherProfile/Delete");
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            bool hasSearch = SearchWeight.HasValue || !string.IsNullOrWhiteSpace(SearchTypeName);

            try
            {
                if (hasSearch)
                {
                    var totalSearchCount = await _profile.CountSearchProfilesAsync(SearchWeight, SearchTypeName);
                    TotalPages = (int)Math.Ceiling(totalSearchCount / (double)PageSize);
                    Profiles = await _profile.SearchProfilesAsync(SearchWeight, SearchTypeName, Pages, PageSize);
                }
                else
                {
                    var totalCount = await _profile.CountProfilesAsync();
                    TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                    Profiles = await _profile.GetProfilesAsync(Pages, PageSize);
                }

                if (Pages > TotalPages && TotalPages > 0)
                {
                    Pages = TotalPages;
                }
            }
            catch (Exception)
            {
                Profiles = new List<DAL.Models.PantherProfile>();
                TotalPages = 0;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            bool hasSearch = SearchWeight.HasValue || !string.IsNullOrWhiteSpace(SearchTypeName);

            try
            {
                if (hasSearch)
                {
                    var totalSearchCount = await _profile.CountSearchProfilesAsync(SearchWeight, SearchTypeName);
                    TotalPages = (int)Math.Ceiling(totalSearchCount / (double)PageSize);
                    Profiles = await _profile.SearchProfilesAsync(SearchWeight, SearchTypeName, Pages, PageSize);
                }
                else
                {
                    var totalCount = await _profile.CountProfilesAsync();
                    TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
                    Profiles = await _profile.GetProfilesAsync(Pages, PageSize);
                }

                if (Pages > TotalPages && TotalPages > 0)
                {
                    Pages = TotalPages;
                }
            }
            catch (Exception)
            {
                Profiles = new List<DAL.Models.PantherProfile>();
                TotalPages = 0;
            }

            return Page();
        }
    }
}