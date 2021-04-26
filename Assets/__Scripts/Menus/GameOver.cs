using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder

public class GameOver : MonoBehaviour 
{
    // declare variables
    [SerializeField] private Text highScoreText;
    [SerializeField] private Text scoreText;
    [SerializeField] GameObject inputText;
    private string spokenWord = "";
    private GrammarRecognizer gr;
    private string playerName;
    private int currentScore;
    private int highScore;

    private void Awake() {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreText.text = highScore.ToString();
        currentScore = PlayerPrefs.GetInt("PlayerScore", currentScore);
        scoreText.text = currentScore.ToString();
    }

    private void Start()
    {
        // create a new GrammarRecognizer (passing in GameOverControls.xml)
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "GameOverControls.xml"), ConfidenceLevel.Low);
        // once the word is recognised, call GR_OnPhraseRecognized
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        // start the keyword recognizer
        gr.Start();
    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // create a new StringBuilder
        StringBuilder message = new StringBuilder();
        // read the semantic meanings from the args passed in.
        SemanticMeaning[] meanings = args.semanticMeanings;
        // use foreach to get all the meanings.
        foreach(SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Value: " + valueString + " ");
            spokenWord = valueString;
        }
    }

    private void Update() 
    {
        // switch on the spokenWord
        switch(spokenWord)
        {
            case "restart":
                RestartGame();
                break;
            case "menu":
                MainMenu();
                break;
            case "quit":
                Quit();
                break;
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