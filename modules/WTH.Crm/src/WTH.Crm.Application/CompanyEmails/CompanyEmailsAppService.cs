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
using Wth.Crm.CompanyEmails;

namespace Wth.Crm.CompanyEmails
{

    [Authorize(CrmPermissions.CompanyEmails.Default)]
    public abstract class CompanyEmailsAppServiceBase : CrmAppService
    {

        protected ICompanyEmailRepository _companyEmailRepository;
        protected CompanyEmailManager _companyEmailManager;

        public CompanyEmailsAppServiceBase(ICompanyEmailRepository companyEmailRepository, CompanyEmailManager companyEmailManager)
        {

            _companyEmailRepository = companyEmailRepository;
            _companyEmailManager = companyEmailManager;

        }

        public virtual async Task<PagedResultDto<CompanyEmailDto>> GetListByCompanyIdAsync(GetCompanyEmailListInput input)
        {
            var companyEmails = await _companyEmailRepository.GetListByCompanyIdAsync(
                input.CompanyId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<CompanyEmailDto>
            {
                TotalCount = await _companyEmailRepository.GetCountByCompanyIdAsync(input.CompanyId),
                Items = ObjectMapper.Map<List<CompanyEmail>, List<CompanyEmailDto>>(companyEmails)
            };
        }

        public virtual async Task<PagedResultDto<CompanyEmailDto>> GetListAsync(GetCompanyEmailsInput input)
        {
            var totalCount = await _companyEmailRepository.GetCountAsync(input.FilterText, input.Value, input.Type);
            var items = await _companyEmailRepository.GetListAsync(input.FilterText, input.Value, input.Type, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyEmailDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyEmail>, List<CompanyEmailDto>>(items)
            };
        }

        public virtual async Task<CompanyEmailDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyEmail, CompanyEmailDto>(await _companyEmailRepository.GetAsync(id));
        }

        [Authorize(CrmPermissions.CompanyEmails.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyEmailRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.CompanyEmails.Create)]
        public virtual async Task<CompanyEmailDto> CreateAsync(CompanyEmailCreateDto input)
        {

            var companyEmail = await _companyEmailManager.CreateAsync(input.CompanyId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<CompanyEmail, CompanyEmailDto>(companyEmail);
        }

        [Authorize(CrmPermissions.CompanyEmails.Edit)]
        public virtual async Task<CompanyEmailDto> UpdateAsync(Guid id, CompanyEmailUpdateDto input)
        {

            var companyEmail = await _companyEmailManager.UpdateAsync(
            id, input.CompanyId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<CompanyEmail, CompanyEmailDto>(companyEmail);
        }
    }
}