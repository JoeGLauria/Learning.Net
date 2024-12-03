using AutoMapper;
using NZWalk.API.Models.Domain;
using NZWalk.API.Models.DTOs;

namespace NZWalk.API.Mappings
{
    public class MappingProfiles:Profile
    {
        public MappingProfiles()
        {
            CreateMap<Region, RegionDTO>().ReverseMap();  
            CreateMap<AddRegionRequestDto, Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
        }
    }
}
