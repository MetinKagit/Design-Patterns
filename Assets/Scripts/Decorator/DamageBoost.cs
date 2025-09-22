
public sealed class DamageBoost : SpellDecorator
{
    private readonly int amount;

    public DamageBoost(ISpell inner, int amount = 2) : base(inner)
    {
        this.amount = amount;
    }

    public override int Damage => Inner.Damage + amount;
    public override int ManaCost => Inner.ManaCost;
    public override string Label => $"{Inner.Label}+Dmg({amount})";
    public override void Cast() => Inner.Cast();
}
