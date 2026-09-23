using UnityEngine;

public class TargetHit : MonoBehaviour
{
    [Header("Effet visuel")]
    public GameObject hitEffectPrefab; // Particle System (explosion / fumée)

    [Header("Durée de vie")]
    [Tooltip("Temps en secondes avant que la cible se détruise toute seule si elle n'est jamais touchée")]
    public float lifeTime = 8f;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void Hit(Vector3 travelDirection)
    {
        if (hitEffectPrefab != null)
        {
            // Oriente l'effet dans le sens du tir : il continue "vers l'arrière" de la cible
            Quaternion effectRotation = travelDirection.sqrMagnitude > 0.01f
                ? Quaternion.LookRotation(travelDirection)
                : Quaternion.identity;

            // Le Particle System se détruit lui-même via son réglage "Stop Action = Destroy"
            Instantiate(hitEffectPrefab, transform.position, effectRotation);
        }

        Destroy(gameObject);
    }
}