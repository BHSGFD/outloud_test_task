using System;
using Zenject;

public class CardMiniGame : IMiniGame
{
    private readonly ICardGameView _cardGameView;
    private readonly ICardMatcher _cardMatcher;
    private readonly ICardMiniGameDataProvider _dataProvider;
    private readonly SignalBus _signalBus;
    private readonly ITimerRunner _timerRunner;
    private CardMiniGameData _cardMiniGameData;
    private IConfigProvider _configProvider;
    private IEndScreenService _endScreen;
    private IMiniGame _miniGameImplementation;

    public CardMiniGame(ICardMiniGameDataProvider dataProvider, ICardGameView cardGameView, ICardMatcher cardMatcher,
        SignalBus signalBus, ITimerRunner timerRunner)
    {
        _cardGameView = cardGameView;
        _dataProvider = dataProvider;
        _cardMatcher = cardMatcher;
        _timerRunner = timerRunner;
        _signalBus = signalBus;

        signalBus.Subscribe<CardMatchSignal>(CardMatched);
        signalBus.Subscribe<CardOpenSignal>(OnCardOpened);
        signalBus.Subscribe<ShuffleCardSignal>(Hint);

        _timerRunner.OnTimerEnded += OnTimerEnded;
    }

    public void StartGame()
    {
        Prepare();
        _cardGameView.InitField(_cardMiniGameData);
        _timerRunner.Start();
        OnGameStarted?.Invoke();
    }

    public void EndGame()
    {
        OnGameEnded?.Invoke();
    }

    public Action OnGameStarted { get; set; }
    public Action OnGameEnded { get; set; }

    private void OnTimerEnded()
    {
        _signalBus.Fire(new MiniGameEndSignal { Type = MiniGameEndType.Lose });
    }

    private void OnCardOpened(CardOpenSignal signal)
    {
        _cardMatcher.AddCard(signal.Card);
    }

    private void CardMatched(CardMatchSignal signal)
    {
        _cardMiniGameData.Cards.Remove(signal.Card1);
        _cardMiniGameData.Cards.Remove(signal.Card2);

        if (_cardMiniGameData.Cards.Count == 0)
        {
            _signalBus.Fire(new MiniGameEndSignal { Type = MiniGameEndType.Win });
            _timerRunner.Stop();
        }
    }

    public void Prepare()
    {
        _cardMiniGameData = _dataProvider.CreateData();
        _cardGameView.Prepare(_cardMiniGameData);
    }

    public void Hint()
    {
        _cardMiniGameData.Cards.Shuffle();
        _cardGameView.ShuffleCard(_cardMiniGameData);
    }
}