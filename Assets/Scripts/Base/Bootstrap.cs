using UnityEngine;
using Zenject;

public class Bootstrap : MonoBehaviour
{
    private IGame _game;
    
    [Inject]
    private void Construct(IGame game)
    {
        _game = game;
    }

    private void Start()
    {
        _game.Init();
    }
}