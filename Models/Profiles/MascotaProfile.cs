using AutoMapper;
using BE_CRUDNET.Models.DTO;

namespace BE_CRUDNET.Models.Profiles
{
    public class MascotaProfile : Profile
    {
        public MascotaProfile()
        {
            CreateMap<MascotaDTO,Mascota>().ReverseMap();
        }
    }
}
