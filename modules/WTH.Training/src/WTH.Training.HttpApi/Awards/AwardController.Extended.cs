using Asp.Versioning;
using System;
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
    public class AwardController : AwardControllerBase, IAwardsAppService
    {
        public AwardController(IAwardsAppService awardsAppService) : base(awardsAppService)
        {
        }
    }
}