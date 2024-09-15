using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace Wth.Crm.Samples;

public interface ISampleAppService : IApplicationService
{
    Task<SampleDto> GetAsync();

    Task<SampleDto> GetAuthorizedAsync();
}
