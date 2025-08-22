using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemySpawner : MonoBehaviour
{
    [Header("References")]
    public GameObject enemyPrefab;
    public Transform[] spawnPoints;
    public Transform parent;
    public UIWaveHud hud;

    [Header("Data Reference")]
    public EnemyData baseEnemyData;

    [Header("Wave Modifiers")]
    public int enemiesPerWave = 5;
    public float spawnInterval = 0.4f;
    public float waveInterval = 2f;

    [Header("Variation Presets")]
    public List<WaveModifier> waveModifiers = new();

    private int currentWave = 0;
    private int spawnedTotal = 0;
    private bool running;
    public Transform goalTransform;

    private void Start()
    {

        if (enemyPrefab == null || baseEnemyData == null || spawnPoints == null || spawnPoints.Length == 0)
        {
            Debug.LogError("Spawner: prefab/data/spawn points missing.");
            return;
        }

        running = true;
        StartCoroutine(RunWaves());

    }

    private IEnumerator RunWaves()
    {
        while (running)
        {
            currentWave++;

            for (int i = 0; i < enemiesPerWave; i++)
            {
                var data = baseEnemyData.Clone();

                if (waveModifiers != null && waveModifiers.Count > 0)
                {
                    var mod = waveModifiers[(currentWave - 1) % waveModifiers.Count];
                    mod.Apply(data);
                }

                Transform sp = spawnPoints[i % spawnPoints.Length];
                var go = Instantiate(enemyPrefab, sp.position, Quaternion.identity, parent);

                var enemy = go.GetComponent<Enemy>();

                if (enemy != null)
                    enemy.Init(data);

                var move = go.GetComponent<EnemyMove>();
                if (move != null)
                    move.target = goalTransform;

                spawnedTotal++;
                hud?.Set(currentWave, spawnedTotal);
                yield return new WaitForSeconds(spawnInterval);
            }

            yield return new WaitForSeconds(waveInterval);
 
        }
    }

}
