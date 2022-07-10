using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using MyAutoService.Utility;

namespace MyAutoService.Pages.Service
{
    public class DetailsModel : PageModel
    {
        private ApplicationDbContext _db;

        public DetailsModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public ServiceHeader ServiceHeader { get; set; }
        public List<ServiceDetails> ServiceDetailss { get; set; }
        public CarServiceViewModel CarServiceVM { get; set; }
        public double CarservicTotal { get; set; }
        public IActionResult OnGet(int ServicId)
        {
            ServiceHeader = _db.ServiceHeaders.Include(u => u.Car).Include(s => s.Car.ApplicationUser)
                .FirstOrDefault(u => u.Id == ServicId);
            if (ServiceHeader == null)
                return NotFound();
            ServiceDetailss = _db.ServicDetalis.Where(d => d.ServiceHeaderId == ServicId).ToList();
            
               
            return Page();
        }
    }
}
