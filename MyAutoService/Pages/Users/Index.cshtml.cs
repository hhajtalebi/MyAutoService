using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MyAutoService.Data;
using MyAutoService.Models;
using MyAutoService.Models.ViewModels;
using MyAutoService.Utility;

namespace MyAutoService.Pages.Users
{
    /// سطح دسترسی برای ادمین سیستم 
    [Authorize(Roles = SD.AdminEndUser)]
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        [BindProperty]
        public UserListViewModel UsersListViewModel { get; set; }
        public async Task<IActionResult> OnGet(int pageId = 1,string searchName=null,string searchEmail=null,string searchPhone=null)
        {
            UsersListViewModel = new UserListViewModel()
            {
                ApplicationUserlist = await _db.User.ToListAsync(),

            };
            ////???? ????? ???? ???? ?????
            StringBuilder param = new StringBuilder();
            param.Append("/Users?pageId=:");
            param.Append("&searchName=");
            if (searchName != null)
                param.Append(searchName);

            param.Append("&searchEmail=");
            if (searchEmail != null)
                param.Append(searchEmail);

            param.Append("&searchPhone=");
            if (searchPhone != null)
                param.Append(searchPhone);

            if (searchName != null || searchEmail != null || searchPhone != null)
            {
                UsersListViewModel.ApplicationUserlist = _db.User
                    .Where(u=>u.Name.Contains(searchName)||u.Email.Contains(searchEmail)||u.PhoneNumber.Contains(searchPhone)).ToList();
            }
            ///////////////////////
            ///
            /// ?? ??? ???? ???? 
            var count = UsersListViewModel.ApplicationUserlist.Count;
            UsersListViewModel.PagingInfo=new PageingInfo()
            {
                CurrentPage = pageId,
                ItemPerPage = SD.PagingUserCount,
                TotalItems = count,
                UrlParams = param.ToString()
            };
            UsersListViewModel.ApplicationUserlist = UsersListViewModel.ApplicationUserlist.OrderBy(u => u.Name)
                .Skip((pageId - 1) * SD.PagingUserCount)
                .Take(SD.PagingUserCount).ToList();
            ///////////////////////
            return Page();
        }
    }
}
