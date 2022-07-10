using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace MyAutoService.Models
{
    public class ApplicationUser:IdentityUser
    {
     
        [MaxLength(200)]
        [DisplayName("نام :")]
        public string Name { get; set; }
        [MaxLength(200)]
        [DisplayName("آدرس :")]
        public string Address { get; set; }
        [MaxLength(200)]
        [DisplayName("شهر :")]
        public string City { get; set; }
        [MaxLength(200)]
        [DisplayName("کد پستی :")]
        public string PostalCode { get; set; }
        [DisplayName("ایمیل")]
        public override string Email
        {
            get { return base.Email;}
            set { base.Email = value; }
        }
        [DisplayName("تلفن")]
        public override string PhoneNumber {
            get { return base.PhoneNumber; }
            set { base.PhoneNumber = value; }
        }

        public virtual List<Car> car { get; set; }
    }
}
