using Furuyoni.Core.Enums;

namespace Furuyoni.Core.Exceptions;

public class FatalDamageException : Exception
{
    public FatalDamageException(PlayerType player) { Player = player; }
    public PlayerType Player { get; }
}