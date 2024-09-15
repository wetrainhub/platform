using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WTH.Platform.Maui.Messages;

public class LanguageChangedMessage : ValueChangedMessage<string?>
{
    public LanguageChangedMessage(string? value) : base(value)
    {
    }
}
