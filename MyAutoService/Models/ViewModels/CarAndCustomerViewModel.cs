namespace MyAutoService.Models.ViewModels
{
    public class CarAndCustomerViewModel
    {
        public ApplicationUser  ApplicationUser { get; set; }
        public IEnumerable<Car> Cars { get; set; }
        public PageingInfo Pageing { get; set; }
    }
}
