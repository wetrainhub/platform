using Asp.Versioning;
using System;
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
    public class AwardTypeController : AwardTypeControllerBase, IAwardTypesAppService
    {
        public AwardTypeController(IAwardTypesAppService awardTypesAppService) : base(awardTypesAppService)
        {
        }
    }
}