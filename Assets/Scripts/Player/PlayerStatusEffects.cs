using System.Collections;
using UnityEngine;

public class PlayerStatusEffects : MonoBehaviour
{
    private PlayerController controller;

    private Coroutine stunCoroutine;
    private Coroutine slowCoroutine;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
    }

    public void ApplyStun(float duration)
    {
        if (stunCoroutine != null)
        {
            StopCoroutine(stunCoroutine);
        }

        stunCoroutine = StartCoroutine(
            StunRoutine(duration)
        );
    }

    public void ApplySlow(
        float multiplier,
        float duration
    )
    {
        if (slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(
            SlowRoutine(multiplier, duration)
        );
    }

    private IEnumerator StunRoutine(float duration)
    {
        controller.SetStunned(true);

        yield return new WaitForSeconds(duration);

        controller.SetStunned(false);

        stunCoroutine = null;
    }

    private IEnumerator SlowRoutine(
        float multiplier,
        float duration
    )
    {
        controller.SetMovementMultiplier(
            multiplier
        );

        yield return new WaitForSeconds(duration);

        controller.SetMovementMultiplier(1f);

        slowCoroutine = null;
    }
}