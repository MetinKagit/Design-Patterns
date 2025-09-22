using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Spell Definition")]
public sealed class SpellDefinitionSO : ScriptableObject
{
    [SerializeField] private int baseDamage = 5;
    [SerializeField] private int baseMana   = 3;
    [SerializeField] private string label   = "Fireball";

    public ISpell CreateBaseSpell() => new BasicSpell(baseDamage, baseMana, label);
}
