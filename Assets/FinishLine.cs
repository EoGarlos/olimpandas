using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    [Header("Interface")]
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private TMP_Text victoryText;
    [SerializeField] private TMP_Text medalText;
    [SerializeField] private TMP_Text nextButtonText;

    private bool raceFinished = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (raceFinished)
            return;

        if (!other.CompareTag("Player"))
            return;

        PlayerController winner =
            other.GetComponent<PlayerController>();

        if (winner == null)
            return;

        raceFinished = true;

        int playerId = winner.PlayerId;

        bool matchFinished =
            GameManager.Instance.AddMedal(playerId);

        // Congela todos os jogadores
        FreezeAllPlayers();

        if (matchFinished)
        {
            victoryText.text =
                $"PLAYER {playerId}\nCAMPEÃO!";

            nextButtonText.text =
                "JOGAR NOVAMENTE";
        }
        else
        {
            victoryText.text =
                $"PLAYER {playerId}\nVENCEU!";

            nextButtonText.text =
                "PRÓXIMA PROVA";
        }

        medalText.text =
            $"MEDALHAS\n{GameManager.Instance.GetScore()}";

        victoryPanel.SetActive(true);

        Debug.Log(
            $"PLAYER {playerId} VENCEU A PROVA!"
        );
    }

    private void FreezeAllPlayers()
    {
        PlayerController[] players =
            FindObjectsByType<PlayerController>(
                FindObjectsSortMode.None
            );

        foreach (PlayerController player in players)
        {
            Rigidbody2D rb =
                player.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.angularVelocity = 0f;
                rb.simulated = false;
            }

            player.enabled = false;
        }
    }
}