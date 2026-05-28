using UnityEngine;

public class TreasureBox : MonoBehaviour
{
    private bool isOpened = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !isOpened)
        {
            isOpened = true;

            OpenTreasure();
        }
    }

    private void OpenTreasure()
    {
        if (SoundManager.instance != null &&
            SoundManager.instance.treasureSound != null)
        {
            SoundManager.instance.PlaySFX(
                SoundManager.instance.treasureSound
            );
        }

        int totalCoinsOnWin = 0;

        player_movement player =
            FindFirstObjectByType<player_movement>();

        if (player != null)
        {
            totalCoinsOnWin =
                PlayerPrefs.GetInt("SavedCoins", 0);
        }

        if (UIManager.instance != null)
        {
            UIManager.instance.TriggerWin(totalCoinsOnWin);
        }
    }
}