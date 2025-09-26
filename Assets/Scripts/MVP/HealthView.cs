using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthView : MonoBehaviour, IHealthView
{
    [Header("UI Binding")]
    [SerializeField] private Slider hpSlider;      
    [SerializeField] private TMP_Text hpText;    
    [SerializeField] private Button damageButton;
    [SerializeField] private Button healButton;

    public event Action OnDamageClicked;
    public event Action OnHealClicked;

    private void Awake()
    {
        if (!hpSlider)
            Debug.LogError(nameof(HealthView) + ": hpSlider can't found.");
        if (!hpText)
            Debug.LogError(nameof(HealthView) + ": hpText can't found.");
        if (!damageButton)
            Debug.LogError(nameof(HealthView) + ": damageButton can't found.");
        if (!healButton)
            Debug.LogError(nameof(HealthView) + ": healButton can't found.");
    }

    private void OnEnable()
    {
        if (damageButton)
            damageButton.onClick.AddListener(HandleDamage);
        if (healButton)
            healButton.onClick.AddListener(HandleHeal);
    }

    private void OnDisable()
    {
        if (damageButton)
            damageButton.onClick.RemoveListener(HandleDamage);
        if (healButton)
            healButton.onClick.RemoveListener(HandleHeal);
    }

    private void HandleDamage() => OnDamageClicked?.Invoke();
    private void HandleHeal()   => OnHealClicked?.Invoke();

    public void SetBar(float normalized)
    {
        if (!hpSlider)
            return;

        hpSlider.value = Mathf.Clamp01(normalized);
    }

    public void SetText(string text)
    {
        if (!hpText)
            return;
        hpText.text = text ?? "";
    }
}
