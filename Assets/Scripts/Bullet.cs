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
                target.Hit();
            }
            Destroy(gameObject);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Ralentit progressivement la balle tant qu'elle est en contact avec une surface
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.linearVelocity *= 0.98f;
    }
}