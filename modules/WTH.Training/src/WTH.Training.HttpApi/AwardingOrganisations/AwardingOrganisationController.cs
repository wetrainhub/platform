using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using WTH.Training.AwardingOrganisations;

namespace WTH.Training.AwardingOrganisations
{
    [RemoteService(Name = "Training")]
    [Area("training")]
    [ControllerName("AwardingOrganisation")]
    [Route("api/training/awarding-organisations")]
    public abstract class AwardingOrganisationControllerBase : AbpController
    {
        protected IAwardingOrganisationsAppService _awardingOrganisationsAppService;

        public AwardingOrganisationControllerBase(IAwardingOrganisationsAppService awardingOrganisationsAppService)
        {
            _awardingOrganisationsAppService = awardingOrganisationsAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AwardingOrganisationDto>> GetListAsync(GetAwardingOrganisationsInput input)
        {
            return _awardingOrganisationsAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AwardingOrganisationDto> GetAsync(Guid id)
        {
            return _awardingOrganisationsAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<AwardingOrganisationDto> CreateAsync(AwardingOrganisationCreateDto input)
        {
            return _awardingOrganisationsAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AwardingOrganisationDto> UpdateAsync(Guid id, AwardingOrganisationUpdateDto input)
        {
            return _awardingOrganisationsAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _awardingOrganisationsAppService.DeleteAsync(id);
        }
    }
}