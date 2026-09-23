using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [Header("Références")]
    [Tooltip("Plusieurs prefabs possibles (formes/couleurs différentes) : un est choisi au hasard à chaque spawn")]
    public GameObject[] targetPrefabs;

    [Header("Zone de spawn aléatoire")]
    [Tooltip("Si laissé vide, utilise la position de ce GameObject comme centre de la zone")]
    public Transform centerPoint;
    [Tooltip("X = largeur de la zone, Z = profondeur de la zone (Y ignoré)")]
    public Vector3 areaSize = new Vector3(10f, 0f, 10f);
    public float spawnHeight = 1f;

    [Header("Timing")]
    public float minSpawnInterval = 1f;
    public float maxSpawnInterval = 3f;

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
        if (targetPrefabs != null && targetPrefabs.Length > 0)
        {
            GameObject prefabToSpawn = targetPrefabs[Random.Range(0, targetPrefabs.Length)];
            Instantiate(prefabToSpawn, GetRandomPositionInArea(), Quaternion.identity);
        }

        if (ScoreManager.Instance != null)
        {
            ScoreManager.Instance.RegisterTargetSpawned();
        }
        ScheduleNextSpawn();

    }

    Vector3 GetRandomPositionInArea()
    {
        Vector3 center = centerPoint != null ? centerPoint.position : transform.position;
        float x = center.x + Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float z = center.z + Random.Range(-areaSize.z / 2f, areaSize.z / 2f);
        return new Vector3(x, spawnHeight, z);
    }

    // Dessine la zone de spawn 
    void OnDrawGizmosSelected()
    {
        Vector3 center = centerPoint != null ? centerPoint.position : transform.position;
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(center.x, spawnHeight, center.z), new Vector3(areaSize.x, 0.1f, areaSize.z));
    }
}