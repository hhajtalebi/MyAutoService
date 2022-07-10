using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using MyAutoService.Utility;

namespace MyAutoService.Pages.Cars
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty] public CarAndCustomerViewModel CarAndCustomerViewModel { get; set; }

        public async Task<IActionResult> OnGet(string userId,int PageId=1)
        {
            if (userId == null)
            {
             
                 var claimsIdentity = (ClaimsIdentity) User.Identity;
#pragma warning disable CS8602
                 var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
#pragma warning restore CS8602

#pragma warning disable CS8602
                userId =claim.Value;
#pragma warning restore CS8602

            }

            CarAndCustomerViewModel = new CarAndCustomerViewModel()
            {
                Cars = await _db.Cars.Where(c => c.UserID == userId).ToListAsync(),
                ApplicationUser = await _db.User.FirstOrDefaultAsync(u=>u.Id==userId)

            };
            StringBuilder param = new StringBuilder();
            param.Append("/Cars?PageId=:");

            var count = CarAndCustomerViewModel.Cars.Count();
            CarAndCustomerViewModel.Pageing = new PageingInfo()
            {
                CurrentPage = PageId,
                ItemPerPage = SD.PageingCarCount,
                TotalItems = count,
                UrlParams = param.ToString()

            };
            CarAndCustomerViewModel.Cars = CarAndCustomerViewModel.Cars.OrderBy(s => s.Name)
                .Skip((PageId - 1) * SD.PageingCarCount).Take(SD.PageingCarCount).ToList();

            return Page();
        }
    }
}
