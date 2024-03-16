using AutoMapper;
using MVC2.Data2;
using MVC2.ViewModels;

namespace MVC2.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile() {
            CreateMap<RegisterVM, Khachhang>();
            //    .ForMember(kh=>kh.Fullname, option=>option.MapFrom(RegisterVM => 
            //        RegisterVM.Fullname))
            //    .ReverseMap();
        }
    }
}
