using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using MyAutoService.Utility;

namespace MyAutoService.Pages.ServicTypes
{
    
    /// سطح دسترسی برای ادمین سیستم 
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }
        [BindProperty]
        public ServicListViewModel ServicListViewModel { get; set; }
        public async Task<IActionResult> OnGet(int pageId=1,string NameServic=null)
        {
            ServicListViewModel= new ServicListViewModel()
            {
                ServiceTypeslist = await _db.ServicType.ToListAsync()

            };

    

            

            #region ////search filter

            StringBuilder param = new StringBuilder();
            param.Append("/ServicTypes?pageId=:");

            param.Append($"NameServic=");
            if (NameServic != null)
            {
                param.Append(NameServic);
            }

            if (NameServic != null)
            {
                ServicListViewModel.ServiceTypeslist = _db.ServicType.Where(s => s.Name.Contains(NameServic)).ToList();
            }
            #endregion


         
            var count = ServicListViewModel.ServiceTypeslist.Count;
            ServicListViewModel.Pageing = new PageingInfo()
            {
                CurrentPage = pageId,
                ItemPerPage = SD.PagingServicCount,
                TotalItems = count,
                UrlParams = param.ToString()

            };
            ServicListViewModel.ServiceTypeslist = ServicListViewModel.ServiceTypeslist.OrderBy(s => s.Name)
                .Skip((pageId - 1) * SD.PagingServicCount).Take(SD.PagingServicCount).ToList();


           
       
            return Page();
        }
    }
}
