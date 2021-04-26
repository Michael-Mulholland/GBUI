using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // private fields
    private BodySourceView bodySourceView;
    private GameDetails gameDetails;

    // public fields
    public bool playerDied = false;

    private void Awake() 
    {
        // finds the object of type GameDetails and BodySourceView
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