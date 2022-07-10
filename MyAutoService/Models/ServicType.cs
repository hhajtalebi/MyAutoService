using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MyAutoService.Models
{
    public class ServicType
    {

        public int Id { get; set; }
        [Required(ErrorMessage = "لطفا نام را وارد کنید") ]
        [MaxLength (500)]
        [DisplayName(displayName: "نام:")]
        public string Name { get; set; }
        [Required(ErrorMessage = "لطفا قیمت را وارد کنید")]
        [Display(Name = "قیمت :")]
        public double Price { get; set; }

       
    }
}
