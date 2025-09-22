public sealed class BasicSpell : ISpell
{
    public int Damage { get; }
    public int ManaCost { get; }
    public string Label { get; }

    public BasicSpell(int damage, int manaCost, string label)
    {
        Damage = damage;
        ManaCost = manaCost;
        Label = label;
    }

    public void Cast() { /* İleride event/log atabilirsin */ }
}
