using System;

public interface IHealthView
{
    void SetBar(float normalized);

    void SetText(string text);

    event Action OnDamageClicked;
    event Action OnHealClicked;

}