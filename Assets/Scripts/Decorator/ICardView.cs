public interface ICardView
{
    void SetDamageText(string text);
    void SetManaText(string text);

    void SetDamageColor(UnityEngine.Color color);
    void SetManaColor(UnityEngine.Color color);
   
    void PulseDamage();
    void PulseMana();

    void SetHintText(string text);
}
