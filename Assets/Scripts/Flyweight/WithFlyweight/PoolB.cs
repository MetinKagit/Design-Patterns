using System.Collections.Generic;
using UnityEngine;

public class PoolB : MonoBehaviour
{
    [Header("Pool Ayarları")]
    [SerializeField] private GameObject prefab;
    [SerializeField, Min(0)] private int prewarmCount = 0;
    [SerializeField] private bool allowExpand = true;
    [SerializeField, Min(1)] private int maxCapacity = 5000;

    private readonly Stack<GameObject> _inactive = new Stack<GameObject>();
    private int _totalCreated;

    void Awake()
    {
        if (prefab == null)
        {
            Debug.LogError("PoolB: Prefab atanmamış.", this);
            enabled = false;
            return;
        }

        for (int i = 0; i < prewarmCount; i++)
        {
            var go = CreateInstance();
            InternalReturn(go);
        }
    }

    private GameObject CreateInstance()
    {
        if (_totalCreated >= maxCapacity)
        {
            Debug.LogWarning("PoolB: Max kapasiteye ulaşıldı.");
            return null;
        }

        var go = Instantiate(prefab, transform);
        go.SetActive(false);
        _totalCreated++;
        return go;
    }

    public GameObject Take()
    {
        GameObject go = _inactive.Count > 0 ? _inactive.Pop() : null;

        if (go == null)
        {
            if (!allowExpand) return null;
            go = CreateInstance();
            if (go == null) return null;
        }

        go.SetActive(true);
        return go;
    }

    public void Return(GameObject go)
    {
        if (go == null) return;

        if (go.TryGetComponent<CarrotB>(out var carrot))
            carrot.OnDespawn();

        InternalReturn(go);
    }

    private void InternalReturn(GameObject go)
    {
        go.SetActive(false);
        go.transform.SetParent(transform, false);
        _inactive.Push(go);
    }

    public int CountInactive => _inactive.Count;
    public int CountTotal => _totalCreated;
}
