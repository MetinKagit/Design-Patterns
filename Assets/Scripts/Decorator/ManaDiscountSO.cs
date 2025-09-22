using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Mods/Mana Discount")]
public sealed class ManaDiscountSO : SpellModSO
{
    [SerializeField] private int amount = 1;
    public override ISpell Wrap(ISpell inner) => new ManaDiscount(inner, amount);
}
