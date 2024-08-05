using UnityHFSM;

public class MenuState : State
{
    private readonly ISceneLoader _sceneLoader;

    public MenuState(ISceneLoader sceneLoader)
    {
        _sceneLoader = sceneLoader;
    }

    public override void OnEnter()
    {
        LoadScene();
        base.OnEnter();
    }

    private void LoadScene()
    {
        _sceneLoader.LoadScene(Constants.SceneName.MENU);
    }
}