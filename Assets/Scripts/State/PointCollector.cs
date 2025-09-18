using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Collider))]
public class PointCollector : MonoBehaviour
{
    [SerializeField] private int smallPointScore = 10;
    [SerializeField] private int bigPointScore = 50;

    public event Action<int> OnScoreChanged;
    public event Action OnBigPointCollected;

    private int totalScore;
    

    void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent<Point>(out var p))
            return;

        totalScore += p.isBig ? bigPointScore : smallPointScore;
        OnScoreChanged?.Invoke(totalScore);

        if (p.isBig)
            OnBigPointCollected?.Invoke();

        Destroy(p.gameObject);
    }
}
