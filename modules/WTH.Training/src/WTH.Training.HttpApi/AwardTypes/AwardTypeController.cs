using Asp.Versioning;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using WTH.Training.AwardTypes;

namespace WTH.Training.AwardTypes
{
    [RemoteService(Name = "Training")]
    [Area("training")]
    [ControllerName("AwardType")]
    [Route("api/training/award-types")]
    public abstract class AwardTypeControllerBase : AbpController
    {
        protected IAwardTypesAppService _awardTypesAppService;

        public AwardTypeControllerBase(IAwardTypesAppService awardTypesAppService)
        {
            _awardTypesAppService = awardTypesAppService;
        }

        [HttpGet]
        public virtual Task<PagedResultDto<AwardTypeDto>> GetListAsync(GetAwardTypesInput input)
        {
            return _awardTypesAppService.GetListAsync(input);
        }

        [HttpGet]
        [Route("{id}")]
        public virtual Task<AwardTypeDto> GetAsync(Guid id)
        {
            return _awardTypesAppService.GetAsync(id);
        }

        [HttpPost]
        public virtual Task<AwardTypeDto> CreateAsync(AwardTypeCreateDto input)
        {
            return _awardTypesAppService.CreateAsync(input);
        }

        [HttpPut]
        [Route("{id}")]
        public virtual Task<AwardTypeDto> UpdateAsync(Guid id, AwardTypeUpdateDto input)
        {
            return _awardTypesAppService.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{id}")]
        public virtual Task DeleteAsync(Guid id)
        {
            return _awardTypesAppService.DeleteAsync(id);
        }
    }
}