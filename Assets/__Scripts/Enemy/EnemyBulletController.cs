using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class EnemyBulletController : MonoBehaviour {

	private Transform bullet;
	public float speed;

	// Use this for initialization
	void Start () {
		bullet = GetComponent<Transform> ();
	}

	void FixedUpdate(){
		bullet.position += Vector3.up * -speed;

		if (bullet.position.y <= -100)
			Destroy (bullet.gameObject);
	}

    IEnumerator Gameover(){
        // wait 3 seconds on the splash screen then load the MainMenu
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("GameOver");
    }
}