using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;   // grammar recogniser
using System.IO;
using System.Text;  // for stringbuilder

[RequireComponent(typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
{
    // == private fields ==
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;
    [SerializeField] private float bulletSpeed = 6.0f;
    //[SerializeField] private float firingRate = 2f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private AudioClip shootClip;
    private Coroutine firingCoroutine;
    private AudioSource audioSource;
    private GameObject bulletParent;
    private string spokenWord = "";
    private GrammarRecognizer gr;

    // public fields
    public bool fireMainWeapon;

    private void Start()
    {
        // get the audio source
        audioSource = GetComponent<AudioSource>();

        bulletParent = GameObject.Find("BulletParent");

        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

        // create a new GrammarRecognizer (passing in GameOverControls.xml)
        gr = new GrammarRecognizer(Path.Combine(Application.streamingAssetsPath, "PlayerWeaponControls.xml"), ConfidenceLevel.Low);
        // once the word is recognised, call GR_OnPhraseRecognized
        gr.OnPhraseRecognized += GR_OnPhraseRecognized;
        // start the keyword recognizer
        gr.Start();
    }

    private void GR_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        // create a new StringBuilder
        StringBuilder message = new StringBuilder();
        // read the semantic meanings from the args passed in.
        SemanticMeaning[] meanings = args.semanticMeanings;
        // use foreach to get all the meanings.
        foreach(SemanticMeaning meaning in meanings)
        {
            string keyString = meaning.key.Trim();
            string valueString = meaning.values[0].Trim();
            message.Append("Key: " + keyString + ", Value: " + valueString + " ");
            spokenWord = valueString;
            FireWeapon(spokenWord);
        }
    }

    private void FireWeapon(string spokenWord) 
    {
        // switch on the spokenWord
        switch(spokenWord)
        {
            case "fire":
                Fire();
                break;
            case "automatic":
                StartAutomaticFiring();
                break;
            case "stop":
                StopAutomaticFiring();
                break;
        }    
    }

    // coroutine returns an IEnumerator type
    private void Fire()
    {
        // use a local AudioSource
        audioSource.PlayOneShot(shootClip, shootVolume);

        // create a bullet
        Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
        // give it the same position as the player
        bullet.transform.position = transform.position;

        Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();

        // fire bullet upwards
        rbb.velocity = Vector2.up * bulletSpeed;
    }

    private void StartAutomaticFiring()
    {
        fireMainWeapon = true;
        StartCoroutine(AutomaticFiring());
    }

    private IEnumerator AutomaticFiring()
    {
        // if true, fire weapon
        // if false, stop firing
        while (fireMainWeapon)
        {
            // use a local AudioSource
            audioSource.PlayOneShot(shootClip, shootVolume);

            // create a bullet
            Bullet bullet = Instantiate(bulletPrefab, bulletParent.transform);
            // give it the same position as the player
            bullet.transform.position = transform.position;

            Rigidbody2D rbb = bullet.GetComponent<Rigidbody2D>();

            // fire bullet upwards
            rbb.velocity = Vector2.up * bulletSpeed;

            // sleep for short time
            yield return new WaitForSeconds(.2f);
        }
    }

    private void StopAutomaticFiring()
    {
        fireMainWeapon = false;
    }
}