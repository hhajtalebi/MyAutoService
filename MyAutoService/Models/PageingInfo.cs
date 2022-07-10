namespace MyAutoService.Models
{
    public class PageingInfo
    {
        public int  TotalItems { get; set; }
        public int ItemPerPage { get; set; }
        public  int CurrentPage { get; set; }
       
        public int TotalPages => (int) Math.Ceiling((decimal) TotalItems / ItemPerPage);
        public string UrlParams { get; set; }
    }

}
