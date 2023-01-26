using AutoMapper;
using Entities.DataTransferObjects;
using Entities.Models;

namespace Entities
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Owner, OwnerDTO>().ReverseMap();
            CreateMap<Owner, OwnerWithAccountsDTO>().ReverseMap();
            CreateMap<Account, AccountDTO>().ReverseMap();
            CreateMap<OwnerModelDTO, Owner>().ReverseMap();
        }
    }
}
