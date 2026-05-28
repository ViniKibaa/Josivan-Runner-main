using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public GameObject winPanel;

    public void ShowWinMenu()
    {
        winPanel.SetActive(true);

        Time.timeScale = 0f;
    }

    public void LoadNextLevel()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex + 1
        );
    }

    public void QuitToMenu()
    {
        Time.timeScale = 1f;

        // Reset moedas
        PlayerPrefs.SetInt("SavedCoins", 0);

        // Remove checkpoint salvo
        PlayerPrefs.DeleteKey("HasCheckpoint");

        PlayerPrefs.Save();

        // Volta pro menu principal
        SceneManager.LoadScene(0);
    }
}