using Zenject;

public class CardMatcher : ICardMatcher
{
    private Card _card1;
    private Card _card2;
    private readonly SignalBus _signalBus;

    private CardMatcher(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    public void AddCard(Card card)
    {
        if (_card1 == null)
        {
            _card1 = card;
            return;
        }

        _card2 = card;

        MatchCards(_card1, _card2);

        _card1 = null;
        _card2 = null;
    }

    public void MatchCards(Card card1, Card card2)
    {
        if (card1.Id == card2.Id)
            _signalBus.Fire(new CardMatchSignal
            {
                Card1 = card1,
                Card2 = card2
            });
        else
            _signalBus.Fire(new CardNotMatchedSignal());
    }
}