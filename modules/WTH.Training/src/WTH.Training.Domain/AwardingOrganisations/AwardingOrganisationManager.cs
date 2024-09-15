using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;
using Volo.Abp.Data;

namespace WTH.Training.AwardingOrganisations
{
    public abstract class AwardingOrganisationManagerBase : DomainService
    {
        protected IAwardingOrganisationRepository _awardingOrganisationRepository;

        public AwardingOrganisationManagerBase(IAwardingOrganisationRepository awardingOrganisationRepository)
        {
            _awardingOrganisationRepository = awardingOrganisationRepository;
        }

        public virtual async Task<AwardingOrganisation> CreateAsync(
        string name, string? email = null, string? telephone = null)
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var awardingOrganisation = new AwardingOrganisation(
             GuidGenerator.Create(),
             name, email, telephone
             );

            return await _awardingOrganisationRepository.InsertAsync(awardingOrganisation);
        }

        public virtual async Task<AwardingOrganisation> UpdateAsync(
            Guid id,
            string name, string? email = null, string? telephone = null, [CanBeNull] string? concurrencyStamp = null
        )
        {
            Check.NotNullOrWhiteSpace(name, nameof(name));

            var awardingOrganisation = await _awardingOrganisationRepository.GetAsync(id);

            awardingOrganisation.Name = name;
            awardingOrganisation.Email = email;
            awardingOrganisation.Telephone = telephone;

            awardingOrganisation.SetConcurrencyStampIfNotNull(concurrencyStamp);
            return await _awardingOrganisationRepository.UpdateAsync(awardingOrganisation);
        }

    }
}