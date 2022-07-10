using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Users
{
    public class DetaileModel : PageModel
    {
        private ApplicationDbContext _db;

        public DetaileModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public ApplicationUser ApplicationUser { get; set; }
        public async Task<IActionResult> OnGet(string id)
        {
            if (id.Trim().Length == 0)
                return NotFound();

            ApplicationUser = _db.User.FirstOrDefault(u => u.Id == id);
            if (ApplicationUser == null)
                return NotFound();


            return Page();
        }
    }
}
