using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
    private GameDetails gameDetails;
	[SerializeField] private float enemyHealth = 100;
	[SerializeField] private float enemyDamageTaken;

    private void Awake() 
    {
        // finds the object of type DialogueTrigger       
        gameDetails = GameObject.FindObjectOfType<GameDetails>();
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "PlayerBullet") 
        {
			TakeDamage(enemyDamageTaken);
			Destroy (other.gameObject);

			if(enemyHealth <= 0)
			{
				gameDetails.PlayerScore(100);	
				Destroy (gameObject); 
			}
		} 
	}

    // amount of damage to take away from player
    public void TakeDamage(float amount){
        // take damage away if player is hit
        enemyHealth -= amount;

    }
}