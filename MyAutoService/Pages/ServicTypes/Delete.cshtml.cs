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

namespace MyAutoService.Pages.ServicTypes
{
    public class DeleteModel : PageModel
    {
        private readonly MyAutoService.Data.ApplicationDbContext _context;

        public DeleteModel(MyAutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ServicType ServicType { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServicType = await _context.ServicType.FirstOrDefaultAsync(m => m.Id == id);

            if (ServicType == null)
            {
                return NotFound();
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ServicType = await _context.ServicType.FindAsync(id);

            if (ServicType != null)
            {
                _context.ServicType.Remove(ServicType);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
