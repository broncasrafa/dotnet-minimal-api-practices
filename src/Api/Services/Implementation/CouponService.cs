using System.Linq.Expressions;
using Api.Data.Repository.Interface;
using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Models.Entity;
using Api.Models.Exceptions;
using Api.Models.Extensions;
using Api.Services.Interface;
using AutoMapper;

namespace Api.Services.Implementation;

public class CouponService(ICouponRepository _repository, ILogger<CouponService> _logger, IMapper _mapper) : ICouponService
{
    public async Task<IEnumerable<CouponResponse>> GetAllAsync()
    {
        var data = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<CouponResponse>>(data);
    }
    public async Task<CouponResponse> FindByIdAsync(int id)
    {
        var data = await _repository.FindByIdAsync(id)
                                    .OrElseThrowsAsync(new CouponNotFoundException(id));
        return _mapper.Map<CouponResponse>(data);
    }
    public async Task<CouponResponse> FindByAsync(Expression<Func<Coupon, bool>> predicate)
    {
        var data = await _repository.FirstOrDefaultAsync(predicate);  
        return _mapper.Map<CouponResponse>(data);
    }
    public async Task<CouponResponse> InsertAsync(CouponCreateRequest request)
    {
        var entity = _mapper.Map<Coupon>(request);
        var newCoupon = await _repository.InsertAsync(entity);

        return _mapper.Map<CouponResponse>(newCoupon);
    }
    public async Task<CouponResponse> UpdateAsync(CouponUpdateRequest request) 
    {
        var currentCoupon = await _repository.FindByIdAsync(request.Id)
                                             .OrElseThrowsAsync(new CouponNotFoundException(request.Id));
        currentCoupon.Name = request.Name;
        currentCoupon.Percent = request.Percent;
        currentCoupon.UpdatedAt = DateTime.UtcNow;
        await _repository.UpdateAsync(currentCoupon);

        return _mapper.Map<CouponResponse>(currentCoupon);
    }
    public async Task DeleteAsync(int id)
    {
        var currentCoupon = await _repository.FindByIdAsync(id)
                                             .OrElseThrowsAsync(new CouponNotFoundException(id));
        // apenas inativa o cupom
        //currentCoupon.IsActive = false;
        //await _repository.UpdateAsync(currentCoupon);
        //return _mapper.Map<CouponResponse>(currentCoupon);
        // ou remove
        await _repository.DeleteAsync(currentCoupon);        
    }
}
