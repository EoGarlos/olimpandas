using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class StunPowerController : MonoBehaviour
{
    [Header("Controle")]
    [SerializeField] private KeyCode powerKey = KeyCode.E;

    [Header("Disparo")]
    [SerializeField] private StunProjectile projectilePrefab;
    [SerializeField] private Transform powerSpawn;

    [Header("Cooldown")]
    [SerializeField] private float cooldown = 2.5f;

    private PlayerController playerController;

    private float nextUseTime = 0f;

    private void Awake()
    {
        playerController =
            GetComponent<PlayerController>();
    }

    private void Update()
    {
        // Se o PlayerController foi desabilitado
        // no final da corrida, não permite disparar.
        if (!playerController.enabled)
            return;

        // Stun bloqueia também o uso de poderes.
        if (playerController.IsStunned)
            return;

        if (!Input.GetKeyDown(powerKey))
            return;

        if (Time.time < nextUseTime)
            return;

        Fire();

        nextUseTime =
            Time.time + cooldown;
    }

    private void Fire()
    {
        if (projectilePrefab == null)
        {
            Debug.LogError(
                $"{name}: Stun Projectile não configurado!"
            );

            return;
        }

        if (powerSpawn == null)
        {
            Debug.LogError(
                $"{name}: PowerSpawn não configurado!"
            );

            return;
        }

        float direction =
            playerController.FacingDirection;

        Vector3 localSpawn =
            powerSpawn.localPosition;

        Vector3 directedLocalSpawn =
            new Vector3(
                Mathf.Abs(localSpawn.x) * direction,
                localSpawn.y,
                localSpawn.z
            );

        Vector3 spawnPosition =
            transform.TransformPoint(
                directedLocalSpawn
            );

        StunProjectile projectile =
            Instantiate(
                projectilePrefab,
                spawnPosition,
                Quaternion.identity
            );

        projectile.Initialize(
            gameObject,
            direction
        );
    }
}