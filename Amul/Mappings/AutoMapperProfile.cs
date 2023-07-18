using Amul.Models.Domain;
using Amul.Models.DTO;
using AutoMapper;

namespace Amul.Mappings
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Icecream, IcecreamGetDTO>().ReverseMap();
            CreateMap<Icecream, IcecreamSendDTO>().ReverseMap();
            CreateMap<Categories, CategoriesGetDTO>().ReverseMap();
            CreateMap<Categories, CategoriesSendDTO>().ReverseMap();
        }
    }
}
