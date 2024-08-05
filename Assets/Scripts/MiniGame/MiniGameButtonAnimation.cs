using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MiniGameButtonAnimation : MonoBehaviour
{
    protected Button _button;
    protected RectTransform _buttonRect;
    protected Image _image;


    private void Awake()
    {
        _button = GetComponent<Button>();
        _buttonRect = GetComponent<RectTransform>();
        _image = GetComponent<Image>();
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Click);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Click);
    }

    public virtual void Click()
    {
        var sequence = DOTween.Sequence();

        sequence.Append(_buttonRect.DOAnchorPosY(-20f, 0.2f));
        sequence.Join(_image.DOColor(new Color(0, 0, 0, 0.6f), 0.3f));
    }
}
