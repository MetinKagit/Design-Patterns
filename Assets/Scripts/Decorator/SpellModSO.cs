using UnityEngine;

public abstract class SpellModSO : ScriptableObject
{
    public abstract ISpell Wrap(ISpell inner);
}
