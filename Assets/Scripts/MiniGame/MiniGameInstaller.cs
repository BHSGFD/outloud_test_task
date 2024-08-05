using UnityEngine;
using Zenject;

public class ShuffleCardSignal
{
}

public class MiniGameInstaller : MonoInstaller
{
    [SerializeField] private CardGameView _cardView;
    [SerializeField] private FieldDrawer _fieldDrawer;

    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);
        Container.Bind<ICardMiniGameDataProvider>().To<CardMiniGameDataProvider>().AsSingle();
        Container.Bind<IMiniGame>().To<CardMiniGame>().AsSingle();
        Container.Bind<ICardAnimateService>().To<CardAnimateService>().AsSingle();
        Container.Bind<IFieldProvider>().To<FieldProvider>().AsSingle();
        var timerRunner = Container.Instantiate<TimerRunner>();
        Container.Bind<ITimerRunner>().FromInstance(timerRunner).AsTransient();
        Container.Bind<ITickable>().FromInstance(timerRunner).AsTransient();

        Container.Bind<ICardGameView>().FromInstance(_cardView).AsSingle();
        Container.Bind<IFieldDrawer>().FromInstance(_fieldDrawer).AsSingle();

        Container.Bind<ICardMatcher>().To<CardMatcher>().AsSingle().NonLazy();

        Container.DeclareSignal<CardOpenSignal>();
        Container.DeclareSignal<CardNotMatchedSignal>();
        Container.DeclareSignal<CardMatchSignal>();
        Container.DeclareSignal<ShuffleCardSignal>();
        Container.DeclareSignal<MiniGameEndSignal>();
    }
}