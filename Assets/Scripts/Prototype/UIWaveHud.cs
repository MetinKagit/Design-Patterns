using UnityEngine;
using TMPro;

public class UIWaveHud : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI label;

    public void Set(int wave, int spawned)
    {
        if (label != null)
            label.text = $"Wave: {wave} | Spawned: {spawned}";
    }
}
