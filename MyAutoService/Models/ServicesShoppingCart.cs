using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MyAutoService.Models
{
    public class ServicesShoppingCart
    {
        [Key]
        public int Id { get; set; }


        public int ServiceTypeId { get; set; }


        [ForeignKey("ServiceTypeId")]
        public virtual ServicType ServiceType { get; set; }
        public int CarId { get; set; }


        [ForeignKey("CarId")]

        public virtual Car Car { get; set; }
    }
}
