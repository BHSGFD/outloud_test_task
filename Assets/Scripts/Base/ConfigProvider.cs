using System.Collections.Generic;

public class ConfigProvider : IConfigProvider
{
    private readonly Dictionary<string, IConfig> _configs = new();

    public ConfigProvider(IAddressableAssetProvider addressableAssetProvider)
    {
        LoadConfigs(addressableAssetProvider);
    }

    public T ResolveConfig<T>(string name) where T : class, IConfig
    {
        return _configs.TryGetValue(name, out var config) ? config as T : null;
    }

    public T ResolveConfig<T>() where T : class, IConfig
    {
        return ResolveConfig<T>(typeof(T).Name);
    }

    public bool IsInitialized { get; private set; }

    private async void LoadConfigs(IAddressableAssetProvider addressableAssetProvider)
    {
        var configs = await addressableAssetProvider.LoadAssetGroup<IConfig>("Config");

        foreach (var config in configs) _configs.Add(config.Name, config);

        IsInitialized = true;
    }
}