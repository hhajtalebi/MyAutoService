using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MyAutoService.Data;
using MyAutoService.Models;

namespace MyAutoService.Pages.Users
{
    public class EditModel : PageModel
    {
        private ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EditModel(ApplicationDbContext db, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [BindProperty] public ApplicationUser ApplicationUser { get; set; }
        [BindProperty]
        public string SelectedRole { get; set; }

        public SelectList Roles { get; set; }

        public async Task<IActionResult> OnGet(string id)
        {
            ////آیدی را مقایسه می کند که خالی نباشد
            if (id.Trim().Length == 0)
                return NotFound();
            /////////آیدی کاربری که انتخاب شده را در بانک جستجو می کند
            ApplicationUser = _db.User.FirstOrDefault(u => u.Id == id);
            if (ApplicationUser == null)
                return NotFound();
            ////نقش کاربر را به وسیله آیدی پیدا می کند
            var userRoles = _userManager.GetRolesAsync(new IdentityUser(){Id = ApplicationUser.Id}).Result;//(ClaimsIdentity) User.Identity;
            ////نقش کاربر را به سلکشن می دهد
            Roles = new SelectList(_roleManager.Roles, "Name", "Name",userRoles.First());
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            
            //////کوئری به بانک زده و کاربر با ایدی که لیست داره مقایسه میکنه
            var  userInDb = _db.User.FirstOrDefault(u => u.Id == ApplicationUser.Id);
            /////اگر کاربر که برگشته با دیتابیس یکی نبود نات فان را ر می گرداند
            if (userInDb == null)
                return NotFound();
            ///////اگر کاربر درست بود فیلد های زیر را آبدیت می کند
            userInDb.Name = ApplicationUser.Name;
            userInDb.Address = ApplicationUser.Address;
            userInDb.PhoneNumber = ApplicationUser.PhoneNumber;
            userInDb.PostalCode = ApplicationUser.PostalCode;
            userInDb.City = ApplicationUser.City;
            ////پیدا کردن نقش کاربر درسیستم 
            var userRoles = _userManager.GetRolesAsync(new IdentityUser() { Id = ApplicationUser.Id }).Result;
            /////////اگر نقش کاربر تغییر کرده نقش فبلی را از سیستم پاک کند و نقش جدید را به کاربر بدهد
            if(SelectedRole!=userRoles.FirstOrDefault())
            {
                ////پاک کردن نقش قبلی کاربر
                await _userManager.RemoveFromRoleAsync(new IdentityUser() { Id = ApplicationUser.Id },
                    userRoles.FirstOrDefault());
                /////افزودن نقش جدید کاربر
                await _userManager.AddToRoleAsync(new IdentityUser() { Id = ApplicationUser.Id }, SelectedRole);
            }

           _db.Update(userInDb);
           await _db.SaveChangesAsync();

           return RedirectToPage("Index");
        }
    }
}
