using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models.ViewModels;

namespace MyAutoService.ViewComponents
{
    public class LoggedInUserViewComponent:ViewComponent

    {
        private ApplicationDbContext _DB;

        public LoggedInUserViewComponent(ApplicationDbContext db)
        {
            _DB = db;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            LoggedInUserViewModel logged = new LoggedInUserViewModel()
            {
                Name = _DB.User.First(u => User.Identity != null && u.Email ==User.Identity.Name).Name,
                ShoppingCart = _DB.ServicesShoppingCarts
                    .Include(c => c.Car).ThenInclude(c => c.ApplicationUser)
                    .Include(c => c.ServiceType)
                    .Where(u => User.Identity != null && u.Car.ApplicationUser.Email == User.Identity.Name)
                    .ToList()
            };
            return View("~/Pages/Shared/Components/LoggedInUser.cshtml",logged);
        }
    }
}
