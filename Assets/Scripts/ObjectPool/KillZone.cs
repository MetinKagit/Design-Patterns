using UnityEngine;

[RequireComponent(typeof(Collider))]
public class KillZone : MonoBehaviour
{
    [SerializeField] private string carrotTag = "Carrot";
    private void OnTriggerEnter(Collider other)
    {
        var carrot = other.GetComponentInParent<Carrot>();
        if (carrot == null)
            return;

        if (!string.IsNullOrEmpty(carrotTag) && !carrot.CompareTag(carrotTag))
            return;

        carrot.ReleaseSelf();
    }
}

