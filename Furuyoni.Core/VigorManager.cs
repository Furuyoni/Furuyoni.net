namespace Furuyoni.Core;

public class VigorManager
{
    public const int MaxVigor = 2;

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

    public bool Flinch { get; internal set; }

    public void Add()
    {
        if (Flinch) { Flinch = false; }
        else { Value         = Math.Min(MaxVigor, Value + 1); }
    }

    public void Minus() { Value = Math.Max(0, Value - 1); }
}