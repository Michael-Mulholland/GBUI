using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private GameDetails gameDetails;
    private BodySourceView bodySourceView;
    public bool playerDied = false;

    private void Awake() 
    {
        // finds the object of type DialogueTrigger       
        gameDetails = GameObject.FindObjectOfType<GameDetails>();
        bodySourceView = GameObject.FindObjectOfType<BodySourceView>();
    }
    private void Update() 
    {
        playerDied = false;    
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Enemy") 
        {
            gameDetails.LooseLife();
		} 
	}
}