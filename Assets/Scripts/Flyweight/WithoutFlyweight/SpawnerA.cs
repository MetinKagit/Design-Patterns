using UnityEngine;

public class SpawnerA : MonoBehaviour
{
    [Header("Spawn Ayarları")]
    [SerializeField] private GameObject carrotPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private int spawnCount = 1500;
    [SerializeField] private Vector3 randomXYJitter = new Vector3(2f, 0f, 2f); // Random jitter to apply to the spawn position

    void Start()
    {
        SpawnNaive();
    }
    
    private void SpawnNaive()
    {
        for (int i = 0; i < spawnCount; i++)
        {

            Vector3 pos = spawnPoint.position + new Vector3(
                Random.Range(-randomXYJitter.x, randomXYJitter.x),
                Random.Range(0f, 0.5f),
                Random.Range(-randomXYJitter.z, randomXYJitter.z)
            );

            // Instantiate a new CarrotA GameObject
            GameObject go = Instantiate(carrotPrefab, pos, Quaternion.identity);

            // CarrotA’ya pozisyon/rotasyon bilgisi gönder
            if (go.TryGetComponent<CarrotA>(out var carrot))
            {
                carrot.PrepareForSpawn(pos, Quaternion.identity);
            }
        }
    }
}
