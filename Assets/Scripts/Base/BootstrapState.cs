using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityHFSM;

public class BootstrapState : State
{
    private readonly IConfigProvider _configProvider;
    private readonly IGame _game;

    public BootstrapState(IConfigProvider configProvider, IGame game)
    {
        _configProvider = configProvider;
        _game = game;
    }

    public override void OnEnter()
    {
        var task = UniTask.RunOnThreadPool(CheckInitializationAndOpenMenu);
        task.ContinueWith(() => _game.OpenMenu());
        base.OnEnter();
    }

    private async UniTask CheckInitializationAndOpenMenu()
    {
        var services = new List<IService>();
        services.Add(_configProvider);

        while (!services.All(service => service.IsInitialized)) await UniTask.WaitForSeconds(0.05f);
    }
}