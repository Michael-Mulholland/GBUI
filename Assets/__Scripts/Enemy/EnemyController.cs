using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour 
{
	// private fields
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;
    [SerializeField] private AudioClip shootClip;
	private float enemyMoveRepeatTime = 0.3f;
	private float enemyMoveTime = 0.1f;
    private AudioSource audioSource;
	private Transform enemyHolder;

	// public fields
	public float fireRate = 0.997f;
	public GameObject shot;
	public float speed;


	void Start () {
		// get the audio source
        audioSource = GetComponent<AudioSource>();
		// get Transform		
		enemyHolder = GetComponent<Transform>();
		// move the enemy 
		InvokeRepeating ("MoveEnemy", enemyMoveTime, enemyMoveRepeatTime);
	}

	void MoveEnemy()
	{
		enemyHolder.position += Vector3.right * speed;

		// for each eneny in the holder
		foreach (Transform enemy in enemyHolder) {
			if (enemy.position.x < -7 || enemy.position.x > 7) {
				speed = -speed;
				enemyHolder.position += Vector3.down * 0.5f;
				return;
			}

			if (Random.value > fireRate) {
            	audioSource.PlayOneShot(shootClip, shootVolume);

				Instantiate (shot, enemy.position, enemy.rotation);
			}

			if (enemy.position.y <= -7) {
				SceneManager.LoadScene("GameOver");
			}
		}

		if (enemyHolder.childCount == 1) {
			CancelInvoke ();
			InvokeRepeating ("MoveEnemy", enemyMoveTime, enemyMoveRepeatTime);
		}

		if (enemyHolder.childCount == 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
}