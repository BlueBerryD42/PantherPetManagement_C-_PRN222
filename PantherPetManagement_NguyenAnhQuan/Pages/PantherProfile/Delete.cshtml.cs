using BLL.Service;
using DAL.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PantherPetManagement_NguyenAnhQuan.Pages.PantherProfile
{
    public class DeleteModel : PageModel
    {
        private readonly IProfileService _profile;

        public DeleteModel(IProfileService profile)
        {
            _profile = profile;
        }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Pages { get; set; } = 1;

        [BindProperty(SupportsGet = true)]
        public double? SearchWeight { get; set; }

        [BindProperty(SupportsGet = true)]
        public string? SearchTypeName { get; set; }

        public DAL.Models.PantherProfile? Profile { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            if (HttpContext.Session.GetInt32("RoleId") != 2)
                return Content("You have no permission to access this function!");

            if (!TempData.ContainsKey("SelectedPantherId"))
                return RedirectToPage("./Index");

            int id = (int)TempData["SelectedPantherId"];

            try
            {
                Profile = await _profile.GetByIdAsync(id);
                if (Profile == null)
                    return RedirectToPage("./Index");
                Id = id;
            }
            catch (System.Exception)
            {
                return RedirectToPage("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            if (HttpContext.Session.GetInt32("RoleId") != 2)
                return Content("You have no permission to access this function!");

            try
            {
                await _profile.DeleteProfileAsync(Id);
            }
            catch (System.Exception)
            {
                // Log the exception here if you have logging configured
                // You might want to add a TempData error message here
                // TempData["ErrorMessage"] = "Failed to delete the profile. Please try again.";
            }

            return RedirectToPage("/PantherProfile/Index");
        }
    }
}