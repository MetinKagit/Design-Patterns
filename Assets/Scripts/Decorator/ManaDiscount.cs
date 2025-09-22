
public sealed class ManaDiscount : SpellDecorator
{
    private readonly int delta;

    public ManaDiscount(ISpell inner, int amount = 1) : base(inner)
    {
        this.delta = amount;
    }
    
    public override int ManaCost => System.Math.Max(0, Inner.ManaCost - delta);
    public override string Label => $"{Inner.Label}-Mana({delta})";
}
