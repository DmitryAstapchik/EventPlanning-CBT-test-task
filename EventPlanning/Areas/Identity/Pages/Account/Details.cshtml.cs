using EventPlanning.Data;
using EventPlanning.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace EventPlanning.Areas.Identity.Pages.Account
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        public DetailsModel(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }
        [BindProperty]
        public UserDetails Input { get; set; }
        public async Task OnGetAsync()
        {
            var identityUser = await _userManager.GetUserAsync(User);
            Input ??= new UserDetails();
            var details = _context.UsersDetails.SingleOrDefault(x => x.Id == Guid.Parse(identityUser.Id));
            if (details != null)
            {
                Input.Email = details.Email;
                Input.FirstName = details.FirstName;
                Input.LastName = details.LastName;
                Input.PhoneNumber = details.PhoneNumber;
            }
        }

        public async Task OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var identityUser = await _userManager.GetUserAsync(User);
                var details = _context.UsersDetails.SingleOrDefault(x => x.Id == Guid.Parse(identityUser.Id));
                if (details == null)
                {
                    _ = _context.UsersDetails.Add(new UserDetails
                    {
                        Id = Guid.Parse(identityUser.Id),
                        Email = Input.Email,
                        FirstName = Input.FirstName,
                        LastName = Input.LastName,
                        PhoneNumber = Input.PhoneNumber,
                    });
                }
                else
                {
                    details.Email = Input.Email;
                    details.FirstName = Input.FirstName;
                    details.LastName = Input.LastName;
                    details.PhoneNumber = Input.PhoneNumber;
                }
                await _context.SaveChangesAsync();
                ViewData["saved"] = "Saved.";
            }
        }
    }
}
