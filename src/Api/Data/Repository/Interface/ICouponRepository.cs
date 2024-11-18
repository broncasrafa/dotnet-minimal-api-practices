using System.Linq.Expressions;
using Api.Models.Entity;

namespace Api.Data.Repository.Interface;

public interface ICouponRepository
{
    Task<IEnumerable<Coupon>> GetAllAsync();
    Task<Coupon> FindByIdAsync(int id);
    Task<Coupon> InsertAsync(Coupon entity);
    Task<Coupon> UpdateAsync(Coupon entity);
    Task DeleteAsync(Coupon entity);

    Task<IEnumerable<Coupon>> FindAsync(Expression<Func<Coupon, bool>> predicate);
    Task<Coupon> SingleOrDefaultAsync(Expression<Func<Coupon, bool>> predicate);
    Task<Coupon> FirstOrDefaultAsync(Expression<Func<Coupon, bool>> predicate);
}
