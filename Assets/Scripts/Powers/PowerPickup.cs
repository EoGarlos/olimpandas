using System.Collections;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerPowerInventory.PowerType powerType;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 5f;

    private SpriteRenderer spriteRenderer;
    private Collider2D pickupCollider;

    private bool isAvailable = true;

    private void Awake()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();

        pickupCollider =
            GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAvailable)
            return;

        PlayerPowerInventory inventory =
            other.GetComponent<PlayerPowerInventory>();

        if (inventory == null)
            return;

        inventory.GrantPower(powerType);

        StartCoroutine(RespawnRoutine());
    }

    private IEnumerator RespawnRoutine()
    {
        isAvailable = false;

        if (spriteRenderer != null)
            spriteRenderer.enabled = false;

        if (pickupCollider != null)
            pickupCollider.enabled = false;

        yield return new WaitForSeconds(
            respawnDelay
        );

        if (spriteRenderer != null)
            spriteRenderer.enabled = true;

        if (pickupCollider != null)
            pickupCollider.enabled = true;

        isAvailable = true;
    }
}