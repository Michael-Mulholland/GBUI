using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EnemyBulletController : MonoBehaviour 
{
	// private fields
	private Transform bullet;

	// public fields
	public float speed;

	void Start () {
		bullet = GetComponent<Transform> ();
	}

	void FixedUpdate(){
		bullet.position += Vector3.up * -speed;

		if (bullet.position.y <= -100)
			Destroy (bullet.gameObject);
	}

    IEnumerator Gameover(){
        // wait 3 seconds then load the GameOver scene
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
}