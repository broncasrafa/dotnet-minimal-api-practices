using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Api.Data.Context;
using Api.Data.Repository.Interface;
using Api.Models.Entity;

namespace Api.Data.Repository.Implementation;

public class CouponRepository(ApplicationDbContext _context, ILogger<CouponRepository> _logger) : ICouponRepository
{
    public async Task<IEnumerable<Coupon>> GetAllAsync()
        => await _context.Coupons.ToListAsync();

    public async Task<Coupon> FindByIdAsync(int id)
        => await _context.Coupons.FindAsync(id);

    public async Task<IEnumerable<Coupon>> FindAsync(Expression<Func<Coupon, bool>> predicate)
        => await _context.Coupons.Where(predicate).ToListAsync();

    public async Task<Coupon> SingleOrDefaultAsync(Expression<Func<Coupon, bool>> predicate)
        => await _context.Coupons.SingleOrDefaultAsync(predicate);

    public async Task<Coupon> FirstOrDefaultAsync(Expression<Func<Coupon, bool>> predicate)
    {
        var data = await _context.Coupons.FirstOrDefaultAsync(predicate);
        return data;
    }

    public async Task<Coupon> InsertAsync(Coupon entity)
    {
        await _context.Set<Coupon>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Coupon> UpdateAsync(Coupon entity)
    {
        _context.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(Coupon entity)
    {
        _context.Remove(entity);
        await _context.SaveChangesAsync();
    }
}
