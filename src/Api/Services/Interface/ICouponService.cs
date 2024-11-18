using System.Linq.Expressions;
using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Models.Entity;

namespace Api.Services.Interface;

public interface ICouponService
{
    Task<IEnumerable<CouponResponse>> GetAllAsync();
    Task<CouponResponse> FindByIdAsync(int id);
    Task<CouponResponse> FindByAsync(Expression<Func<Coupon, bool>> predicate);
    Task<CouponResponse> InsertAsync(CouponCreateRequest request);
    Task<CouponResponse> UpdateAsync(CouponUpdateRequest request);
    Task DeleteAsync(int id);
}
