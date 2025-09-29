using BLL.Dtos;
using BLL.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PantherPetManagement_NguyenAnhQuan.Pages.PantherProfile
{
    public class CreateModel : PageModel
    {
        private readonly IProfileService _profile;
        //private readonly IHubContext<ProfileHub> _hubContext;

        public CreateModel(IProfileService profile/*, IHubContext<ProfileHub> hubContext*/)
        {
            _profile = profile;
            //_hubContext = hubContext;
        }

        [BindProperty]
        public ProfileDto Input { get; set; } = new ProfileDto();

        public SelectList TypeItems { get; set; } = new SelectList(new object[] { }, "Value", "Text");

        public async Task<IActionResult> OnGetAsync()
        {
            if (HttpContext.Session.GetInt32("AccountId") == null)
                return RedirectToPage("/Index");

            if (HttpContext.Session.GetInt32("RoleId") != 2)
                return Content("You have no permission to access this function!");

            try
            {
                var PantherTypes = await _profile.GetAllPantherTypesAsync();
                TypeItems = new SelectList(PantherTypes, "PantherTypeId", "PantherTypeName");
            }
            catch (System.Exception)
            {
                // Log the exception here if you have logging configured
                TypeItems = new SelectList(new object[] { }, "Value", "Text");
                // You might want to add an error message here
                // TempData["ErrorMessage"] = "Failed to load Panther types. Please refresh the page.";
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                try
                {
                    var PantherTypes = await _profile.GetAllPantherTypesAsync();
                    TypeItems = new SelectList(PantherTypes, "PantherTypeId", "PantherTypeName");
                }
                catch (System.Exception)
                {
                    // Log the exception here if you have logging configured
                    TypeItems = new SelectList(new object[] { }, "Value", "Text");
                }
                return Page();
            }

            try
            {
                var newProfileId = await _profile.CreateProfileAsync(Input);
                var newProfile = await _profile.GetByIdAsync(newProfileId);
                //await _hubContext.Clients.All.SendAsync("RefreshList");
                return RedirectToPage("./Index");
            }
            catch (System.Exception)
            {
                // Log the exception here if you have logging configured
                ModelState.AddModelError(string.Empty, "An error occurred while creating the profile. Please try again.");

                try
                {
                    var PantherTypes = await _profile.GetAllPantherTypesAsync();
                    TypeItems = new SelectList(PantherTypes, "PantherTypeId", "PantherTypeName");
                }
                catch (System.Exception)
                {
                    TypeItems = new SelectList(new object[] { }, "Value", "Text");
                }

                return Page();
            }
        }
    }
}
