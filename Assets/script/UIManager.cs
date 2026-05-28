using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [Header("UI Panels")]
    [SerializeField] private GameObject inGameUI;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject winScreenPanel;

    [Header("Win Screen Text")]
    [SerializeField] private TextMeshProUGUI finalScoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        Time.timeScale = 1f;

        if (inGameUI != null)
            inGameUI.SetActive(true);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

        if (winScreenPanel != null)
            winScreenPanel.SetActive(false);
    }

    // MAIN MENU / GENERAL BUTTONS
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
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

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;

        // Reset moedas
        PlayerPrefs.SetInt("SavedCoins", 0);

        // Remove checkpoint salvo
        PlayerPrefs.DeleteKey("HasCheckpoint");

        PlayerPrefs.Save();

        SceneManager.LoadScene(0);
    }

    // GAME OVER SCREEN
    public void TriggerGameOver()
    {
        Time.timeScale = 0f;

        if (inGameUI != null)
            inGameUI.SetActive(false);

        if (gameOverPanel != null)
            gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;

        // Reset moedas
        PlayerPrefs.SetInt("SavedCoins", 0);

        // Remove checkpoint salvo
        PlayerPrefs.DeleteKey("HasCheckpoint");

        PlayerPrefs.Save();

        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }

    // WIN SCREEN
    public void TriggerWin(int finalCoins)
    {
        Time.timeScale = 0f;

        if (inGameUI != null)
            inGameUI.SetActive(false);

        if (winScreenPanel != null)
            winScreenPanel.SetActive(true);

        if (finalScoreText != null)
        {
            finalScoreText.text =
                "Congratulations!\nFinal Score: " +
                finalCoins.ToString() +
                " Coins";
        }
    }

    // NEXT LEVEL BUTTON
    public void NextLevel()
    {
        Time.timeScale = 1f;

        int nextSceneIndex =
            SceneManager.GetActiveScene().buildIndex + 1;

        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            // Reset moedas ao terminar o jogo
            PlayerPrefs.SetInt("SavedCoins", 0);

            PlayerPrefs.DeleteKey("HasCheckpoint");

            PlayerPrefs.Save();

            SceneManager.LoadScene(0);
        }
    }
}