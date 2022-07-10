using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.ServicTypes
{
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public ServicType ServicType { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }

        public IActionResult OnPost(ServicType servicType)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _db.ServicType.Add(servicType);
            _db.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
