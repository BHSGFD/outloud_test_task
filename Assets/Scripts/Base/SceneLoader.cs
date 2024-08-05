using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;

public class SceneLoader : ISceneLoader
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public async UniTask LoadSceneAsync(string sceneName)
    {
        await SceneManager.LoadSceneAsync(sceneName).ToUniTask();
    }

    public async UniTask LoadSceneAsync(int sceneIndex)
    {
        await SceneManager.LoadSceneAsync(sceneIndex).ToUniTask();
    }

    public bool IsInitialized => true;
}