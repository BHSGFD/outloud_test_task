using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;
using Random = UnityEngine.Random;

public class FieldDrawer : MonoBehaviour, IFieldDrawer
{
    [SerializeField] private RectTransform _cardContainer;
    private  IAddressableAssetProvider _assetProvider;
    private  IFieldProvider _fieldProvider;
    private CardView[] _cardViews;
    
    private  DiContainer _container;
    private Vector2 _offset;
    private float _size;

    [Inject]
    public void Construct(IFieldProvider provider,
        IAddressableAssetProvider assetProvider, DiContainer container)
    {
        _fieldProvider = provider;
        _assetProvider = assetProvider;
        _container = container;
    }

    public void DrawField(Action onComplete)
    {
        UpdateParams();
        UpdateField(destroyCancellationToken)
            .ContinueWith(() => onComplete?.Invoke()).Forget();
    }

    public void Reshuffle(Action onComplete)
    {
        ReshuffleTask(destroyCancellationToken).ContinueWith(() => onComplete?.Invoke()).Forget();
    }

    public CardView GetCard(Card card)
    {
        return _cardViews.FirstOrDefault(view => view.Cell.Card == card);
    }

    public void OpenAllCards()
    {
        foreach (var card in _cardViews)
            card.Open();
    }

    public void CloseAllCards()
    {
        foreach (var card in _cardViews)
            card.Close();
    }

    public void Lock()
    {
        foreach (var card in _cardViews)
            card.Lock();
    }

    public void Unlock()
    {
        foreach (var card in _cardViews)
            card.Unlock();
    }

    public List<CardView> GetCardsInGame()
    {
        return _cardViews.Where(view => view.Cell.Card != null).ToList();
    }

    private async UniTask ReshuffleTask(CancellationToken token)
    {
        for (var index = 0; index < _fieldProvider.Field.FieldGrid.Length; index++)
        {
            var cell = _fieldProvider.Field.FieldGrid[index];
            var view = _cardViews[(int)(cell.Position.x * _fieldProvider.Field.Size + cell.Position.y)];

            if(cell.Card == null) continue;
            
            view.UpdateView(_size * 0.7f);

            var xPos = _offset.x + cell.Position.x * (_size + cell.Position.x) + _size * 0.5f;
            var yPos = _offset.y + cell.Position.y * (_size + cell.Position.y) + _size * 0.5f;
            view.MoveWithRotation(new Vector2(xPos, -yPos), Random.Range(-15, 15));
            await UniTask.Delay(50, cancellationToken: token);
            token.ThrowIfCancellationRequested();
        }
    }

    private void UpdateParams()
    {
        var field = _fieldProvider.Field;
        var panelWidth = _cardContainer.rect.width;
        var panelHeight = _cardContainer.rect.height;
        
        _size = _cardContainer.rect.width / field.Size;
        _offset = new Vector2((panelWidth - field.Size * _size) / (field.Size + 1),
            (panelHeight - field.Size * _size) / (field.Size + 1));
        _cardViews = new CardView[(int)Mathf.Pow(field.Size, 2)];
    }

    private async UniTask UpdateField(CancellationToken token)
    {
        var card = await _assetProvider.LoadGameObjectAsset<CardView>(new AssetReference("Card"));

        foreach (var cell in _fieldProvider.Field.FieldGrid)
        {
            await UniTask.WaitForSeconds(0.05f, cancellationToken: token);
            token.ThrowIfCancellationRequested();

            var cardView = _container.InstantiatePrefabForComponent<CardView>(card, transform);
            _cardViews[(int)(cell.Position.x * _fieldProvider.Field.Size + cell.Position.y)] = cardView;
            cardView.Init(cell, _size * 0.7f);

            var xPos = _offset.x + cell.Position.x * (_size + cell.Position.x) + _size * 0.5f;
            var yPos = _offset.y + cell.Position.y * (_size + cell.Position.y) + _size * 0.5f;
            cardView.MoveWithRotation(new Vector2(xPos, -yPos), Random.Range(-15, 15));
        }
    }
}