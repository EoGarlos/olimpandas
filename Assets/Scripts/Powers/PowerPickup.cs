using UnityEngine;

public class PowerPickup : MonoBehaviour
{
    [SerializeField]
    private PlayerPowerInventory.PowerType powerType;

    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerPowerInventory inventory =
            other.GetComponent<PlayerPowerInventory>();

        if (inventory == null)
            return;

        inventory.GrantPower(powerType);

        Destroy(gameObject);
    }
}