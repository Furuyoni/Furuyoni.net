using Furuyoni.Core.Crystals;
using Microsoft.Extensions.DependencyInjection;

namespace Furuyoni.Core;

public class StatusHub
{
    internal StatusHub(PlayerType displayFor, IServiceProvider provider)
    {
        var opponent = displayFor switch
        {
            PlayerType.PlayerA => PlayerType.PlayerB,
            PlayerType.PlayerB => PlayerType.PlayerA,
            _                  => throw new ArgumentOutOfRangeException()
        };

        Distance = provider.GetRequiredService<DistanceHandler>();
        Shadow   = provider.GetRequiredService<ShadowHandler>();
        PlayerA  = provider.GetRequiredKeyedService<Player>(PlayerType.PlayerA);
        PlayerB  = provider.GetRequiredKeyedService<Player>(PlayerType.PlayerB);

        (MySelf, Opponent) = displayFor switch
        {
            PlayerType.PlayerA => (PlayerA, PlayerB),
            PlayerType.PlayerB => (PlayerB, PlayerA),
            _                  => throw new ArgumentOutOfRangeException()
        };
        // Card         = provider.GetRequiredKeyedService<CardManager>(displayFor);
        // OpponentCard = provider.GetRequiredKeyedService<CardManager>(opponent).AsSecret();

        // TODO Grant card
    }

    public DistanceHandler Distance     { get; }
    public ShadowHandler   Shadow       { get; }
    public Player          PlayerA      { get; }
    public Player          PlayerB      { get; }
    public Player          MySelf       { get; }
    public Player          Opponent     { get; }
    public CardManager     Card         { get; }
    public CardManager     OpponentCard { get; }

    public bool Resolve => MySelf.Life.Resolve;

    // public ICollection<Card> GrantCard         { get; }
    // public ICollection<Card> OpponentGrantCard { get; }
}