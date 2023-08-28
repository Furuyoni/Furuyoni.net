using Furuyoni.Core.Cards;
using Furuyoni.Core.Crystals;
using Furuyoni.Core.Enums;
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

        Distance     = provider.GetRequiredService<DistanceHandler>();
        Void         = provider.GetRequiredService<VoidHandler>();
        Player       = provider.GetRequiredKeyedService<Player>(displayFor);
        Opponent     = provider.GetRequiredKeyedService<Player>(opponent);
        Card         = provider.GetRequiredKeyedService<CardManager>(displayFor);
        OpponentCard = provider.GetRequiredKeyedService<CardManager>(opponent).AsSecret();

        // TODO Grant card
    }

    public DistanceHandler   Distance     { get; }
    public VoidHandler       Void         { get; }
    public Player            Player       { get; }
    public Player            Opponent     { get; }
    public CardManager       Card         { get; }
    public SecretCardManager OpponentCard { get; }

    public ICollection<Card> GrantCard         { get; }
    public ICollection<Card> OpponentGrantCard { get; }
}