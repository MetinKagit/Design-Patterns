using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPresenter : MonoBehaviour
{
    public enum ApplyMode {Toggle}

    [Header("View Binding")]
    [SerializeField] private CardViewUnity view;

    [Header("Build Source")]
    [SerializeField] private SpellBuildSO buildAsset; 

    [Header("Booster apply mode")]
    [SerializeField] private ApplyMode mode = ApplyMode.Toggle;

    [Header("Toggle Mode Settings (mode==Toggle)")]
    [SerializeField] private bool addDamageBoost = false;
    [SerializeField] private int  damageAmount   = 2;
    [SerializeField] private bool addManaDiscount = false;
    [SerializeField] private int  manaDelta       = 1;

    [SerializeField] private Button toggleDamageButton;
    [SerializeField] private Button toggleManaButton;
    
    private readonly List<Func<ISpell, ISpell>> runtimeWrappers = new();

    private void Awake()
    {
        if (!view) throw new InvalidOperationException("CardPresenter: CardViewUnity not initialized .");

    }

    private void Start() => RebuildAndRender();
    public void ToggleDamageBoost()
    {
        addDamageBoost = !addDamageBoost;
        toggleDamageButton.gameObject.SetActive(false);
        RebuildAndRender();
    
    }

    public void ToggleManaDiscount()
    {
        addManaDiscount = !addManaDiscount;
        toggleManaButton.gameObject.SetActive(false);
        RebuildAndRender();
        
    }


     public void RebuildAndRender()
    {
        var (baseDmg, baseMana, _) = buildAsset.GetBaseStats();
        ISpell finalSpell = buildAsset.Build();   // Mods boşsa sadece base döner

        if (mode == ApplyMode.Toggle)
        {
            if (addDamageBoost) finalSpell = new DamageBoost(finalSpell, damageAmount);
            if (addManaDiscount) finalSpell = new ManaDiscount(finalSpell, manaDelta);
        }
        else 
        {
            for (int i = 0; i < runtimeWrappers.Count; i++)
                finalSpell = runtimeWrappers[i](finalSpell);
        }

        int finalDmg  = finalSpell.Damage;
        int finalMana = finalSpell.ManaCost;

        int diffD = finalDmg  - baseDmg;
        int diffM = finalMana - baseMana;

        view.SetDamageText(finalDmg.ToString());
        view.SetManaText(finalMana.ToString());

        if (diffD > 0)      view.SetDamageColor(view.GoodColor);
        else if (diffD < 0) view.SetDamageColor(view.BadColor);
        else                view.SetDamageColor(view.DefaultColor);

        if (diffM < 0)      view.SetManaColor(view.GoodColor);
        else if (diffM > 0) view.SetManaColor(view.BadColor);
        else                view.SetManaColor(view.DefaultColor);

        if (diffD != 0) view.PulseDamage();
        if (diffM != 0) view.PulseMana();

        view.SetHintText($"Base {baseDmg}/{baseMana} → Final {finalDmg}/{finalMana}");
    }

}

