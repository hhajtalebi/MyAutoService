using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Service
{
    public class HistoryModel : PageModel
    {
        ApplicationDbContext context;

        public HistoryModel(ApplicationDbContext context)
        {
            this.context = context;
        }
        [BindProperty]
        public List<ServiceHeader> ServiceHeaders { get; set; }
        public string UserID { get; set; }
        public void OnGet(int CarId)
        {
            ServiceHeaders = context.ServiceHeaders.Include(c => c.Car).Include(u => u.Car.ApplicationUser)
                .Where(U => U.CarId == CarId).ToList();
#pragma warning disable CS8602
            UserID=context.Cars.Where(u=>u.Id==CarId).FirstOrDefault().UserID;
#pragma warning restore CS8602

        }
    }
}
