using System.Collections.Generic;
namespace MyAutoService.Models.ViewModels
{
    public class UserListViewModel
    {
        public List<ApplicationUser> ApplicationUserlist { get; set; }
        public PageingInfo PagingInfo { get; set; }

    }
}
