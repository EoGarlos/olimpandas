using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TMP_Text medalText;

    private bool raceFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (raceFinished)
            return;

        if (!other.CompareTag("Player"))
            return;

        raceFinished = true;

        // Por enquanto este personagem é o Player 1.
        int playerId = 1;

        bool matchFinished = GameManager.Instance.AddMedal(playerId);

        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();

        if (rb != null)
            rb.linearVelocity = Vector2.zero;

        PlayerController controller =
            other.GetComponent<PlayerController>();

        if (controller != null)
            controller.enabled = false;

        if (matchFinished)
        {
            victoryText.text = "CAMPEÃO!";
        }
        else
        {
            victoryText.text = "VITÓRIA!";
        }

        medalText.text =
            $"MEDALHAS\n{GameManager.Instance.GetScore()}";

        victoryPanel.SetActive(true);

        Debug.Log(
            matchFinished
                ? "PLAYER 1 VENCEU A PARTIDA!"
                : "PLAYER 1 VENCEU A PROVA!"
        );
    }
}