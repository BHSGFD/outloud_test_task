using System;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class EndScreenService : MonoBehaviour
{
    [SerializeField] private CanvasGroup _endWin;
    [SerializeField] private CanvasGroup _endLose;

    private IGame _game;
    private SignalBus _signalBus;
    
    [Inject]
    public void Construct(IGame game, SignalBus signalBus)
    {
        _game = game;
        _signalBus = signalBus;
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<MiniGameEndSignal>(Callback);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<MiniGameEndSignal>(Callback);
    }

    private void Callback(MiniGameEndSignal signal)
    {
        if (signal.Type == MiniGameEndType.Lose)
        {
            EndLose();
        }

        if (signal.Type == MiniGameEndType.Win)
        {
            EndWin();
        }
    }

    public void EndWin()
    {
        _endWin.alpha = 0;
        _endWin.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(2f);
        sequence.Append(_endWin.DOFade(1, 0.3f));
        sequence.AppendInterval(2f).AppendCallback(()=> _game.OpenMenu());
    }

    public void EndLose()
    {
        _endLose.alpha = 0;
        _endLose.gameObject.SetActive(true);

        Sequence sequence = DOTween.Sequence();
        sequence.Append(_endLose.DOFade(1, 0.3f));
        sequence.AppendInterval(2f).AppendCallback(()=> _game.OpenMenu());
    }
}