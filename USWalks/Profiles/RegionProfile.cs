using AutoMapper;

namespace USWalks.Profile
{
    public class RegionProfile : AutoMapper.Profile
    {
        public RegionProfile()
        {
            CreateMap<Models.Domain.Region, Models.DTO.Region>().ReverseMap();
        }
    }
}
