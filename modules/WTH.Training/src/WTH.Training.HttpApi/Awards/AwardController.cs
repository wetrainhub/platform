using WTH.Training.Shared;
using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using WTH.Training.Awards;

namespace WTH.Training.Awards
{
    [RemoteService(Name = "Training")]
    [Area("training")]
    [ControllerName("Award")]
    [Route("api/training/awards")]
    public abstract class AwardControllerBase : AbpController
    {
        protected IAwardsAppService _awardsAppService;

        public AwardControllerBase(IAwardsAppService awardsAppService)
        {
            _awardsAppService = awardsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AwardWithNavigationPropertiesDto>> GetListAsync(GetAwardsInput input)
        {
            return _awardsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("with-navigation-properties/{id}")]
        public virtual Task<AwardWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return _awardsAppService.GetWithNavigationPropertiesAsync(id);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AwardDto> GetAsync(Guid id)
        {
            return _awardsAppService.GetAsync(id);
        }

        [HttpGet]
        [Route("award-type-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetAwardTypeLookupAsync(LookupRequestDto input)
        {
            return _awardsAppService.GetAwardTypeLookupAsync(input);
        }

        [HttpGet]
        [Route("awarding-organisation-lookup")]
        public virtual Task<PagedResultDto<LookupDto<Guid>>> GetAwardingOrganisationLookupAsync(LookupRequestDto input)
        {
            return _awardsAppService.GetAwardingOrganisationLookupAsync(input);
        }

        [HttpPost]
        public virtual Task<AwardDto> CreateAsync(AwardCreateDto input)
        {
            return _awardsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AwardDto> UpdateAsync(Guid id, AwardUpdateDto input)
        {
            return _awardsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _awardsAppService.DeleteAsync(id);
        }
    }
}