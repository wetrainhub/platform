using WTH.Training.Shared;
using WTH.Training.AwardingOrganisations;
using WTH.Training.AwardTypes;
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
using WTH.Training.Awards;

namespace WTH.Training.Awards
{

    [Authorize(TrainingPermissions.Awards.Default)]
    public abstract class AwardsAppServiceBase : TrainingAppService
    {

        protected IAwardRepository _awardRepository;
        protected AwardManager _awardManager;

        protected IRepository<WTH.Training.AwardTypes.AwardType, Guid> _awardTypeRepository;
        protected IRepository<WTH.Training.AwardingOrganisations.AwardingOrganisation, Guid> _awardingOrganisationRepository;

        public AwardsAppServiceBase(IAwardRepository awardRepository, AwardManager awardManager, IRepository<WTH.Training.AwardTypes.AwardType, Guid> awardTypeRepository, IRepository<WTH.Training.AwardingOrganisations.AwardingOrganisation, Guid> awardingOrganisationRepository)
        {

            _awardRepository = awardRepository;
            _awardManager = awardManager; _awardTypeRepository = awardTypeRepository;
            _awardingOrganisationRepository = awardingOrganisationRepository;

        }

        public virtual async Task<PagedResultDto<AwardWithNavigationPropertiesDto>> GetListAsync(GetAwardsInput input)
        {
            var totalCount = await _awardRepository.GetCountAsync(input.FilterText, input.Name, input.Code, input.AwardTypeId, input.AwardingOrganisationId);
            var items = await _awardRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.Code, input.AwardTypeId, input.AwardingOrganisationId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AwardWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AwardWithNavigationProperties>, List<AwardWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<AwardWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<AwardWithNavigationProperties, AwardWithNavigationPropertiesDto>
                (await _awardRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<AwardDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Award, AwardDto>(await _awardRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetAwardTypeLookupAsync(LookupRequestDto input)
        {
            var query = (await _awardTypeRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<WTH.Training.AwardTypes.AwardType>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WTH.Training.AwardTypes.AwardType>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetAwardingOrganisationLookupAsync(LookupRequestDto input)
        {
            var query = (await _awardingOrganisationRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Name != null &&
                         x.Name.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<WTH.Training.AwardingOrganisations.AwardingOrganisation>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<WTH.Training.AwardingOrganisations.AwardingOrganisation>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(TrainingPermissions.Awards.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _awardRepository.DeleteAsync(id);
        }

        [Authorize(TrainingPermissions.Awards.Create)]
        public virtual async Task<AwardDto> CreateAsync(AwardCreateDto input)
        {
            if (input.AwardTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["AwardType"]]);
            }
            if (input.AwardingOrganisationId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["AwardingOrganisation"]]);
            }

            var award = await _awardManager.CreateAsync(
            input.AwardTypeId, input.AwardingOrganisationId, input.Name, input.Code, input.Description
            );

            return ObjectMapper.Map<Award, AwardDto>(award);
        }

        [Authorize(TrainingPermissions.Awards.Edit)]
        public virtual async Task<AwardDto> UpdateAsync(Guid id, AwardUpdateDto input)
        {
            if (input.AwardTypeId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["AwardType"]]);
            }
            if (input.AwardingOrganisationId == default)
            {
                throw new UserFriendlyException(L["The {0} field is required.", L["AwardingOrganisation"]]);
            }

            var award = await _awardManager.UpdateAsync(
            id,
            input.AwardTypeId, input.AwardingOrganisationId, input.Name, input.Code, input.Description, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Award, AwardDto>(award);
        }
    }
}