﻿namespace MyAutoService.Models.ViewModels
{
    public class CarServiceViewModel
    {
        public Car Car { get; set; }
        public ServiceHeader ServiceHeader { get; set; }
        public ServiceDetails ServiceDetails { get; set; }
        public List<ServicType> ServicTypeslList { get; set; }
        public List<ServicesShoppingCart> ServicesShoppingCarts { get; set; }

    }
}
