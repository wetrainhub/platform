using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace WTH.Training.Awards
{
    public abstract class AwardManagerBase : DomainService
    {
        protected IAwardRepository _awardRepository;

        public AwardManagerBase(IAwardRepository awardRepository)
        {
            _awardRepository = awardRepository;
        }

        public virtual async Task<Award> CreateAsync(
        Guid awardTypeId, Guid awardingOrganisationId, string name, string code, string? description = null)
        {
            Check.NotNull(awardTypeId, nameof(awardTypeId));
            Check.NotNull(awardingOrganisationId, nameof(awardingOrganisationId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var award = new Award(
             GuidGenerator.Create(),
             awardTypeId, awardingOrganisationId, name, code, description
             );

            return await _awardRepository.InsertAsync(award);
        }

        public virtual async Task<Award> UpdateAsync(
            Guid id,
            Guid awardTypeId, Guid awardingOrganisationId, string name, string code, string? description = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNull(awardTypeId, nameof(awardTypeId));
            Check.NotNull(awardingOrganisationId, nameof(awardingOrganisationId));
            Check.NotNullOrWhiteSpace(name, nameof(name));
            Check.NotNullOrWhiteSpace(code, nameof(code));

            var award = await _awardRepository.GetAsync(id);

            award.AwardTypeId = awardTypeId;
            award.AwardingOrganisationId = awardingOrganisationId;
            award.Name = name;
            award.Code = code;
            award.Description = description;

            award.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _awardRepository.UpdateAsync(award);
        }

    }
}