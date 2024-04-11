namespace MinimalAPI.Model
{
    public static class CouponStore
    {
        public static List<Coupon> Coupons = new List<Coupon>()
        {
            new Coupon() { Id = 1, IsActive = true, Name = "DL500", Percent = 50, Created = DateTime.Now },
            new Coupon() { Id = 2, IsActive = true, Name = "DL400", Percent = 40, Created = DateTime.Now },
            new Coupon() { Id = 3, IsActive = true, Name = "DL300", Percent = 30, Created = DateTime.Now }
        };
    }
}