namespace Furuyoni.Core;

public class ForceManager
{
    public const int MaxForce = 2;

    // private readonly ILogger<ForceManager> _logger;
    // private readonly PlayerType            _player;
    // private readonly TurnManager           _turn;

    // public ForceManager(ILogger<ForceManager> logger, [ServiceKey] PlayerType player, TurnManager turn)
    // {
    //     _logger = logger;
    //     _player = player;
    //     _turn   = turn;
    // }

    public int Value { get; private set; }

    public bool Cringe { get; internal set; }

    public void Add()
    {
        if (Cringe) { Cringe = false; }
        else { Value         = Math.Min(MaxForce, Value + 1); }
    }
}