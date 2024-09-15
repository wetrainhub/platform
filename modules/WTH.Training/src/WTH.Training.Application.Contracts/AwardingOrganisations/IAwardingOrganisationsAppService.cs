using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WTH.Training.AwardingOrganisations
{
    public partial interface IAwardingOrganisationsAppService : IApplicationService
    {

        Task<PagedResultDto<AwardingOrganisationDto>> GetListAsync(GetAwardingOrganisationsInput input);

        Task<AwardingOrganisationDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AwardingOrganisationDto> CreateAsync(AwardingOrganisationCreateDto input);

        Task<AwardingOrganisationDto> UpdateAsync(Guid id, AwardingOrganisationUpdateDto input);
    }
}