using CommunityToolkit.Mvvm.Messaging.Messages;
using Volo.Saas.Host.Dtos;

namespace WTH.Platform.Maui.Messages;

public class TenantEditMessage : ValueChangedMessage<SaasTenantUpdateDto>
{
    public TenantEditMessage(SaasTenantUpdateDto value) : base(value)
    {
    }
}
