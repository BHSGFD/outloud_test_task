using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class CardGameView : MonoBehaviour, ICardGameView
{
    private CardAnimateService _animateService;
    private DiContainer _container;
    private IFieldDrawer _fieldDrawer;
    private IFieldProvider _fieldProvider;
    private SignalBus _signalBus;

    private void Start()
    {
        _animateService = _container.Instantiate<CardAnimateService>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _signalBus.Fire(new ShuffleCardSignal());
    }

    private void OnEnable()
    {
        _signalBus.Subscribe<CardMatchSignal>(OnCardMatched);
        _signalBus.Subscribe<CardNotMatchedSignal>(CardNotMatched);
    }

    private void OnDisable()
    {
        _signalBus.Unsubscribe<CardMatchSignal>(OnCardMatched);
        _signalBus.Unsubscribe<CardNotMatchedSignal>(CardNotMatched);
    }

    public void Prepare(CardMiniGameData data)
    {
        _fieldProvider.InitField(data);
    }

    public void InitField(CardMiniGameData data)
    {
        StartField();
    }

    public void ShuffleCard(CardMiniGameData data)
    {
        _fieldProvider.UpdateField(data);
        AnimateShuffleCard();
    }

    public void Lock()
    {
        _fieldDrawer.Lock();
    }

    public void Unlock()
    {
        _fieldDrawer.Unlock();
    }

    [Inject]
    private void Container(DiContainer container, IFieldDrawer fieldDrawer, SignalBus signalBus,
        IFieldProvider fieldProvider)
    {
        _fieldDrawer = fieldDrawer;
        _fieldProvider = fieldProvider;
        _signalBus = signalBus;
        _container = container;
    }

    private void CardNotMatched()
    {
        Lock();
        UniTask.Delay((int)(Constants.Animations.OPEN_TIME * 1000)).ContinueWith(() => CloseAllCards()).Forget();
        UniTask.Delay((int)(Constants.Animations.OPEN_TIME * 1000 + 500)).ContinueWith(() => Unlock()).Forget();
    }

    private void OnCardMatched(CardMatchSignal signal)
    {
        var cardView1 = _fieldDrawer.GetCard(signal.Card1);
        var cardView2 = _fieldDrawer.GetCard(signal.Card2);

        _fieldProvider.ClearCell(new List<CellData> { cardView1.Cell, cardView2.Cell });
        UniTask.Delay((int)(Constants.Animations.OPEN_TIME * 1000))
            .ContinueWith(() => _animateService.AnimateCardHiding(cardView1, cardView2)).Forget();
    }

    private void AnimateShuffleCard()
    {
        var sequence = DOTween.Sequence();
        sequence.Append(TookAwayCards());
        sequence.AppendCallback(() => _fieldDrawer.Reshuffle(ShowCards));
    }

    private Sequence TookAwayCards()
    {
        var sequence = DOTween.Sequence();
        CloseAllCards();
        sequence.AppendInterval(0.5f);
        sequence.Append(_animateService.MoveCardsToCenter(_fieldDrawer.GetCardsInGame()));
        sequence.AppendInterval(0.5f);
        return sequence;
    }


    public void OpenAllCards()
    {
        _fieldDrawer.OpenAllCards();
    }

    public void CloseAllCards()
    {
        _fieldDrawer.CloseAllCards();
    }

    private void StartField()
    {
        _fieldDrawer.DrawField(() => ShowCards());
    }

    private void ShowCards()
    {
        var sequence = DOTween.Sequence();
        Lock();
        sequence.AppendInterval(0.5f).AppendCallback(() => OpenAllCards());
        sequence.AppendInterval(1.5f).AppendCallback(() =>
        {
            CloseAllCards();
            Unlock();
        });
    }
}