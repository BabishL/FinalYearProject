using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MainMenuControl : MonoBehaviour
{
    public TMP_Text scoreText;

    void Start()
    {
        int finalscore = MasterInfo.coinCount;

        if (scoreText != null)
        {
            scoreText.text = "Score: " + finalscore.ToString();
        }
        else
        {
            Debug.LogError("scoretext is not assigned");
        }
    }
    public void StartGame()
    {
        MasterInfo.coinCount = 0;
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
