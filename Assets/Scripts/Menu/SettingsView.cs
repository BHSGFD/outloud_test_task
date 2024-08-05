using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SettingsView : MonoBehaviour
{
    [SerializeField] private Button _moveBackButton;

    [SerializeField] private SettingSwitch _switch;
    [SerializeField] private Button _convertTypeButton;
    [SerializeField] private TMP_Text _convertTypeText;
    [SerializeField] private Button _saveTypeButton;
    [SerializeField] private TMP_Text _saveTypeText;
    
    private IMenuController _menuController;

    private IGameSaveService _saveService;

    private void OnEnable()
    {
        var save = _saveService.Load();
        _switch.UpateStatus(save.GameOptions.IsOn);
        _switch.OnSwitch += OnSwitch;

        _convertTypeButton.onClick.AddListener(OnConvertButtonPressed);
        _saveTypeButton.onClick.AddListener(OnSaveButtonPressed);
        _moveBackButton.onClick.AddListener(MoveBack);

        UpdateButtons();
    }

    private void OnDisable()
    {
        _switch.OnSwitch -= OnSwitch;
        _convertTypeButton.onClick.RemoveListener(OnConvertButtonPressed);
        _saveTypeButton.onClick.RemoveListener(OnSaveButtonPressed);
    }

    [Inject]
    private void Construct(IGameSaveService saveService, IMenuController menuController)
    {
        _saveService = saveService;
        _menuController = menuController;
    }

    private void MoveBack()
    {
        _menuController.OpenMenu();
    }


    private void UpdateButtons()
    {
        _convertTypeText.text = _saveService.ConvertType == ConvertType.Json ? "JSON" : "BASE64";
        _saveTypeText.text = _saveService.SaveType == SaveType.PlayerPrefs ? "PLAYER PREFS" : "FILE";
    }

    private void OnSaveButtonPressed()
    {
        _saveService.ChangeSaveType(
            _saveService.SaveType == SaveType.PlayerPrefs ? SaveType.File : SaveType.PlayerPrefs,
            _saveService.ConvertType);
        _saveTypeText.text = _saveService.SaveType == SaveType.PlayerPrefs ? "PLAYER PREFS" : "FILE";
    }

    private void OnConvertButtonPressed()
    {
        _saveService.ChangeSaveType(_saveService.SaveType,
            _saveService.ConvertType == ConvertType.Json ? ConvertType.Base64 : ConvertType.Json);
        _convertTypeText.text = _saveService.ConvertType == ConvertType.Json ? "JSON" : "BASE64";
    }

    private void OnSwitch(bool status)
    {
        var save = _saveService.Load();
        save.GameOptions.IsOn = status;
        _saveService.Save(save);
    }
}