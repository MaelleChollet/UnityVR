using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class GunShooter : MonoBehaviour
{
    [Header("Références")]
    public GameObject bulletPrefab;
    public Transform muzzlePoint;

    [Header("Paramètres de tir")]
    public float bulletSpeed = 15f;
    public float fireRate = 0.2f; // délai minimum entre deux tirs

    private float lastFireTime;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.activated.AddListener(OnTrigger);
    }

    void OnDestroy()
    {
        grabInteractable.activated.RemoveListener(OnTrigger);
    }

    private void OnTrigger(ActivateEventArgs args)
    {
        if (Time.time - lastFireTime < fireRate) return;
        lastFireTime = Time.time;
        Shoot();
    }

    private void Shoot()
    {
        if (bulletPrefab == null || muzzlePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, muzzlePoint.position, muzzlePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            rb.linearVelocity = muzzlePoint.forward * bulletSpeed;
        }

        // Empêche la balle d'entrer en collision avec le pistolet lui-même à l'instant du spawn
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Collider[] gunColliders = GetComponentsInChildren<Collider>();
        if (bulletCollider != null)
        {
            foreach (Collider gunCollider in gunColliders)
            {
                Physics.IgnoreCollision(bulletCollider, gunCollider);
            }
        }
    }
}