using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Mods/Damage Boost")]
public sealed class DamageBoostSO : SpellModSO
{
    [SerializeField] private int amount = 2;
    public override ISpell Wrap(ISpell inner) => new DamageBoost(inner, amount);
}
