using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class RespawnIfFallen : MonoBehaviour
{
    [Header("Référence joueur")]
    [Tooltip("Généralement la Main Camera du XR Origin. Si laissé vide, récupérée automatiquement via Camera.main au démarrage.")]
    public Transform player;

    [Header("Paramètres de respawn")]
    [Tooltip("Si l'objet passe sous cette hauteur Y, il est considéré comme tombé dans le vide")]
    public float fallThreshold = -10f;
    [Tooltip("Distance devant le joueur à laquelle l'objet réapparaît")]
    public float respawnDistance = 0.6f;
    [Tooltip("Décalage vertical par rapport à la hauteur du joueur (négatif = plus bas, pratique pour réapparaître à hauteur de main plutôt qu'à hauteur des yeux)")]
    public float respawnHeightOffset = -0.3f;

    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();

        if (player == null && Camera.main != null)
        {
            player = Camera.main.transform;
        }
    }

    void Update()
    {
        if (transform.position.y < fallThreshold)
        {
            Respawn();
        }
    }

    private void Respawn()
    {
        // Sécurité : si jamais l'objet est tenu au moment du check, on ne le téléporte pas
        if (grabInteractable != null && grabInteractable.isSelected) return;
        if (player == null) return;

        Vector3 targetPosition = player.position + player.forward * respawnDistance;
        targetPosition.y = player.position.y + respawnHeightOffset;

        transform.position = targetPosition;
        transform.rotation = Quaternion.identity;

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}