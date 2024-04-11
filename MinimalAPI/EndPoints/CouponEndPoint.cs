using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalAPI.Model;
using MinimalAPI.Repository.Contract;
using MinimalAPI.Utilities;

namespace MinimalAPI.EndPoints
{
    public static class CouponEndPoint
    {
        public static void CouponEndPoints(this WebApplication app)
        {
            // Add Coupon
            app.MapPost("/api/coupons", AddCoupon).WithName("CreateCoupon")
            .Accepts<AddCouponModel>("application/json")
            .Produces<APIResponse>(200)
            .Produces(400);

            //  Get all Coupons
            app.MapGet("/api/coupons", GetCoupons).WithName("GetCoupons")
            .Produces<APIResponse>(200);
            //.RequireAuthorization("AdminOnly");

            //GET COUPON BY ID
            app.MapGet("/api/coupons/{id:int}", GetCoupon).WithName("GetCoupon")
            .Produces<APIResponse>(200);

            //  UPDATE COUPON BY ID
            app.MapPut("/api/coupons/{id:int}", UpdateCoupon).WithName("UpdateCoupon")
            .Produces<APIResponse>(200);

            // DELETE COUPON BY ID
            app.MapDelete("/api/coupons/{id:int}", DeleteCoupon).WithName("DeleteCoupon")
            .Produces<APIResponse>(200);
        }

        public static APIResponse AddCoupon(ICouponRepository coupleRepository, [FromBody] AddCouponModel addCoupon)
        {
            var response = new APIResponse();

            if (string.IsNullOrEmpty(addCoupon.Name))
            {
                return APIStatusResponse.GetBadResponse("Invalid coupon name");
            }

            if (coupleRepository.GetCoupons().Any(x => x.Name.ToLower() == addCoupon.Name.ToLower()))
            {
                return APIStatusResponse.GetBadResponse("Coupon is already exists..");
            }

            var result = coupleRepository.AddCoupon(addCoupon);

            return APIStatusResponse.GetCreateResponse(result);
        }

        [Authorize(Roles = "AdminOnly")]
        public static APIResponse GetCoupons(ICouponRepository coupleRepository)
        {
            return APIStatusResponse.GetOkResponse(coupleRepository.GetCoupons());
        }

        public static APIResponse GetCoupon(ICouponRepository coupleRepository, int id)
        {
            return APIStatusResponse.GetOkResponse(coupleRepository.GetCoupon(id));
        }

        public static APIResponse UpdateCoupon(ICouponRepository coupleRepository, IMapper mapper, int id, [FromBody] AddCouponModel updateCoupon)
        {
            if (id == 0 && string.IsNullOrEmpty(updateCoupon.Name))
            {
                return APIStatusResponse.GetBadResponse("Invalid coupon name");
            }

            if (coupleRepository.GetCoupons().Any(x => x.Name.ToLower() == updateCoupon.Name.ToLower() && id != x.Id))
            {
                return APIStatusResponse.GetBadResponse("Coupon is already exists..");
            }

            var result = coupleRepository.UpdateCoupon(id, updateCoupon);

            return APIStatusResponse.GetOkResponse(result);
        }

        public static APIResponse DeleteCoupon(ICouponRepository coupleRepository, int id)
        {
            var result = coupleRepository.DeleteCoupon(id);

            return APIStatusResponse.GetOkResponse(result);
        }
    }
}