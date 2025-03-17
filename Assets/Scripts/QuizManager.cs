using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class QuizManager : MonoBehaviour
{
    [System.Serializable]
    public class QuizQuestion
    {
        public string question;
        public string[] options;
        public int correctAnswerIndex;
    }

    public List<QuizQuestion> questions; // List of quiz questions
    public TextMeshProUGUI questionText;
    public Button[] answerButtons;
    public GameObject quizPanel;
    public float quizInterval = 10f; // Time before quiz appears
    private int currentQuestionIndex;

    void Start()
    {
        quizPanel.SetActive(false); // Hide the quiz at the start
        StartCoroutine(StartQuizTimer());
    }

    IEnumerator StartQuizTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(quizInterval);
            ShowQuiz();
        }
    }

    void ShowQuiz()
    {
        Time.timeScale = 0; // Pause the game
        quizPanel.SetActive(true); // Show the quiz
        LoadNewQuestion();
    }

    void LoadNewQuestion()
    {
        currentQuestionIndex = Random.Range(0, questions.Count);
        questionText.text = questions[currentQuestionIndex].question;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = questions[currentQuestionIndex].options[i];
            Color buttonColor;
            ColorUtility.TryParseHtmlString("#5d2190", out buttonColor);
            answerButtons[i].image.color = buttonColor;
            int index = i; // Capture the current index for the listener
            answerButtons[i].onClick.RemoveAllListeners(); // Clear previous listeners
            answerButtons[i].onClick.AddListener(() => CheckAnswer(index));
        }
    }

    void CheckAnswer(int selectedIndex)
    {
        Button selectedButton = answerButtons[selectedIndex];

        if (selectedIndex == questions[currentQuestionIndex].correctAnswerIndex)
        {
            selectedButton.image.color = Color.green;
            MasterInfo.coinCount += 5;
            StartCoroutine(ResumeAfterDelay());
        }
        else
        {
            selectedButton.image.color = Color.red; 
            StartCoroutine(ReturnToMainMenu());

        }
    }
    IEnumerator ResumeAfterDelay()
    {
        Debug.Log("ResumeAfterDelay coroutine started...");
        yield return new WaitForSecondsRealtime(1f); // Wait 1 second
        Debug.Log("Resuming game now...");
        ResumeGame();
    }
    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSecondsRealtime(1f); // Wait 1 second
        SceneManager.LoadScene(2); 
    }

    void ResumeGame()
    {
        quizPanel.SetActive(false);
        Time.timeScale = 1; // Resume the game
    }

}
