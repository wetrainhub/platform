using CommunityToolkit.Mvvm.Messaging.Messages;

namespace WTH.Platform.Maui.Messages;

public class ProfilePictureChangedMessage : ValueChangedMessage<string>
{
    public ProfilePictureChangedMessage(string value) : base(value)
    {
    }
}
