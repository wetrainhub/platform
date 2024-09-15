using System.Linq;
using Volo.Abp.Features;
using Volo.Chat;

namespace WTH.Platform.Features;

public class ChatServiceFeatureProvider : FeatureDefinitionProvider 
{
    public override void Define(IFeatureDefinitionContext context)
    {
        context
            .GetGroupOrNull(ChatFeatures.GroupName)!
            .Features
            .FirstOrDefault(f => f.Name == ChatFeatures.Enable)!
            .DefaultValue = "true";
    }
}