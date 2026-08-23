using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class SlowPowerController : MonoBehaviour
{
    [Header("Controle")]
    [SerializeField] private KeyCode powerKey = KeyCode.Q;

    [Header("Disparo")]
    [SerializeField] private SlowProjectile projectilePrefab;
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
        if (!playerController.enabled)
            return;

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
                $"{name}: Slow Projectile não configurado!"
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

        SlowProjectile projectile =
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