using Asp.Versioning;
using System;
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
    public class AwardingOrganisationController : AwardingOrganisationControllerBase, IAwardingOrganisationsAppService
    {
        public AwardingOrganisationController(IAwardingOrganisationsAppService awardingOrganisationsAppService) : base(awardingOrganisationsAppService)
        {
        }
    }
}