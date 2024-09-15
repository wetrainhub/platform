using Wth.Crm.CompanyAddresses;
using Wth.Crm.CompanyEmails;
using Wth.Crm.CompanyTelephones;

using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Wth.Crm.Companies;

public class CompanyDeletedEventHandler : ILocalEventHandler<EntityDeletedEventData<Company>>, ITransientDependency
{
    private readonly ICompanyAddressRepository _companyAddressRepository;
    private readonly ICompanyEmailRepository _companyEmailRepository;
    private readonly ICompanyTelephoneRepository _companyTelephoneRepository;

    public CompanyDeletedEventHandler(ICompanyAddressRepository companyAddressRepository, ICompanyEmailRepository companyEmailRepository, ICompanyTelephoneRepository companyTelephoneRepository)
    {
        _companyAddressRepository = companyAddressRepository;
        _companyEmailRepository = companyEmailRepository;
        _companyTelephoneRepository = companyTelephoneRepository;

    }

    public async Task HandleEventAsync(EntityDeletedEventData<Company> eventData)
    {
        if (eventData.Entity is not ISoftDelete softDeletedEntity)
        {
            return;
        }

        if (!softDeletedEntity.IsDeleted)
        {
            return;
        }

        try
        {
            await _companyAddressRepository.DeleteManyAsync(await _companyAddressRepository.GetListByCompanyIdAsync(eventData.Entity.Id));
            await _companyEmailRepository.DeleteManyAsync(await _companyEmailRepository.GetListByCompanyIdAsync(eventData.Entity.Id));
            await _companyTelephoneRepository.DeleteManyAsync(await _companyTelephoneRepository.GetListByCompanyIdAsync(eventData.Entity.Id));

        }
        catch
        {
            //...
        }
    }
}