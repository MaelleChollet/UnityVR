using UnityEngine;

public class TargetHit : MonoBehaviour
{
    [Header("Effet visuel")]
    public GameObject hitEffectPrefab; // Particle System (explosion / fumée)

    public void Hit()
    {
        if (hitEffectPrefab != null)
        {
            GameObject effect = Instantiate(hitEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, 3f); // nettoie l'effet après 3 secondes
        }

        Destroy(gameObject);
    }
}