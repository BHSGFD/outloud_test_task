public interface IConfigProvider : IService
{
    public bool IsInitialized { get; }
    public T ResolveConfig<T>(string name) where T : class, IConfig;
    public T ResolveConfig<T>() where T : class, IConfig;
}