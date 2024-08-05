using UnityHFSM;
using Zenject;

public class GameStateMachine : StateMachine
{
    private readonly DiContainer _container;

    public GameStateMachine(DiContainer container)
    {
        _container = container;
    }

    public override void Init()
    {
        AddState(Constants.StateNames.GameStates.BOOTSTRAP_STATE, _container.Instantiate<BootstrapState>());
        AddState(Constants.StateNames.GameStates.MENU_STATE, _container.Instantiate<MenuState>());
        AddState(Constants.StateNames.GameStates.CARD_MINI_GAME_STATE, _container.Instantiate<CardMiniGameState>());
        SetStartState(Constants.StateNames.GameStates.BOOTSTRAP_STATE);

        base.Init();
    }
}