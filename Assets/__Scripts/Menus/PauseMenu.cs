using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.IO;
using System.Text;  // for stringbuilder
public class PauseMenu : MonoBehaviour
{
    // private fields
    // Speech Recognition Specification Grammar (SRSG) XML Grammar
    // It looks for phrases
    private GrammarRecognizer gr;
    private string spokenWord = "";

    // public fields
    public static bool GamePaused = false;
    public GameObject pauseMenuUI;
    public AudioMixer audioMixer;

    private void Awake()
    {
        Resume();
    }

    private void Start()
    {
        // create a new GrammarRecognizer (passing in GameOverControls.xml)
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "PauseControls.xml"), ConfidenceLevel.Low);
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
            FireWeapon(spokenWord);
        }
    }

    private void FireWeapon(string spokenWord) 
    {
        // switch on the spokenWord
        switch(spokenWord)
        {
            case "pause":
                Pause();
                break;
            case "resume":
                Resume();
                break;
            case "menu":
                LoadMenu();
                break;
            case "volume on":
                Volume(true);
                break;
            case "volume off":
                Volume(false);
                break;
            case "quit":
                QuitGame();
                break;
        }    
    }

    private void Update() 
    {
        // if Esc key is pressed, pause game
        if(Input.GetKeyDown(KeyCode.Escape)){

            // if GamePaused is true, then resume game else pause game
            if(GamePaused){
                Resume();
            } else {
                Pause();
            }
        }
    }

    // Resume game
    public void Resume()
    {
        // disable game object - resume
        pauseMenuUI.SetActive(false);
        // scale in which time is passing - 1f means time (the game) will resume as normal
        Time.timeScale = 1f;
        // set to false
        GamePaused = false;
    }

    // Pause game
    private void Pause()
    {
        // enable game object - pause
        pauseMenuUI.SetActive(true);
        // scale in which time is passing - 0f means time (the game) will completely stop
        Time.timeScale = 0f;
        // set to true
        GamePaused = true;
    }

    // loads the menu - selected in the pause menu
    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void GameVolume(float volume)
    {
        // allows me to turn the volume up and down
        // within the main menu and pause settings
        audioMixer.SetFloat("volume", volume);
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

    // quits the application
    public void QuitGame()
    {
        Debug.Log("Quitting the game.....");
        Application.Quit();
    }
}