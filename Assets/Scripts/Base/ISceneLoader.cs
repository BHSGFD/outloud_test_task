using Cysharp.Threading.Tasks;

public interface ISceneLoader : IService
{
    void LoadScene(string sceneName);
    void LoadScene(int sceneIndex);
    UniTask LoadSceneAsync(string sceneName);
    UniTask LoadSceneAsync(int sceneIndex);
}