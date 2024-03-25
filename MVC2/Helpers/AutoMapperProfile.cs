using AutoMapper;
using MVC2.Data2;
using MVC2.ViewModels;

namespace MVC2.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<RegisterVM, Khachhang>().ReverseMap();
            //    .ForMember(kh=>kh.Fullname, option=>option.MapFrom(RegisterVM => 
            //        RegisterVM.Fullname))
            //    .ReverseMap();
            CreateMap<LoginVM, Khachhang>()
                .ForMember(kh=>kh.Email, lg=>lg.MapFrom(lo=>lo.username))
                .ForMember(kh => kh.Pwd, lg => lg.MapFrom(lo => lo.password))
                .ReverseMap();
            CreateMap<Product,CartItem>().ReverseMap();
            CreateMap<OrderVM,Order>().ReverseMap();
            CreateMap<CartItem,ChitietBill>().ReverseMap();
        }
    }
}
