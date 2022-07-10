#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Cars
{
    public class DeleteModel : PageModel
    {
        private readonly MyAutoService.Data.ApplicationDbContext _context;

        public DeleteModel(MyAutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Car Car { get; set; }

        public async Task<IActionResult> OnGetAsync(int? CarId)
        {
            if (CarId == null)
            {
                return NotFound();
            }

            Car = await _context.Cars
                .Include(c => c.ApplicationUser).FirstOrDefaultAsync(m => m.Id == CarId);

            if (Car == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? CarId)
        {
            if (CarId == null)
            {
                return NotFound();
            }

            Car = await _context.Cars.FindAsync(CarId);

            if (Car != null)
            {
                _context.Cars.Remove(Car);
                string DeletePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                if (System.IO.File.Exists(DeletePath))
                {
                    System.IO.File.Delete(DeletePath);
                }
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
