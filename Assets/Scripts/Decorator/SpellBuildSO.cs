using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Cards/Spell Build")]
public sealed class SpellBuildSO : ScriptableObject
{
    [SerializeField] private SpellDefinitionSO baseDefinition;
    [SerializeField] private List<SpellModSO> mods = new();

    public ISpell Build()
    {
        if (!baseDefinition) throw new System.InvalidOperationException("Base definition atanmadı.");
        ISpell s = baseDefinition.CreateBaseSpell();
        for (int i = 0; i < mods.Count; i++)
            if (mods[i]) s = mods[i].Wrap(s);
        return s;
    }

    public (int damage, int mana, string label) GetBaseStats()
    {
        var b = baseDefinition ? baseDefinition.CreateBaseSpell() : null;
        return b == null ? (0, 0, "<null>") : (b.Damage, b.ManaCost, b.Label);
    }
}
