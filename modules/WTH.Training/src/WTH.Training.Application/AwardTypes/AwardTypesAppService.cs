using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using WTH.Training.Permissions;
using WTH.Training.AwardTypes;

namespace WTH.Training.AwardTypes
{

    [Authorize(TrainingPermissions.AwardTypes.Default)]
    public abstract class AwardTypesAppServiceBase : TrainingAppService
    {

        protected IAwardTypeRepository _awardTypeRepository;
        protected AwardTypeManager _awardTypeManager;

        public AwardTypesAppServiceBase(IAwardTypeRepository awardTypeRepository, AwardTypeManager awardTypeManager)
        {

            _awardTypeRepository = awardTypeRepository;
            _awardTypeManager = awardTypeManager;

        }

        public virtual async Task<PagedResultDto<AwardTypeDto>> GetListAsync(GetAwardTypesInput input)
        {
            var totalCount = await _awardTypeRepository.GetCountAsync(input.FilterText, input.Name, input.HasReferenceNumber, input.HasExpiryDate);
            var items = await _awardTypeRepository.GetListAsync(input.FilterText, input.Name, input.HasReferenceNumber, input.HasExpiryDate, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AwardTypeDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AwardType>, List<AwardTypeDto>>(items)
            };
        }

        public virtual async Task<AwardTypeDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AwardType, AwardTypeDto>(await _awardTypeRepository.GetAsync(id));
        }

        [Authorize(TrainingPermissions.AwardTypes.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _awardTypeRepository.DeleteAsync(id);
        }

        [Authorize(TrainingPermissions.AwardTypes.Create)]
        public virtual async Task<AwardTypeDto> CreateAsync(AwardTypeCreateDto input)
        {

            var awardType = await _awardTypeManager.CreateAsync(
            input.Name, input.HasReferenceNumber, input.HasExpiryDate
            );

            return ObjectMapper.Map<AwardType, AwardTypeDto>(awardType);
        }

        [Authorize(TrainingPermissions.AwardTypes.Edit)]
        public virtual async Task<AwardTypeDto> UpdateAsync(Guid id, AwardTypeUpdateDto input)
        {

            var awardType = await _awardTypeManager.UpdateAsync(
            id,
            input.Name, input.HasReferenceNumber, input.HasExpiryDate, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<AwardType, AwardTypeDto>(awardType);
        }
    }
}