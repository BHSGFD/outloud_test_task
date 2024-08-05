using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingSwitch : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image _toggle;

    private Sequence _sequence;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (IsOn)
            Off();
        else
            On();
    }

    public void UpateStatus(bool isOn)
    {
        IsOn = isOn;

        var toggleRectTransform = _toggle.transform as RectTransform;
        var anchoredPosition = toggleRectTransform.anchoredPosition;
        anchoredPosition.x = IsOn ? 140f : 62f;
        toggleRectTransform.anchoredPosition = anchoredPosition;
    }

    public void On()
    {
        IsOn = true;
        var toggleRectTransform = _toggle.transform as RectTransform;

        _sequence?.Kill();

        _sequence = DOTween.Sequence();
        _sequence.Append(toggleRectTransform.DOAnchorPosX(140f, 0.5f));

        OnSwitch?.Invoke(IsOn);
    }

    private void Off()
    {
        IsOn = false;
        var toggleRectTransform = _toggle.transform as RectTransform;

        _sequence?.Kill();

        _sequence = DOTween.Sequence();
        _sequence.Append(toggleRectTransform.DOAnchorPosX(62f, 0.5f));

        OnSwitch?.Invoke(IsOn);
    }
    
    public Action<bool> OnSwitch { get; set; }
    public bool IsOn { get; private set; }
}