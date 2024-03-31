using AutoMapper;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Automappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserData.UserDResp, UserDataDTO>();
            CreateMap<UserDataInsertDTO, UserData.UserDResp>();

            CreateMap<UserDataInsertDTO, UserDataDTO>();
            CreateMap<AddressDataInsertDTO, UserData.Address>();
            CreateMap<UserData.Address, AddressDTO>();

            CreateMap<UserDataUpdateDTO, UserData.UserDResp>();
            
            CreateMap<UserDataDTO, UserData.UserDResp>();
        }
    }
}
