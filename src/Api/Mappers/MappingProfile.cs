using Api.Models.DTO.Request;
using Api.Models.DTO.Response;
using Api.Models.Entity;
using AutoMapper;

namespace Api.Mappers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CouponCreateRequest, Coupon>().ReverseMap();
        CreateMap<CouponUpdateRequest, Coupon>().ReverseMap();
        CreateMap<Coupon, CouponResponse>().ReverseMap();

        CreateMap<RegisterRequest, LocalUser>().ReverseMap();
        CreateMap<LocalUser, UserResponse>().ReverseMap();
    }
}
