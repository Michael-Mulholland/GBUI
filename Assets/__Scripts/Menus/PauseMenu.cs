using UnityEngine;
using UnityEngine.Windows.Speech;   // grammar recogniser
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Linq;

public class PauseMenu : MonoBehaviour
{
    // KeywordRecognizer object initializer
    private KeywordRecognizer keywordRecognizer;
    // Dictionary containing all defined keywords for the game
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
    public static bool GamePaused = false;

    public GameObject pauseMenuUI;
    public AudioMixer audioMixer;


    private void Awake()
    {
        Resume();
    }

    private void Start()
    {
        // Add words/sentence to dictionary and call the corresponding function */
        keywords.Add("pause", () => {Pause();});
        keywords.Add("pause game", () => {Pause();});
        keywords.Add("pause the game", () => {Pause();});

        keywords.Add("resume", () => {Resume();});
        keywords.Add("resume game", () => {Resume();});
        keywords.Add("resume the game", () => {Resume();});
        // keywords.Add("start", () => {Resume();});
        // keywords.Add("start game", () => {Resume();});
        // keywords.Add("start the game", () => {Resume();});

        keywords.Add("go to main menu", () => {LoadMenu();});
        keywords.Add("load menu", () => {LoadMenu();});

        keywords.Add("volume on", () => {VolumeOnOff(true);});

        keywords.Add("volume off", () => {VolumeOnOff(false);});

        keywords.Add("quit game", () => {QuitGame();});
        
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

    public void VolumeOnOff(bool setMusic)
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