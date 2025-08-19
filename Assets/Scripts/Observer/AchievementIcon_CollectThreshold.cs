using UnityEngine;
using UnityEngine.UI;

public class AchievementIcon_CollectThreshold : MonoBehaviour, IObserver<ItemCollected>
{
    [SerializeField] private CollectSubject subject;
    [SerializeField] private ItemType targetType = ItemType.Carrot;
    [SerializeField] private int threshold = 9;

    [Header("UI")]
    [SerializeField] private Image icon;
    [SerializeField] private Sprite grayscaleSprite;
    [SerializeField] private Sprite coloredSprite;

    private bool achieved;

    private void Awake()
    {
        if (icon && grayscaleSprite) icon.sprite = grayscaleSprite; 
    }

    private void OnEnable()
    {
        subject?.Subscribe(this);
       
        if (subject && !achieved)
        {
            int count = subject.GetCount(targetType);
            OnNotify(new ItemCollected(targetType, count));
        }
    }

    private void OnDisable() { subject?.Unsubscribe(this); }

    public void OnNotify(ItemCollected data)
    {
        if (achieved) return;
        if (data.Type != targetType) return;

        if (data.Total >= threshold)
        {
            achieved = true;
            if (icon && coloredSprite) icon.sprite = coloredSprite;
        }
    }
}
