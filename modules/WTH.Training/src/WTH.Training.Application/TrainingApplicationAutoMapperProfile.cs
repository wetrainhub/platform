using WTH.Training.Awards;
using WTH.Training.AwardTypes;
using System;
using WTH.Training.Shared;
using Volo.Abp.AutoMapper;
using WTH.Training.AwardingOrganisations;
using AutoMapper;

namespace WTH.Training;

public class TrainingApplicationAutoMapperProfile : Profile
{
    public TrainingApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */

        CreateMap<AwardingOrganisation, AwardingOrganisationDto>();

        CreateMap<AwardType, AwardTypeDto>();

        CreateMap<AwardType, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));

        CreateMap<Award, AwardDto>();
        CreateMap<AwardWithNavigationProperties, AwardWithNavigationPropertiesDto>();
        CreateMap<AwardingOrganisation, LookupDto<Guid>>().ForMember(dest => dest.DisplayName, opt => opt.MapFrom(src => src.Name));
    }
}