using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine.Windows.Speech;   // grammar recogniser
using System.Collections.Generic;
using System.IO;
using System.Text;  // for stringbuilder

public class MainMenu : MonoBehaviour
{
    // private fields
    [SerializeField] private Text highScoreText;
    private string spokenWord = "";
    private GrammarRecognizer gr;
    private int backInteger;
    private int highScore;

    // public fields
    [SerializeField] GameObject gameControlsPanel;
    [SerializeField] GameObject highscorePanel;
    [SerializeField] GameObject mainMenuPanel;
    [SerializeField] GameObject optionsPanel;
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public AudioSource playSound;
    public AudioMixer audioMixer;

    private void Awake() {
        highScore = PlayerPrefs.GetInt("HighScore", highScore);
        highScoreText.text = highScore.ToString();
    }

    void Start()
    {
        // create a new GrammarRecognizer (passing in MainMenuController.xml)
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "MainMenuController.xml"), ConfidenceLevel.Low);
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
            SpokenWord(spokenWord);
        }
    }

    private void SpokenWord(string word) 
    {
        // switch on the spokenWord
        switch(word)
        {
            case "play":
                Play();
                break;
            case "options":
                Options();
                break;
            case "quit":
                Quit();
                break;
            case "controls":
                Controls();
                break;
            case "highscore":
                Highscore();
                break;
            case "volumeon":
                Volume(true);
                break;
            case "volumeoff":
                Volume(false);
                break;
            case "back":
                Back();
                break;
        }
    }

    private void Back()
    {
        // used to naviagte to the right panel
        // 0 = navigate to the main menu panel
        // 1 = navigate to the options panel
        // 2 = navigate to the high score panel
        if(backInteger == 0)
        {
            optionsPanel.gameObject.SetActive(false);
            mainMenuPanel.gameObject.SetActive(true);
        }
        else if(backInteger == 1)
        {
            gameControlsPanel.gameObject.SetActive(false);
            optionsPanel.gameObject.SetActive(true);
            backInteger = 0;
        }
        else if(backInteger == 2)
        {
            highscorePanel.gameObject.SetActive(false);
            optionsPanel.gameObject.SetActive(false);
            mainMenuPanel.gameObject.SetActive(true);
            backInteger = 0;
        }
    }

    private void Reset()
    {
        PlayerPrefs.DeleteKey("HighScore");
        highScore = 0;
        PlayerPrefs.SetInt("HighScore",highScore);
        highScoreText.text = highScore.ToString();
    }

    private void Highscore()
    {
        backInteger = 2;
        // activate the high score panal and deactivate the options panel
        mainMenuPanel.gameObject.SetActive(false);
        highscorePanel.gameObject.SetActive(true);
    }

    private void Volume(bool setMusic)
    {
        // turn the music on and off
        if(setMusic == false)
        {
            setMusic = true;
            AudioListener.pause = true;
        }
        else
        {
            setMusic = false;
            AudioListener.pause = false;
        }
    }

    private void Controls()
    {
        // used to naviagte to the right panel
        backInteger = 1;
        // activate the game controls panel and deactivate the options panel
        optionsPanel.gameObject.SetActive(false);
        gameControlsPanel.gameObject.SetActive(true);
    }

    public void Quit()
    {
        Debug.Log("Quitting the game.....");
        Application.Quit();
    }

    private void Options()
    {
        // used to naviagte to the right panel
        backInteger = 0;
        // activate the options panal and deactivate the main menu panel
        mainMenuPanel.gameObject.SetActive(false);
        optionsPanel.gameObject.SetActive(true);
    }

    public void Play()
    {
        // loads level 1
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PlaySound()
    {
        playSound.Play();
    }   

    public void GameVolume(float volume)
    {
        // allows me to turn the volume up and down
        // within the main menu and pause settings
        audioMixer.SetFloat("volume", volume);
    }
}