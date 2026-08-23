using UnityEngine;

public class SlowProjectile : MonoBehaviour
{
    [Header("Movimento")]
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 3f;

    [Header("Slow")]
    [Range(0.1f, 1f)]
    [SerializeField] private float slowMultiplier = 0.45f;

    [SerializeField] private float slowDuration = 2f;

    private Rigidbody2D rb;
    private Collider2D projectileCollider;

    private GameObject owner;
    private float direction = 1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        projectileCollider = GetComponent<Collider2D>();
    }

    public void Initialize(
        GameObject projectileOwner,
        float facingDirection
    )
    {
        owner = projectileOwner;

        direction =
            facingDirection >= 0f
                ? 1f
                : -1f;

        IgnoreOwnerCollisions();

        rb.linearVelocity = new Vector2(
            direction * speed,
            0f
        );

        Destroy(gameObject, lifetime);
    }

    private void IgnoreOwnerCollisions()
    {
        if (owner == null || projectileCollider == null)
            return;

        Collider2D[] ownerColliders =
            owner.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D ownerCollider in ownerColliders)
        {
            Physics2D.IgnoreCollision(
                projectileCollider,
                ownerCollider,
                true
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (owner != null)
        {
            if (
                other.gameObject == owner ||
                other.transform.IsChildOf(owner.transform)
            )
            {
                return;
            }
        }

        PlayerStatusEffects statusEffects =
            other.GetComponent<PlayerStatusEffects>();

        if (statusEffects != null)
        {
            statusEffects.ApplySlow(
                slowMultiplier,
                slowDuration
            );

            Destroy(gameObject);
            return;
        }

        Destroy(gameObject);
    }
}