namespace MyAutoService.Utility
{
    public static class PriceConvert
    {
        public static string ToToman(this Double value)
        {
            return value.ToString("#,0 تومان");
        }
    }
}
