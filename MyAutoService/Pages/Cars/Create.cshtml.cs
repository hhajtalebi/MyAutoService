#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Cars
{
    public class CreateModel : PageModel
    {
        private readonly MyAutoService.Data.ApplicationDbContext _context;

        public CreateModel(MyAutoService.Data.ApplicationDbContext context)
        {
            _context = context;
        }
        [BindProperty]
        public Car Car { get; set; }
        public IActionResult OnGet(string userid )
        {
            Car = new Car();

         ////   if (userid == null)
            {
              ///  return NotFound();
                /// var claimsIdentity = (ClaimsIdentity)User.Identity;
                ///  var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

                ///  userid = claim.Value;

            }

            Car.UserID = userid;
            return Page();
        }

        [BindProperty]
        public IFormFile ImgUp { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            

            if (ImgUp !=null)
            {
                Car.ImageName = Guid.NewGuid().ToString() + Path.GetExtension(ImgUp.FileName);
                string savePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/CarImages", Car.ImageName);
                using (var fileStream = new FileStream(savePath, FileMode.Create))
                {
                    ImgUp.CopyTo(fileStream);
                }
            }
            _context.Cars.Add(Car);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
