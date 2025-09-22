using System;

public abstract class SpellDecorator : ISpell
{
    protected readonly ISpell Inner;

    protected SpellDecorator(ISpell inner)
    {
        Inner = inner ?? throw new ArgumentNullException(nameof(inner));
    }

    public virtual int Damage   => Inner.Damage;
    public virtual int ManaCost => Inner.ManaCost;
    public virtual string Label => Inner.Label;

    public virtual void Cast() => Inner.Cast();
}
