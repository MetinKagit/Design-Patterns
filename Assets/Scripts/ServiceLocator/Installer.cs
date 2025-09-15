using UnityEngine;

public class Installer : MonoBehaviour
{
    void Awake()
    {
        Services.Provide(new MemoryLog());
    }
}
