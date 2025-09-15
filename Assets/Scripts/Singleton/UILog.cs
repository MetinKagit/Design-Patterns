using UnityEngine;
using TMPro;
public class UILog : MonoBehaviour
{
    [SerializeField] TMP_Text logText;

    public void OnSingletonClick()
    {
        SingletonLogger.Instance.Write("Clicked via Singleton");
        logText.text = SingletonLogger.Instance.ReadAll();
    }

    public void OnLocatorClick()
{
    Services.Log.Write("Clicked via Service Locator");
    logText.text = Services.Log.ReadAll();
}
}
