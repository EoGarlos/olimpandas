using System.Collections;
using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerPowerInventory.PowerType powerType;

    [Header("Respawn")]
    [SerializeField] private float respawnDelay = 5f;

    private SpriteRenderer spriteRenderer;

    private bool isAvailable = true;

    private void Awake()
    {
        spriteRenderer =
            GetComponent<SpriteRenderer>();
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

        StartCoroutine(
            RespawnRoutine()
        );
    }

    private IEnumerator RespawnRoutine()
    {
        isAvailable = false;

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        yield return new WaitForSeconds(
            respawnDelay
        );

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        isAvailable = true;
    }
}