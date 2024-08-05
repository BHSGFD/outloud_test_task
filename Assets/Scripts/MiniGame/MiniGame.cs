using UnityEngine;
using Zenject;

public class MiniGame : MonoBehaviour
{
    private IMiniGame _miniGame;
    
    [Inject]
    private void Construct(IMiniGame miniGame)
    {
        _miniGame = miniGame;
    }

    private void Start()
    {
        _miniGame.StartGame();
    }
}