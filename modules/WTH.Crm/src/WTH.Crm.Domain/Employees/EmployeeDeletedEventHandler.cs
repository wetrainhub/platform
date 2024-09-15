using Wth.Crm.EmployeeEmails;
using Wth.Crm.EmployeeTelephones;
using Wth.Crm.EmployeeAddresses;

using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Domain.Entities.Events;
using Volo.Abp.EventBus;

namespace Wth.Crm.Employees;

public class EmployeeDeletedEventHandler : ILocalEventHandler<EntityDeletedEventData<Employee>>, ITransientDependency
{
    private readonly IEmployeeEmailRepository _employeeEmailRepository;
    private readonly IEmployeeTelephoneRepository _employeeTelephoneRepository;
    private readonly IEmployeeAddressRepository _employeeAddressRepository;

    public EmployeeDeletedEventHandler(IEmployeeEmailRepository employeeEmailRepository, IEmployeeTelephoneRepository employeeTelephoneRepository, IEmployeeAddressRepository employeeAddressRepository)
    {
        _employeeEmailRepository = employeeEmailRepository;
        _employeeTelephoneRepository = employeeTelephoneRepository;
        _employeeAddressRepository = employeeAddressRepository;

    }

    public async Task HandleEventAsync(EntityDeletedEventData<Employee> eventData)
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
            await _employeeEmailRepository.DeleteManyAsync(await _employeeEmailRepository.GetListByEmployeeIdAsync(eventData.Entity.Id));
            await _employeeTelephoneRepository.DeleteManyAsync(await _employeeTelephoneRepository.GetListByEmployeeIdAsync(eventData.Entity.Id));
            await _employeeAddressRepository.DeleteManyAsync(await _employeeAddressRepository.GetListByEmployeeIdAsync(eventData.Entity.Id));

        }
        catch
        {
            //...
        }
    }
}