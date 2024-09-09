using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Metadata;

namespace Apps.Wordbee;

public class Application : IApplication, ICategoryProvider
{
    public IEnumerable<ApplicationCategory> Categories
    {
        get => [ApplicationCategory.CatAndTms];
        set { }
    }

    public string Name
    {
        get => "Wordbee";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}