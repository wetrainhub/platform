using Wth.Crm.Companies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Domain.Services;

namespace Wth.Crm.CompanyEmails
{
    public abstract class CompanyEmailManagerBase : DomainService
    {
        protected ICompanyEmailRepository _companyEmailRepository;

        public CompanyEmailManagerBase(ICompanyEmailRepository companyEmailRepository)
        {
            _companyEmailRepository = companyEmailRepository;
        }

        public virtual async Task<CompanyEmail> CreateAsync(
        Guid companyId, string value, CompanyEmailType type)
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var companyEmail = new CompanyEmail(
             GuidGenerator.Create(),
             companyId, value, type
             );

            return await _companyEmailRepository.InsertAsync(companyEmail);
        }

        public virtual async Task<CompanyEmail> UpdateAsync(
            Guid id,
            Guid companyId, string value, CompanyEmailType type
        )
        {
            Check.NotNullOrWhiteSpace(value, nameof(value));
            Check.NotNull(type, nameof(type));

            var companyEmail = await _companyEmailRepository.GetAsync(id);

            companyEmail.CompanyId = companyId;
            companyEmail.Value = value;
            companyEmail.Type = type;

            return await _companyEmailRepository.UpdateAsync(companyEmail);
        }

    }
}