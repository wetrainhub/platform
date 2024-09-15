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
using WTH.Training.AwardingOrganisations;

namespace WTH.Training.AwardingOrganisations
{

    [Authorize(TrainingPermissions.AwardingOrganisations.Default)]
    public abstract class AwardingOrganisationsAppServiceBase : TrainingAppService
    {

        protected IAwardingOrganisationRepository _awardingOrganisationRepository;
        protected AwardingOrganisationManager _awardingOrganisationManager;

        public AwardingOrganisationsAppServiceBase(IAwardingOrganisationRepository awardingOrganisationRepository, AwardingOrganisationManager awardingOrganisationManager)
        {

            _awardingOrganisationRepository = awardingOrganisationRepository;
            _awardingOrganisationManager = awardingOrganisationManager;

        }

        public virtual async Task<PagedResultDto<AwardingOrganisationDto>> GetListAsync(GetAwardingOrganisationsInput input)
        {
            var totalCount = await _awardingOrganisationRepository.GetCountAsync(input.FilterText, input.Name, input.Email);
            var items = await _awardingOrganisationRepository.GetListAsync(input.FilterText, input.Name, input.Email, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<AwardingOrganisationDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<AwardingOrganisation>, List<AwardingOrganisationDto>>(items)
            };
        }

        public virtual async Task<AwardingOrganisationDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<AwardingOrganisation, AwardingOrganisationDto>(await _awardingOrganisationRepository.GetAsync(id));
        }

        [Authorize(TrainingPermissions.AwardingOrganisations.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _awardingOrganisationRepository.DeleteAsync(id);
        }

        [Authorize(TrainingPermissions.AwardingOrganisations.Create)]
        public virtual async Task<AwardingOrganisationDto> CreateAsync(AwardingOrganisationCreateDto input)
        {

            var awardingOrganisation = await _awardingOrganisationManager.CreateAsync(
            input.Name, input.Email, input.Telephone
            );

            return ObjectMapper.Map<AwardingOrganisation, AwardingOrganisationDto>(awardingOrganisation);
        }

        [Authorize(TrainingPermissions.AwardingOrganisations.Edit)]
        public virtual async Task<AwardingOrganisationDto> UpdateAsync(Guid id, AwardingOrganisationUpdateDto input)
        {

            var awardingOrganisation = await _awardingOrganisationManager.UpdateAsync(
            id,
            input.Name, input.Email, input.Telephone, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<AwardingOrganisation, AwardingOrganisationDto>(awardingOrganisation);
        }
    }
}