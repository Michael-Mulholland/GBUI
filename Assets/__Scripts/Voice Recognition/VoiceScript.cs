// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using UnityEngine.Windows.Speech;
// using System.Linq;
// using UnityEngine.SceneManagement;
// using UnityEngine.UI;

// public class VoiceScript : MonoBehaviour
// {   
//     // private fields
//     // KeywordRecognizer object initializer
//     KeywordRecognizer keywordRecognizer;
//     // Dictionary containing all defined keywords for the game
//     Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();
//     [SerializeField] GameObject mainMenuPanel;
//     [SerializeField] GameObject optionsPanel;
//     [SerializeField] GameObject highscorePanel;
//     [SerializeField] GameObject gameControlsPanel;
//     [SerializeField] private Text highScoreText;
//     private int backInteger;
//     private int highScore;

//     void Start()
//     {
//         // Add words/sentence to dictionary and call the corresponding function */
//         keywords.Add("play", () => {Play();});
//         keywords.Add("play the game", () => {Play();});
//         keywords.Add("start game", () => {Play();});
//         keywords.Add("start the game", () => {Play();});

//         keywords.Add("options", () => {Options();});
//         keywords.Add("game options", () => {Options();});
//         keywords.Add("go to the game options", () => {Options();});

//         keywords.Add("quit", () => {Quit();});
//         keywords.Add("quit game", () => {Quit();});
//         keywords.Add("quit the game", () => {Quit();});

//         keywords.Add("controls", () => {Controls();});
//         keywords.Add("game controls", () => {Controls();});
//         keywords.Add("go to the game controls", () => {Controls();});

//         keywords.Add("volumeon", () => {Volume(true);});
//         keywords.Add("turn volume on", () => {Volume(true);});

//         keywords.Add("volumeoff", () => {Volume(false);});
//         keywords.Add("turn volume off", () => {Volume(false);});

//         keywords.Add("highscore", () => {Highscore();});
//         keywords.Add("games highscore", () => {Highscore();});
//         keywords.Add("show highscore", () => {Highscore();});

//         keywords.Add("reset", () => {Reset();});
//         keywords.Add("reset highscore", () => {Reset();});
//         keywords.Add("clear highscore", () => {Reset();});

//         keywords.Add("back", () => {Back();});
//         keywords.Add("back to main menu", () => {Back();});
//         keywords.Add("go back", () => {Back();});
//         keywords.Add("back to the main menu", () => {Back();});
        
//         keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
//         keywordRecognizer.OnPhraseRecognized += KeyWordRecognizerOnPhraseRecognized;
//         keywordRecognizer.Start();
//     }

//     /* If keyword is in the list of recognized words */
//     void KeyWordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
//     {
//         System.Action keywordAction;

//         if (keywords.TryGetValue(args.text, out keywordAction))
//         {
//             keywordAction.Invoke();
//         }
//     }
    
//     private void Back()
//     {
//         // used to naviagte to the right panel
//         // 0 = navigate to the main menu panel
//         // 1 = navigate to the options panel
//         // 2 = navigate to the high score panel
//         if(backInteger == 0)
//         {
//             optionsPanel.gameObject.SetActive(false);
//             mainMenuPanel.gameObject.SetActive(true);
//         }
//         else if(backInteger == 1)
//         {
//             gameControlsPanel.gameObject.SetActive(false);
//             optionsPanel.gameObject.SetActive(true);
//             backInteger = 0;
//         }
//         else if(backInteger == 2)
//         {
//             highscorePanel.gameObject.SetActive(false);
//             optionsPanel.gameObject.SetActive(false);
//             mainMenuPanel.gameObject.SetActive(true);
//             backInteger = 0;
//         }
//     }

//     private void Reset()
//     {
//         PlayerPrefs.DeleteKey("HighScore");
//         highScore = 0;
//         PlayerPrefs.SetInt("HighScore",highScore);
//         highScoreText.text = highScore.ToString();
//     }

//     private void Highscore()
//     {
//         backInteger = 2;
//         // activate the high score panal and deactivate the options panel
//         mainMenuPanel.gameObject.SetActive(false);
//         highscorePanel.gameObject.SetActive(true);
//     }

//     private void Volume(bool setMusic)
//     {
//         // turn the music on and off
//         if(setMusic == false)
//         {
//             setMusic = true;
//             AudioListener.pause = true;
//         }
//         else
//         {
//             setMusic = false;
//             AudioListener.pause = false;
//         }
//     }

//     private void Controls()
//     {
//         // used to naviagte to the right panel
//         backInteger = 1;
//         // activate the game controls panel and deactivate the options panel
//         optionsPanel.gameObject.SetActive(false);
//         gameControlsPanel.gameObject.SetActive(true);
//     }

//     private void Quit()
//     {
//         Debug.Log("Quitting the game.....");
//         Application.Quit();
//     }

//     private void Options()
//     {
//         // used to naviagte to the right panel
//         backInteger = 0;
//         // activate the options panal and deactivate the main menu panel
//         mainMenuPanel.gameObject.SetActive(false);
//         optionsPanel.gameObject.SetActive(true);
//     }

//     private void Play()
//     {
//         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
//     }
// }