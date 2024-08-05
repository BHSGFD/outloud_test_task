using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<IAddressableAssetProvider>().To<AddressableAssetProvider>().AsSingle();
        Container.Bind<IConfigProvider>().To<ConfigProvider>().AsSingle();
        Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        Container.Bind<IGame>().To<Game>().AsSingle();
        Container.Bind<IGameSaveService>().To<GameSaveService>().AsSingle();
        
    }
}