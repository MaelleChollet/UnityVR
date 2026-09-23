using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 5f;

    void Start()
    {
        // Détruit la balle automatiquement si elle ne touche rien (évite d'encombrer la scène)
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Target"))
        {
            TargetHit target = collision.gameObject.GetComponent<TargetHit>();
            if (target != null)
            {
                Rigidbody bulletRb = GetComponent<Rigidbody>();
                Vector3 travelDirection = (bulletRb != null && bulletRb.linearVelocity.sqrMagnitude > 0.01f)
                    ? bulletRb.linearVelocity.normalized
                    : transform.forward;

                target.Hit(travelDirection);
            }
            Destroy(gameObject);
        }
    }
}