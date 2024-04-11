using MinimalAPI.Model;

namespace MinimalAPI.Repository.Contract
{
    public interface ICouponRepository
    {
        string AddCoupon(AddCouponModel addCoupon);

        string UpdateCoupon(int id, AddCouponModel updateCoupon);

        string DeleteCoupon(int id);

        Coupon GetCoupon(int id);

        List<Coupon> GetCoupons();
    }
}
