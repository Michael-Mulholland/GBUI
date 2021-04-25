using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.Linq;

public class GameOver : MonoBehaviour {
    // declare variables
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] GameObject inputText;
    private string playerName;
    private int highScore;
    private int currentScore;
    // KeywordRecognizer object initializer
    private KeywordRecognizer keywordRecognizer;
    // Dictionary containing all defined keywords for the game
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Awake() {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreText.text = highScore.ToString();
        currentScore = PlayerPrefs.GetInt("PlayerScore", currentScore);
        scoreText.text = currentScore.ToString();
    }

    private void Start()
    {
        // get player total score
        SaveData data = SaveSystem.LoadGame();
        if(data != null)
        highScore = data.score;

        // Add words/sentence to dictionary and call the corresponding function */
        keywords.Add("restart", () => {RestartGame();});
        keywords.Add("restart game", () => {RestartGame();});
        keywords.Add("restart the game", () => {RestartGame();});

        keywords.Add("main menu", () => {MainMenu();});
        keywords.Add("go to the main menu", () => {MainMenu();});
        keywords.Add("load the main menu", () => {MainMenu();});

        keywords.Add("quit the game", () => {Quit();});
        
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeyWordRecognizerOnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void KeyWordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
        }
    }

    public void RestartGame()
    {
        // load the game scene
        SceneManager.LoadScene("Level1");
    }

    public void MainMenu()
    {
        // load the Main Menu
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        // quit the application++
        Application.Quit();
    }

    public void SubmitScore()
    {
        // get player name from input box
        playerName = inputText.GetComponent<TextMeshProUGUI>().text;

        // add new score 
        SaveSystem.AddHighScoreEntry(highScore, playerName);
    }
}