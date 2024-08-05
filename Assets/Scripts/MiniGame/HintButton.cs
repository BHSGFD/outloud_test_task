using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class HintButton : MiniGameButtonAnimation
{
    [SerializeField] private TextMeshProUGUI _text;
    
    private bool _locked;
    private Sequence _sequence;
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public override void Click()
    {
        if (_locked)
            return;

        _locked = true;
        _signalBus.Fire(new ShuffleCardSignal());

        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.Append(_buttonRect.DOAnchorPosY(-20f, 0.2f));
        _sequence.Join(_image.DOColor(new Color(0, 0, 0, 0.6f), 0.3f));
        _sequence.Join(_text.DOColor(new Color(1, 1, 1, 0.4f), 0.3f));
        _sequence.AppendInterval(10f).OnComplete(OnComplete);
    }

    private void OnComplete()
    {
        Return();
    }

    private void Return()
    {
        _sequence?.Kill();
        _sequence = DOTween.Sequence();

        _sequence.Append(_buttonRect.DOAnchorPosY(0, 1));
        _sequence.Join(_image.DOColor(new Color(1, 1, 1, 1), 0.4f));
        _sequence.Join(_text.DOColor(Color.black, 0.3f));
        _sequence.OnComplete(() => { _locked = false; });
    }
}