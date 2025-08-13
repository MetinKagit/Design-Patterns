using UnityEngine;

public class SpawnerB : MonoBehaviour
{
    [Header("Spawn Ayarları")]
    [SerializeField] private PoolB pool;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int spawnCount = 1500;
    [SerializeField] private Vector3 randomXYJitter = new Vector3(2f, 0f, 2f);

    void Start()
    {
        SpawnFromPool();
    }

    private void SpawnFromPool()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Vector3 pos = spawnPoint.position + new Vector3(
                Random.Range(-randomXYJitter.x, randomXYJitter.x),
                Random.Range(0f, 0.5f),
                Random.Range(-randomXYJitter.z, randomXYJitter.z)
            );

            var go = pool.Take();
            if (go != null && go.TryGetComponent<CarrotB>(out var carrot))
                carrot.PrepareForSpawn(pos, Quaternion.identity);
        }
    }
}
