using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class GameDetails : MonoBehaviour
{
    // private fields
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;
    [SerializeField] private AudioClip playerExplosion;
    [SerializeField] private Text scoreText;
    private static GameDetails instance;
    private AudioSource audioSource;
    private static int playerLives;
    private float loadDelay = 1.0f;
    private static int highScore;
    //private static int score = 0;

    // public fields
    [SerializeField] Text highScoreText;
    [SerializeField] Text playerLivesText;
    public static int currentScore;
    public Image[] shipImageLifes;
    public int numberOfLives;
    public Sprite emptyShip;
    public Sprite ship;

    private void Awake()
    {
        // set player lives and the current score
        playerLives = 3;
        currentScore = 0;

        // dont destroy when loading a new scene
        DontDestroyOnLoad(gameObject); 
                
        if(instance == null)
        {    
            // In first scene, make us the singleton.
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if(instance != this)
        {
            // On reload, singleton already set, so destroy duplicate. 
            Destroy(gameObject);  
            currentScore = PlayerPrefs.GetInt("PlayerScore", currentScore);
            playerLives = PlayerPrefs.GetInt("PlayerLives", playerLives);
        }
    }
    private void Start() 
    {
        // get the audio source
        audioSource = GetComponent<AudioSource>();    

        // PlayerPref to save the players highscore and allow the user to see it in the main menu
        if (PlayerPrefs.HasKey("HighScore"))
        {
            highScore = PlayerPrefs.GetInt("HighScore", highScore);
        }
    }

    private void Update() 
    {
        highScoreText.text = highScore.ToString();    

        // Create a temporary reference to the current scene.
        Scene currentScene = SceneManager.GetActiveScene();
 
        // Retrieve the name of this scene.
        string sceneName = currentScene.name;

        // destroy the player object if one of the following scenes are loaded
        if (sceneName == "MainMenu" || sceneName == "GameOver" || sceneName == "PlayerWins")
        {
            Destroy(gameObject);
        }
    }

    public void PlayerScore(int score)
    {
        // add to the player score
        currentScore += score;

        // display player score on the screen
        scoreText.text = currentScore.ToString();
        
        // set the score
        PlayerPrefs.SetInt("PlayerScore", currentScore);

        // call to see if there is a new high score  
        HighestScore();
    }

    public void HighestScore()
    {
        // check to see if the highscore is beat
        // if it is, save the new highscore
        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }
    }

    public void LooseLife()
    {
        // take one live away
        playerLives--;
        // save the players lives left
        PlayerPrefs.SetInt("PlayerLives", playerLives);

        if (playerLives > numberOfLives)
        {
            playerLives = numberOfLives;
        }

        // used for the players lives
        for (int i = 0; i < shipImageLifes.Length; i++)
        {
            if (i < playerLives)
            {
                shipImageLifes[i].sprite = ship;
            }
            else
            {
                shipImageLifes[i].sprite = emptyShip;
            }

            if (i < numberOfLives)
            {
                shipImageLifes[i].enabled = true;
            }
            else
            {
                shipImageLifes[i].enabled = false;
            }
        }
        if(playerLives == 0)
        {
            // use a local AudioSource
            audioSource.PlayOneShot(playerExplosion, shootVolume);
            
            GameOver();
        }      
    }

    void GameOver()
    {
        Invoke("LoadGameOverScene", loadDelay);
    }
    private void LoadGameOverScene()
    {
        SceneManager.LoadScene("GameOver");
    }
}