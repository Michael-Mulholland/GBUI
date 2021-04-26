using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Enemy : MonoBehaviour
{
	// private fields
	[SerializeField] private float enemyDamageTaken = 0;
	[SerializeField] private float enemyHealth = 100;
    private GameDetails gameDetails;

    private void Awake() 
    {
        // finds the object of type GameDetails       
        gameDetails = GameObject.FindObjectOfType<GameDetails>();
    }
	void OnTriggerEnter2D(Collider2D other)
	{
		// If the players bullet hits the enemy, the enemy takes damage.
		// The bullet is destroyed
		// If the enemies health is less than 0, destroy the enemy
		if (other.tag == "PlayerBullet") 
        {
			TakeDamage(enemyDamageTaken);
			Destroy (other.gameObject);

			if(enemyHealth <= 0)
			{
				// Add 100 to the players score
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