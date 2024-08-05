using DG.Tweening;
using UnityEngine;

public class MenuController : MonoBehaviour, IMenuController
{
    [SerializeField] private CanvasGroup _menu;
    [SerializeField] private CanvasGroup _settings;

    private Sequence _sequence;

    public void OpenMenu()
    {
        _settings.interactable = false;
        _menu.interactable = true;

        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.Append(_settings.DOFade(0, 0.3f));
        _sequence.AppendCallback(() =>
        {
            _menu.alpha = 0;
            _menu.gameObject.SetActive(true);
            _settings.gameObject.SetActive(false);
        });

        _sequence.Append(_menu.DOFade(1, 0.3f));
    }

    public void OpenSettings()
    {
        _settings.interactable = true;
        _menu.interactable = false;

        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.Append(_menu.DOFade(0, 0.3f));
        _sequence.AppendCallback(() =>
        {
            _settings.alpha = 0;
            _menu.gameObject.SetActive(false);
            _settings.gameObject.SetActive(true);
        });

        _sequence.Append(_settings.DOFade(1, 0.3f));
    }
}