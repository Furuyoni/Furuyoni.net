namespace Furuyoni.Core.Cards;

public abstract class SpecialCard : Card
{
    public abstract int? Cost { get; }

    // public abstract int  CalculateCost();
}

public abstract class ResurgableSpecialCard : SpecialCard
{
    public abstract bool CheckResurgence();
}

public abstract class ImmediateResurgableSpecialCard : ResurgableSpecialCard
{
}