using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;
using Image = UnityEngine.UI.Image;


public interface ICardAnimateService
{
    public Sequence AnimateCardHiding(CardView card1, CardView card2);
    public Sequence MoveCardsToCenter(List<CardView> cards);
}
public class CardAnimateService : ICardAnimateService
{
    private ICardGameView _cardGameView;
    private SignalBus _signalBus;
    
    
    public CardAnimateService(ICardGameView cardGameView, SignalBus signalBus)
    {
        _cardGameView = cardGameView;
        _signalBus = signalBus;
    }

    public Sequence AnimateCardHiding(CardView card1, CardView card2)
    {
        _cardGameView.Lock();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(AnimateCard(0.05f, card1.transform as RectTransform));
        sequence.Join(AnimateCard(0.08f, card2.transform as RectTransform));
        sequence.OnComplete(() => _cardGameView.Unlock());
        return sequence;
    }

    private Sequence AnimateCard(float time, RectTransform rectTransform)
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(time);
        sequence.Append(rectTransform.transform.DOLocalMove(new Vector2(0, -Screen.height * 0.6f), 0.5f));
        sequence.Append(rectTransform.transform.DOLocalMove(new Vector2(0, -Screen.height * 0.6f), 0.5f));
        return sequence;
    }

    public Sequence MoveCardsToCenter(List<CardView> cards)
    {
        Sequence sequence = DOTween.Sequence();
        foreach (var card in cards)
            sequence.AppendInterval(0.05f).AppendCallback(() => card.transform.DOLocalMove(Vector3.zero, 0.5f));
        
        sequence.AppendInterval(0.5f);
        return sequence;
    }
}

public class CardView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Image _image;
    [SerializeField] private TMP_Text _text;
    
    private SignalBus _signalBus;
    private Sequence _sequence;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }
    
    public void Init(CellData cell, float size)
    {
        Cell = cell;
        _image.color = Cell.Card.Color;
        _text.text = Cell.Card.Id;
        
        RectTransform rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = Vector2.one * size;
    }

    public void UpdateView(float size)
    {
        if (Cell.Card != null)
        {
            _image.color = Cell.Card.Color;
            _text.text = Cell.Card.Id;
        }

        RectTransform rectTransform = transform as RectTransform;
        rectTransform.sizeDelta = Vector2.one * size;
    }
    
    public void MoveWithRotation(Vector2 position, float angle)
    {
        RectTransform rectTransform = transform as RectTransform;

        _sequence = DOTween.Sequence();

        _sequence.Append(rectTransform.DOAnchorPos(position, 0.5f));
        _sequence.Join(rectTransform.DORotate(Vector3.forward * angle, 0.5f));
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(IsLocked) return;
        if(IsOpened) return;
        
        Open();
    }

    public void Open()
    {
        if(IsOpened) return;
        
        IsOpened = true;
        _animator.Play("Open", 0, 0);
        
        if(IsLocked) return;
        
        _signalBus.Fire(new CardOpenSignal()
        {
            Card = Cell.Card
        });
    }

    public void Close()
    {
        if(!IsOpened) return;
        
        IsOpened = false;
        _animator.Play("Close", 0, 0);
    }

    public void Lock()
    {
        IsLocked = true;
    }

    public void Unlock()
    {
        IsLocked = false;
    }
    
    public void Hide()
    {
        IsHided = true;
    }
    
    public CellData Cell { get; private set; }
    public bool IsLocked { get; private set; }
    public bool IsOpened { get; private set; }
    public bool IsHided { get; private set; }
}