using UnityEngine;
using TMPro;
using System.Collections;
public sealed class CardViewUnity : MonoBehaviour, ICardView
{
    [Header("Texts")]
    [SerializeField] private  TextMeshProUGUI damageText;
    [SerializeField] private TextMeshProUGUI manaText;
    [SerializeField] private TextMeshProUGUI hintText;

    [Header("Colors")]
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private Color goodColor = new Color(0f, 0.8f, 0.3f); // green
    [SerializeField] private Color badColor = new Color(0.9f, 0.2f, 0.2f); // red

    public Color DefaultColor => defaultColor;
    public Color GoodColor => goodColor;
    public Color BadColor => badColor;

    public void SetDamageText(string text)
    {
        if (damageText)
            damageText.text = text;
    }
    public void SetManaText(string text)
    {
        if (manaText)
            manaText.text = text;
    }

    public void SetDamageColor(Color color)
    {
        if (damageText)
            damageText.color = color;
    }
    public void SetManaColor(Color color)
    {
        if (manaText)
            manaText.color = color;
    }
    public void SetHintText(string text)
    {
        if (hintText)
            hintText.text = text;
    }

    public void PulseDamage()
    {
        if (damageText)
            StartCoroutine(PulseRoutine(damageText.rectTransform));
    }
    public void PulseMana()
    {
        if (manaText)
            StartCoroutine(PulseRoutine(manaText.rectTransform));
    }

    private IEnumerator PulseRoutine(RectTransform target)
    {
        if (!target)
            yield break;

        const float dur = 0.12f;
        Vector3 start = target.localScale;
        Vector3 end   = start * 1.12f;

        float t = 0f;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;
            float a = t / dur;
            target.localScale = Vector3.Lerp(start, end, a);
            yield return null;
        }

        t = 0f;
        while (t < dur)
        {
            t += Time.unscaledDeltaTime;
            float a = t / dur;
            target.localScale = Vector3.Lerp(end, start, a);
            yield return null;
        }
        target.localScale = start;
    }

}
