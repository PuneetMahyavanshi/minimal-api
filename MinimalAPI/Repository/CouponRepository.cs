using AutoMapper;
using MinimalAPI.Model;
using MinimalAPI.Repository.Contract;

namespace MinimalAPI.Repository
{
    public class CouponRepository : ICouponRepository
    {
        private readonly List<Coupon> _coupons;
        private readonly IMapper _mapper;

        public CouponRepository(IMapper mapper, List<Coupon> coupon)
        {
            _coupons = coupon;
            _mapper = mapper;
        }

        public string AddCoupon(AddCouponModel addCoupon)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(addCoupon);
                coupon.Id = _coupons.Count + 1;
                coupon.Created = DateTime.Now;
                _coupons.Add(coupon);

                return "Data added successfully";
            }
            catch
            {
                return "Failed to add data";
            }
        }

        public string UpdateCoupon(int id, AddCouponModel updateCoupon)
        {
            try
            {
                var coupon = _coupons.FirstOrDefault(x => x.Id == id);
                coupon = _mapper.Map(updateCoupon, coupon);
                coupon.LastUpdated = DateTime.Now;

                return "Data updated successfully";
            }
            catch
            {
                return "Failed to update data";
            }
        }

        public string DeleteCoupon(int id)
        {
            try
            {
                var coupon = _coupons.FirstOrDefault(x => x.Id == id);

                _coupons.Remove(coupon);

                return "Data deleted successfully";
            }
            catch
            {
                return "Failed to delete data";
            }
        }

        public Coupon GetCoupon(int id)
        {
            return _coupons.FirstOrDefault(x => x.Id == id);
        }

        public List<Coupon> GetCoupons()
        {
            return _coupons;
        }
    }
}