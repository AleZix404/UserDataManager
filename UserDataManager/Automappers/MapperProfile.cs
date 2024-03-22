using AutoMapper;
using UserDataManager.EntityFramework.DTO;
using UserDataManager.EntityFramework.Models;

namespace UserDataManager.Automappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<UserData.UserDataResponse, UserDataDTO>();
            CreateMap<UserDataInsertDTO, UserData.UserDataResponse>();//repo

            CreateMap<UserDataInsertDTO, UserDataDTO>();
            CreateMap<AddressDataInsertDTO, UserData.Address>();
            CreateMap<UserData.Address, AddressDTO>();

            CreateMap<UserDataUpdateDTO, UserData.UserDataResponse>();
            
            CreateMap<UserDataDTO, UserData.UserDataResponse>();
        }
    }
}
