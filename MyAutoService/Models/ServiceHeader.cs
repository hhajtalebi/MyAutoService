using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyAutoService.Models
{
    public class ServiceHeader
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "کیلومتر")]
        public double Miles { get; set; }
        [Required]
        [Display(Name = " قیمت کل")]

        public double TotalPrice { get; set; }

        [Display(Name = "توضیحات")]

        public string? Description { get; set; }

        [Required]

        [DisplayFormat(DataFormatString = "yyyy/MM/dd")]
        [Display(Name = "تاریخ ثبت")]

        public DateTime DateAdded { get; set; }


        public int CarId { get; set; }


        [ForeignKey("CarId")]

        public virtual Car Car { get; set; }

    }
}
