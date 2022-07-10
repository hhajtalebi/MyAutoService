using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;

namespace MyAutoService.Pages.Service
{
    public class CreateModel : PageModel
    {
        private ApplicationDbContext _db;

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public CarServiceViewModel CarServiceVM { get; set; }
        public async Task<IActionResult> OnGet(int CarId)
        {
            ///یک نمونه از مدل ساختیم
            CarServiceVM = new CarServiceViewModel()
            {
                Car = await _db.Cars.Include(u => u.ApplicationUser).FirstOrDefaultAsync(s => s.Id == CarId),
                ServiceHeader = new ServiceHeader()

            };
            ///سبد خرید رو بدست آوردیم
            List<string> LstServiceTypeInShoppingCard = _db.ServicesShoppingCarts
                .Include(c => c.ServiceType)
                .Where(C => C.CarId == CarId)
                .Select(c => c.ServiceType.Name).ToList();
            ///کوئری برای سرویس هایی که در سبد خرید نیستند را آوردیم
            IQueryable<ServicType> lstService = from s in _db.ServicType
                                                where !(LstServiceTypeInShoppingCard.Contains(s.Name))
                                                select s;

            ////لیست سرویس ها را داخل servictypelist قرار دادیم
            CarServiceVM.ServicTypeslList = lstService.ToList();
            /////سبد خرید را از بانک پرکردیم که تا الان چه سرویس هایی انحام شده
            CarServiceVM.ServicesShoppingCarts = _db.ServicesShoppingCarts.Include(c => c.ServiceType)
                .Where(c => c.CarId == CarId).ToList();
            ////جمع اولیه قیمت را صفر دادیم
            CarServiceVM.ServiceHeader.TotalPrice = 0;
            ////محاسبه قیمت سبد خرید با استفاده از حلقه
            foreach (var itemCart in CarServiceVM.ServicesShoppingCarts)
            {
                CarServiceVM.ServiceHeader.TotalPrice += itemCart.ServiceType.Price;
            }


            return Page();
        }
        public async Task<IActionResult> OnPost()
        {
                CarServiceVM.ServiceHeader.DateAdded = DateTime.Now;
                CarServiceVM.ServicesShoppingCarts = _db.ServicesShoppingCarts.Include(c => c.ServiceType)
                    .Where(c => c.CarId == CarServiceVM.Car.Id).ToList();
                foreach (var shopping in CarServiceVM.ServicesShoppingCarts)
                {
                    CarServiceVM.ServiceHeader.TotalPrice += shopping.ServiceType.Price;

                }

                CarServiceVM.ServiceHeader.CarId = CarServiceVM.Car.Id;
                _db.ServiceHeaders.Add(CarServiceVM.ServiceHeader);
                await _db.SaveChangesAsync();

                foreach (var shopping in CarServiceVM.ServicesShoppingCarts)
                {
                    ServiceDetails details = new ServiceDetails()
                    {
                        ServiceHeaderId = CarServiceVM.ServiceHeader.Id,
                        ServiceName = shopping.ServiceType.Name,
                        ServicePrice = shopping.ServiceType.Price,
                        ServiceTypeId = shopping.ServiceTypeId
                    };
                    _db.ServicDetalis.Add(details);
                }
                _db.ServicesShoppingCarts.RemoveRange(CarServiceVM.ServicesShoppingCarts);
                await _db.SaveChangesAsync();

                return RedirectToPage("/Cars/Index", new { userId = CarServiceVM.Car.UserID });
           

           
        }
        public async Task<IActionResult> OnPostAddToCart()
        {
            ServicesShoppingCart shopping = new ServicesShoppingCart()
            {
                CarId = CarServiceVM.Car.Id,
                ServiceTypeId = CarServiceVM.ServiceDetails.ServiceTypeId
            };
            _db.ServicesShoppingCarts.Add(shopping);
            await _db.SaveChangesAsync();
            return RedirectToPage("Create", new {CarId = CarServiceVM.Car.Id});

        }

        public async Task<IActionResult> OnPostRemoveFromCart(int ServiceTypeId)
        {
            ServicesShoppingCart shoppingcart =_db.ServicesShoppingCarts.First(s => s.CarId == CarServiceVM.Car.Id && ServiceTypeId == ServiceTypeId);
            _db.Remove(shoppingcart);
            await _db.SaveChangesAsync();
            return RedirectToPage("Create", new { CarId = CarServiceVM.Car.Id });

        }

       
    }
}
