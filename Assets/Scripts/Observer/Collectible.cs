using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Collectible : MonoBehaviour
{
    public ItemType type = ItemType.Carrot;
    [SerializeField] private CollectSubject collectSubject;
    [SerializeField] private bool destroyOnCollect = true;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (collectSubject != null)
            collectSubject.ReportCollect(type);

        if (destroyOnCollect) Destroy(gameObject);
        else gameObject.SetActive(false);
    }
}
