using TMPro;
using UnityEngine;

public class HUDCounter : MonoBehaviour, IObserver<ItemCollected>{
    [SerializeField] private CollectSubject subject;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI carrotText;
    [SerializeField] private TextMeshProUGUI cauliflowerText;
    
    private void Awake()
    {
        if (carrotText) carrotText.text = "0";
        if (cauliflowerText) cauliflowerText.text = "0";
    }

    private void OnEnable()  { subject?.Subscribe(this); }
    private void OnDisable() { subject?.Unsubscribe(this); }

    public void OnNotify(ItemCollected data)
    {
        if (data.Type == ItemType.Carrot && carrotText)
            carrotText.text = data.Total.ToString();
        else if (data.Type == ItemType.Cauliflower && cauliflowerText)
            cauliflowerText.text = data.Total.ToString();
    }

}
