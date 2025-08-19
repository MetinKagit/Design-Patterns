using UnityEngine;
using UnityEngine.UI;

public class AchievementIcon_JumpThreshold : MonoBehaviour, IObserver<Jumped>
{
    [SerializeField] private JumpSubject subject;
    [SerializeField] private int threshold = 12;

    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Sprite grayscaleSprite;
    [SerializeField] private Sprite coloredSprite;

    private bool achieved;

    private void Awake()
    {
        if (icon != null && grayscaleSprite != null)
            icon.sprite = grayscaleSprite; 
    }

    private void OnEnable()  { subject?.Subscribe(this); }
    private void OnDisable() { subject?.Unsubscribe(this); }

    public void OnNotify(Jumped data)
    {
        if (achieved) return;
        if (data.Total >= threshold)
        {
            achieved = true;
            if (icon != null && coloredSprite != null)
                icon.sprite = coloredSprite; 
        }
    }
}
