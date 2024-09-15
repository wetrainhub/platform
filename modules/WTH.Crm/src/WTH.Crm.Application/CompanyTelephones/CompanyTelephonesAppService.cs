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
using Wth.Crm.CompanyTelephones;

namespace Wth.Crm.CompanyTelephones
{

    [Authorize(CrmPermissions.CompanyTelephones.Default)]
    public abstract class CompanyTelephonesAppServiceBase : CrmAppService
    {

        protected ICompanyTelephoneRepository _companyTelephoneRepository;
        protected CompanyTelephoneManager _companyTelephoneManager;

        public CompanyTelephonesAppServiceBase(ICompanyTelephoneRepository companyTelephoneRepository, CompanyTelephoneManager companyTelephoneManager)
        {

            _companyTelephoneRepository = companyTelephoneRepository;
            _companyTelephoneManager = companyTelephoneManager;

        }

        public virtual async Task<PagedResultDto<CompanyTelephoneDto>> GetListByCompanyIdAsync(GetCompanyTelephoneListInput input)
        {
            var companyTelephones = await _companyTelephoneRepository.GetListByCompanyIdAsync(
                input.CompanyId,
                input.Sorting,
                input.MaxResultCount,
                input.SkipCount);

            return new PagedResultDto<CompanyTelephoneDto>
            {
                TotalCount = await _companyTelephoneRepository.GetCountByCompanyIdAsync(input.CompanyId),
                Items = ObjectMapper.Map<List<CompanyTelephone>, List<CompanyTelephoneDto>>(companyTelephones)
            };
        }

        public virtual async Task<PagedResultDto<CompanyTelephoneDto>> GetListAsync(GetCompanyTelephonesInput input)
        {
            var totalCount = await _companyTelephoneRepository.GetCountAsync(input.FilterText, input.Value, input.Type);
            var items = await _companyTelephoneRepository.GetListAsync(input.FilterText, input.Value, input.Type, input.Sorting, input.MaxResultCount, input.SkipCount);

            return new PagedResultDto<CompanyTelephoneDto>
            {
                TotalCount = totalCount,
                Items = ObjectMapper.Map<List<CompanyTelephone>, List<CompanyTelephoneDto>>(items)
            };
        }

        public virtual async Task<CompanyTelephoneDto> GetAsync(Guid id)
        {
            return ObjectMapper.Map<CompanyTelephone, CompanyTelephoneDto>(await _companyTelephoneRepository.GetAsync(id));
        }

        [Authorize(CrmPermissions.CompanyTelephones.Delete)]
        public virtual async Task DeleteAsync(Guid id)
        {
            await _companyTelephoneRepository.DeleteAsync(id);
        }

        [Authorize(CrmPermissions.CompanyTelephones.Create)]
        public virtual async Task<CompanyTelephoneDto> CreateAsync(CompanyTelephoneCreateDto input)
        {

            var companyTelephone = await _companyTelephoneManager.CreateAsync(input.CompanyId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<CompanyTelephone, CompanyTelephoneDto>(companyTelephone);
        }

        [Authorize(CrmPermissions.CompanyTelephones.Edit)]
        public virtual async Task<CompanyTelephoneDto> UpdateAsync(Guid id, CompanyTelephoneUpdateDto input)
        {

            var companyTelephone = await _companyTelephoneManager.UpdateAsync(
            id, input.CompanyId
            , input.Value, input.Type
            );

            return ObjectMapper.Map<CompanyTelephone, CompanyTelephoneDto>(companyTelephone);
        }
    }
}