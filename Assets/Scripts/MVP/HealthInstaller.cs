using UnityEngine;

public sealed class HealthInstaller : MonoBehaviour
{
    [Header("Bindings")]
    [SerializeField] private HealthView view;

    [Header("Config")]
    [SerializeField, Min(1)] private int maxHp = 100;
    [SerializeField] private int startHp = 100;
    [SerializeField, Min(1)] private int step = 10;

    private HealthModel model;
    private HealthPresenter presenter;

    private void Awake()
    {
        if (!view)
        {
            Debug.LogError($"{nameof(HealthInstaller)}: {nameof(view)}  must be assigned.");
            enabled = false;
            return;
        }
    }

    private void Start()
    {
        model = new HealthModel(maxHp, startHp);
        presenter = new HealthPresenter(model, view, step);
    }

    private void OnDestroy()
    {
        presenter?.Dispose();
        presenter = null;
        model = null;
    }
}
