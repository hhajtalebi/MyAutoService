using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MyAutoService.Data;
using MyAutoService.Models;
using NuGet.Protocol;

namespace MyAutoService.Pages
{
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        public List<ServicType> ServiceTypes { get; set; }
        public List<Car> Cars { get; set; }
        public void OnGet()
        {
            ServiceTypes = _db.ServicType.ToList();
            Cars= _db.Cars.ToList();
        }
    }
}