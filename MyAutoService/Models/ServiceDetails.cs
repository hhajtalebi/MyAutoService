using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MyAutoService.Models
{
    public class ServiceDetails
    {
        [Key]
        public int Id { get; set; }


        public int ServiceHeaderId { get; set; }

        [ForeignKey("ServiceHeaderId")]
        public virtual ServiceHeader Header { get; set; }


        [Display(Name = "سرویس")]
        public int ServiceTypeId { get; set; }


        [ForeignKey("ServiceTypeId")]

        public virtual ServicType ServiceType { get; set; }

        [Display(Name = "قیمت سرویس ")]

        public double ServicePrice { get; set; }
        [Display(Name = "نام سرویس")]

        public string ServiceName { get; set; }

    }
}
