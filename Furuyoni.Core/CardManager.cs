using Furuyoni.Core.Cards;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Furuyoni.Core;

// public class SecretCardManager
// {
//     private readonly CardManager _base;
//
//     public SecretCardManager(CardManager @base) { _base = @base; }
//
//     public int        DeckRemain => _base.DeckRemain;
//     public List<Card> Hand       => _base.Hand;
//     public List<Card> Discard    => _base.Discard;
//     public int        Covered    => _base.Covered.Count;
//     public int        HandLimit  => _base.HandLimit;
// }

public class CardManager
{
    public const int DefaultHandLimit = 2;

    private readonly List<Card> _deck = new(7);

    private readonly ILogger<CardManager> _logger;
    private readonly PlayerType           _playerType;
    private readonly IUserReaction        _userReaction;

    public CardManager(ILogger<CardManager> logger, [ServiceKey] PlayerType playerType, IServiceProvider provider)
    {
        _logger       = logger;
        _playerType   = playerType;
        _userReaction = provider.GetRequiredKeyedService<IUserReaction>(playerType);
    }

    public int DeckRemain => _deck.Count;

    public List<Card> Hand { get; } = new(7);

    public List<Card> Discard { get; } = new(7);

    public List<Card> Covered { get; } = new(7);

    public int HandLimit { get; internal set; } = DefaultHandLimit;

    // public SecretCardManager AsSecret() { return new SecretCardManager(this); }

    public async Task Rebuild()
    {
        var wantRebuild = await _userReaction.RebuildDeckAsync();
        if (!wantRebuild) { return; }

        _deck.AddRange(Discard);
        _deck.AddRange(Covered);
        Discard.Clear();
        Covered.Clear();
    }

    public ICollection<Card> Draw(int count)
    {
        var picked = new List<Card>(count);
        var i      = 0;
        while (i < count)
        {
            if (Hand.Count == 0) { return picked; }

            i++;
            var pick = Hand[Random.Shared.Next(0, Hand.Count)];
            Hand.Remove(pick);
            picked.Add(pick);
        }

        return picked;
    }

    public async Task DiscardOverloadAsync()
    {
        if (Hand.Count <= HandLimit) { return; }

        var discard = await _userReaction.DiscardOverloadAsync(Hand);
        foreach (var card in discard)
        {
            Hand.Remove(card);
            Covered.Add(card);
        }
    }
}