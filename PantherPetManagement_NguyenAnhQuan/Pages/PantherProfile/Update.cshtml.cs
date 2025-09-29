using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using BLL.Service;
using BLL.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace PantherPetManagement_NguyenAnhQuan.Pages.PantherProfile
{
    public class UpdateModel : PageModel
    {
        private readonly IProfileService _profile;
        //private readonly IHubContext<ProfileHub> _hubContext;

        public UpdateModel(IProfileService profile/*, IHubContext<ProfileHub> hubContext*/)
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

            if (!TempData.ContainsKey("SelectedPantherId"))
                return RedirectToPage("./Index");

            int id = (int)TempData["SelectedPantherId"];

            try
            {
                var profile = await _profile.GetByIdAsync(id);
                if (profile == null)
                    return RedirectToPage("./Index");

                Input = new ProfileDto
                {
                    PantherProfileId = profile.PantherProfileId,
                    PantherTypeId = profile.PantherTypeId,
                    PantherName = profile.PantherName ?? string.Empty,
                    Weight = profile.Weight,
                    Characteristics = profile.Characteristics ?? string.Empty,
                    Warning = profile.Warning ?? string.Empty,
                    ModifiedDate = profile.ModifiedDate = DateTime.Now
                };

                var PantherTypes = await _profile.GetAllPantherTypesAsync();
                TypeItems = new SelectList(PantherTypes, "PantherTypeId", "PantherTypeName");
            }
            catch (System.Exception)
            {
                return RedirectToPage("./Index");
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
                    TypeItems = new SelectList(new object[] { }, "Value", "Text");
                }
                return Page();
            }

            try
            {
                await _profile.UpdateProfileAsync(Input.PantherProfileId, Input);
                var updatedProfile = await _profile.GetByIdAsync(Input.PantherProfileId);
                //await _hubContext.Clients.All.SendAsync("RefreshList");
                return RedirectToPage("./Index");
            }
            catch (System.Exception)
            {
                ModelState.AddModelError(string.Empty, "An error occurred while updating the profile. Please try again.");

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