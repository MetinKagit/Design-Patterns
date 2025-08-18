using UnityEngine;

public class AchievementMenuController : MonoBehaviour
{
    [SerializeField] private GameObject achievementPanel;

    // Açma butonuna bağla
    public void OpenPanel()
    {
        if (achievementPanel != null)
            achievementPanel.SetActive(true);
    }

    // Kapatma butonuna bağla
    public void ClosePanel()
    {
        if (achievementPanel != null)
            achievementPanel.SetActive(false);
    }
}
