using Wth.Crm.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wth.Crm.CompanyTelephones
{
    public abstract class CompanyTelephoneManagerBase : DomainService
    {
        protected ICompanyTelephoneRepository _companyTelephoneRepository;

        public CompanyTelephoneManagerBase(ICompanyTelephoneRepository companyTelephoneRepository)
        {
            _companyTelephoneRepository = companyTelephoneRepository;
        }

        public virtual async Task<CompanyTelephone> CreateAsync(
        Guid companyId, string value, CompanyTelephoneType type)
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var companyTelephone = new CompanyTelephone(
             GuidGenerator.Create(),
             companyId, value, type
             );

            return await _companyTelephoneRepository.InsertAsync(companyTelephone);
        }

        public virtual async Task<CompanyTelephone> UpdateAsync(
            Guid id,
            Guid companyId, string value, CompanyTelephoneType type
        )
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var companyTelephone = await _companyTelephoneRepository.GetAsync(id);

            companyTelephone.CompanyId = companyId;
            companyTelephone.Value = value;
            companyTelephone.Type = type;

            return await _companyTelephoneRepository.UpdateAsync(companyTelephone);
        }

    }
}