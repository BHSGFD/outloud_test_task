using Cysharp.Threading.Tasks;
using UnityHFSM;

public class CardMiniGameState : State
{
    private readonly ISceneLoader _sceneLoader;

    public CardMiniGameState(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public override void OnEnter()
    {
        EnterGame().Forget();
    }

    private async UniTask EnterGame()
    {
        await LoadScene();
        base.OnEnter();
    }

    private async UniTask LoadScene()
    {
        await _sceneLoader.LoadSceneAsync(Constants.SceneName.MINI_GAME);
    }
}