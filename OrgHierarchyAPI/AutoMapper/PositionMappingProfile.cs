using AutoMapper;
using OrgHierarchyAPI.DTOs;
using OrgHierarchyAPI.Models;

namespace OrgHierarchyAPI.AutoMapper
{
    public class PositionMappingProfile:Profile
    {
        public PositionMappingProfile()
        {
            CreateMap<Position, PositionDto>().ReverseMap();
            CreateMap<Position, PositionCreateUpdateDto>().ReverseMap();
            CreateMap<Position, PositionTreeDto>()
                .ForMember(dest => dest.Children, opt => opt.MapFrom(src => src.Children));
        }
    }
}
