using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class MenuView : MonoBehaviour
{
    [SerializeField] private Button _start;
    [SerializeField] private Button _settings;

    [SerializeField] private CanvasGroup _canvasGroup;
    private IGame _game;

    private IMenuController _menuController;

    private void OnEnable()
    {
        _start.onClick.AddListener(StartButtonClicked);
        _settings.onClick.AddListener(SettingButtonClicked);
    }

    private void OnDisable()
    {
        _start.onClick.RemoveListener(StartButtonClicked);
        _settings.onClick.RemoveListener(SettingButtonClicked);
    }

    [Inject]
    private void Construct(IGame game, IMenuController menuController)
    {
        _menuController = menuController;
        _game = game;
    }

    private void SettingButtonClicked()
    {
        _menuController.OpenSettings();
    }

    private void StartButtonClicked()
    {
        _game.OpenCardMiniGame();
        _canvasGroup.interactable = false;
    }
}