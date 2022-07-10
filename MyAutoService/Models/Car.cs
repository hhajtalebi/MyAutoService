using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyAutoService.Models
{
    public class Car
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("نام")]
        [Required(ErrorMessage = "لطفا {0}را وارد کنید")]
        [MaxLength(200)]
        public string Name { get; set; }
        [DisplayName("مدل")]
        [MaxLength(200)]
        public string Model { get; set; }
        [DisplayName("رنگ")]
        [MaxLength(100)]
        public string color { get; set; }
        [DisplayName("سال")]
        [MaxLength(15)]
        public string Year { get; set; }
        [DisplayName("تصویر")]
        [MaxLength(50)]
        public string ImageName  { get; set; }

         public string UserID { get; set; }
        [ForeignKey("UserID")]
        public virtual ApplicationUser ApplicationUser { get; set; }
        public List<ServicesShoppingCart> ServicesShoppingCart { get; set; }



    }
}
