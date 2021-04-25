using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class EnemyController : MonoBehaviour {

	private Transform enemyHolder;
	public float speed;
	public GameObject shot;
	public float fireRate = 0.997f;

    [SerializeField] private AudioClip shootClip;
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
		// get the audio source
        audioSource = GetComponent<AudioSource>();
		
		InvokeRepeating ("MoveEnemy", 0.1f, 0.3f);
		enemyHolder = GetComponent<Transform>();
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

			//EnemyBulletController called too?
			if (Random.value > fireRate) {
				// use a local AudioSource
            	audioSource.PlayOneShot(shootClip, shootVolume);

				Instantiate (shot, enemy.position, enemy.rotation);
			}


			if (enemy.position.y <= -7) {
				SceneManager.LoadScene("GameOver");
			}
		}

		if (enemyHolder.childCount == 1) {
			CancelInvoke ();
			InvokeRepeating ("MoveEnemy", 0.1f, 0.25f);
		}

		if (enemyHolder.childCount == 0) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
		}
	}
	

}