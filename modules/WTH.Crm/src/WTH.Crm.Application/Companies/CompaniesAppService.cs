using Wth.Crm.Shared;
using Wth.Crm.Notes;
using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Wth.Crm.Permissions;
using Wth.Crm.Companies;

namespace Wth.Crm.Companies
{

    [Authorize(CrmPermissions.Companies.Default)]
    public abstract class CompaniesAppServiceBase : CrmAppService
    {

        protected ICompanyRepository _companyRepository;
        protected CompanyManager _companyManager;

        protected IRepository<Wth.Crm.Notes.Note, Guid> _noteRepository;

        public CompaniesAppServiceBase(ICompanyRepository companyRepository, CompanyManager companyManager, IRepository<Wth.Crm.Notes.Note, Guid> noteRepository)
        {

            _companyRepository = companyRepository;
            _companyManager = companyManager; _noteRepository = noteRepository;

        }

        public virtual async Task<PagedResultDto<CompanyWithNavigationPropertiesDto>> GetListAsync(GetCompaniesInput input)
        {
            var totalCount = await _companyRepository.GetCountAsync(input.FilterText, input.Name, input.TaxReference, input.NoteId);
            var items = await _companyRepository.GetListWithNavigationPropertiesAsync(input.FilterText, input.Name, input.TaxReference, input.NoteId, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyWithNavigationPropertiesDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyWithNavigationProperties>, List<CompanyWithNavigationPropertiesDto>>(items)
            };
        }

        public virtual async Task<CompanyWithNavigationPropertiesDto> GetWithNavigationPropertiesAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyWithNavigationProperties, CompanyWithNavigationPropertiesDto>
                (await _companyRepository.GetWithNavigationPropertiesAsync(id));
        }

        public virtual async Task<CompanyDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<Company, CompanyDto>(await _companyRepository.GetAsync(id));
        }

        public virtual async Task<PagedResultDto<LookupDto<Guid>>> GetNoteLookupAsync(LookupRequestDto input)
        {
            var query = (await _noteRepository.GetQueryableAsync())
                .WhereIf(!string.IsNullOrWhiteSpace(input.Filter),
                    x => x.Content != null &&
                         x.Content.Contains(input.Filter));

            var lookupData = await query.PageBy(input.SkipCount, input.MaxResultCount).ToDynamicListAsync<Wth.Crm.Notes.Note>();
            var totalCount = query.Count();
            return new PagedResultDto<LookupDto<Guid>>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<Wth.Crm.Notes.Note>, List<LookupDto<Guid>>>(lookupData)
            };
        }

        [Authorize(CrmPermissions.Companies.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.Companies.Create)]
        public virtual async Task<CompanyDto> CreateAsync(CompanyCreateDto input)
        {

            var company = await _companyManager.CreateAsync(
            input.NoteIds, input.Name, input.TaxReference
            );

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }

        [Authorize(CrmPermissions.Companies.Edit)]
        public virtual async Task<CompanyDto> UpdateAsync(Guid id, CompanyUpdateDto input)
        {

            var company = await _companyManager.UpdateAsync(
            id,
            input.NoteIds, input.Name, input.TaxReference, input.ConcurrencyStamp
            );

            return ObjectMapper.Map<Company, CompanyDto>(company);
        }
    }
}