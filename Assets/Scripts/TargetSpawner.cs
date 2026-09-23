using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Références")]
    public GameObject targetPrefab;
    public Transform[] spawnPoints; // placer plusieurs Transforms vides dans la scène

    [Header("Timing")]
    public float minSpawnInterval = 2f;
    public float maxSpawnInterval = 5f;

    void Start()
    {
        ScheduleNextSpawn();
    }

    void ScheduleNextSpawn()
    {
        float delay = Random.Range(minSpawnInterval, maxSpawnInterval);
        Invoke(nameof(SpawnTarget), delay);
    }

    void SpawnTarget()
    {
        if (spawnPoints != null && spawnPoints.Length > 0)
        {
            Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Instantiate(targetPrefab, point.position, point.rotation);
        }

        ScheduleNextSpawn();
    }
}