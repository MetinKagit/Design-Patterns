using UnityEngine;

public class CarrotSpawner : MonoBehaviour
{
    [Header("Pool")]
    [SerializeField] private CarrotPool pool;
    [SerializeField] private int warmupCount = 20;

    [Header("Spawn")]
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector3 launchForce = new Vector3(0, 6, 12);
    [SerializeField, Min(0.01f)] private float spawnInterval = 0.25f;
    [SerializeField, Min(0f)] private float startDelay = 0.2f;


    [Header("Optional Randomness")]
    [SerializeField] private float forceJitter = 0f;
    [SerializeField] private float posJitter = 0f;

    private float _timer;
    private bool _started;

    private void Awake()
    {
        if (pool == null)
        {
            Debug.LogError("[CarrotSpawner] Pool not found.");
            enabled = false;
            return;
        }

        pool.Initialize();

        if (warmupCount > 0)
            pool.WarmUp(warmupCount);

        _timer = -startDelay;
        _started = true;

        if (spawnPoint == null)
            spawnPoint = transform;
    }

    private void Update()
    {
        if (!_started)
            return;
        _timer += Time.deltaTime;

        while (_timer >= spawnInterval)
        {
            _timer -= spawnInterval;

            // Pozisyon/rotasyon
            Vector3 pos = spawnPoint.position;
            if (posJitter > 0f)
            {
                pos += new Vector3(
                    Random.Range(-posJitter, posJitter),
                    Random.Range(-posJitter, posJitter),
                    Random.Range(-posJitter, posJitter)
                );
            }

            var carrot = pool.Get(pos, spawnPoint.rotation);

            // Kuvvet
            Vector3 force = launchForce;
            if (forceJitter > 0f)
            {
                force += new Vector3(
                    Random.Range(-forceJitter, forceJitter),
                    Random.Range(-forceJitter, forceJitter),
                    Random.Range(-forceJitter, forceJitter)
                );
            }

            carrot.Launch(force);
        }
    }
}