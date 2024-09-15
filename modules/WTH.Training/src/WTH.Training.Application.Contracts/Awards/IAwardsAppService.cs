using WTH.Training.Shared;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WTH.Training.Awards
{
    public partial interface IAwardsAppService : IApplicationService
    {

        Task<PagedResultDto<AwardWithNavigationPropertiesDto>> GetListAsync(GetAwardsInput input);

        Task<AwardWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id);

        Task<AwardDto> GetAsync(Guid id);

        Task<PagedResultDto<LookupDto<Guid>>> GetAwardTypeLookupAsync(LookupRequestDto input);

        Task<PagedResultDto<LookupDto<Guid>>> GetAwardingOrganisationLookupAsync(LookupRequestDto input);

        Task DeleteAsync(Guid id);

        Task<AwardDto> CreateAsync(AwardCreateDto input);

        Task<AwardDto> UpdateAsync(Guid id, AwardUpdateDto input);
    }
}