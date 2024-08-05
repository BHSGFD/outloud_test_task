using System.Collections.Generic;
using UnityEngine;

public class CardMiniGameDataProvider : ICardMiniGameDataProvider
{
    private readonly IConfigProvider _configProvider;

    public CardMiniGameDataProvider(IConfigProvider configProvider)
    {
        _configProvider = configProvider;
    }

    public CardMiniGameData CreateData()
    {
        var config = _configProvider.ResolveConfig<CardGameConfig>();

        var cards = new List<Card>();
        for (var i = 0; i < Mathf.Pow(config.Size, 2) * 0.5f; i++)
        {
            var color = new Color(Random.Range(0, 1f), Random.Range(0, 1f), Random.Range(0, 1f), 1);
            cards.Add(new Card(color, i.ToString()));
            cards.Add(new Card(color, i.ToString()));
        }

        cards.Shuffle();

        var data = new CardMiniGameData(config.Size, cards);
        return data;
    }
}