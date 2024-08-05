using Zenject;

public class Game : IGame
{
    private readonly DiContainer _container;

    private GameStateMachine _stateMachine;


    public Game(DiContainer container)
    {
        _container = container;
    }

    public void Init()
    {
        _stateMachine = _container.Instantiate<GameStateMachine>();
        _stateMachine.Init();
    }

    public void OpenMenu()
    {
        _stateMachine.RequestStateChange(Constants.StateNames.GameStates.MENU_STATE);
    }

    public void OpenCardMiniGame()
    {
        _stateMachine.RequestStateChange(Constants.StateNames.GameStates.CARD_MINI_GAME_STATE);
    }
}