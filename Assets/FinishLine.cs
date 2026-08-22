using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private bool raceFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (raceFinished)
            return;

        if (other.CompareTag("Player"))
        {
            raceFinished = true;

            Debug.Log("PLAYER VENCEU!");

            // Para o jogador imediatamente
            Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
            }

            // Desativa os controles
            PlayerController controller = other.GetComponent<PlayerController>();

            if (controller != null)
            {
                controller.enabled = false;
            }
        }
    }
}