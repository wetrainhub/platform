using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace WTH.Training.AwardTypes
{
    public partial interface IAwardTypesAppService : IApplicationService
    {

        Task<PagedResultDto<AwardTypeDto>> GetListAsync(GetAwardTypesInput input);

        Task<AwardTypeDto> GetAsync(Guid id);

        Task DeleteAsync(Guid id);

        Task<AwardTypeDto> CreateAsync(AwardTypeCreateDto input);

        Task<AwardTypeDto> UpdateAsync(Guid id, AwardTypeUpdateDto input);
    }
}