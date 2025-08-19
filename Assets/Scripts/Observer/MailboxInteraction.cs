using UnityEngine;

[RequireComponent(typeof(Collider))]
public class MailboxInteraction : MonoBehaviour
{
    [SerializeField] private MailboxSubject subject;
    [SerializeField] private GameObject prompt;
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private bool inRange;
    private bool consumed;

    private void Reset()
    {
        var col = GetComponent<Collider>();
        col.isTrigger = true; 
    }

    private void Start()
    {
        if (subject && subject.Done && prompt) prompt.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (consumed || (subject && subject.Done)) return;
        if (!other.CompareTag("Player")) return;

        inRange = true;
        if (prompt) prompt.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        inRange = false;
        if (prompt) prompt.SetActive(false);
    }

    private void Update()
    {
        if (consumed || (subject && subject.Done)) return;
        if (!inRange) return;

        if (Input.GetKeyDown(interactKey))
        {
            consumed = true;
            subject?.ReportMailboxChecked();
            if (prompt) prompt.SetActive(false);
        }
    }
}
