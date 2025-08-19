using UnityEngine;
using UnityEngine.UI;

public class AchievementIcon_Mailbox : MonoBehaviour, IObserver<MailboxChecked>
{
    [SerializeField] private MailboxSubject subject;

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
        if (subject) OnNotify(new MailboxChecked(subject.Done));
    }

    private void OnDisable() { subject?.Unsubscribe(this); }

    public void OnNotify(MailboxChecked data)
    {
        if (achieved) return;
        if (!data.Done) return;

        achieved = true;
        if (icon && coloredSprite) icon.sprite = coloredSprite;
    }
}
