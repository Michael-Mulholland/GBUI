using UnityEngine;
using System.Collections;
using UnityEngine.Windows.Speech;   // grammar recogniser
using System.Collections.Generic;
using System.Linq;

[RequireComponent(typeof(AudioSource))]
public class PlayerWeapon : MonoBehaviour
{
    // public fields
    public bool fireMainWeapon;

    // == private fields ==
    [SerializeField][Range(0f, 1.0f)] private float shootVolume = 0.5f;
    [SerializeField] private float bulletSpeed = 6.0f;
    [SerializeField] private float firingRate = 2f;
    [SerializeField] private Bullet bulletPrefab;
    [SerializeField] private AudioClip shootClip;
    private Coroutine firingCoroutine;
    private AudioSource audioSource;
    private GameObject bulletParent;

    // KeywordRecognizer object initializer
    private KeywordRecognizer keywordRecognizer;
    // Dictionary containing all defined keywords for the game
    Dictionary<string, System.Action> keywords = new Dictionary<string, System.Action>();

    private void Start()
    {
        // get the audio source
        audioSource = GetComponent<AudioSource>();

        bulletParent = GameObject.Find("BulletParent");

        if (!bulletParent)
        {
            bulletParent = new GameObject("BulletParent");
        }

        // Add words/sentence to dictionary and call the corresponding function */
        keywords.Add("fire", () => {Fire();});
        keywords.Add("fire weapon", () => {Fire();});
        keywords.Add("shoot", () => {Fire();});

        keywords.Add("automatic", () => {StartAutomaticFiring();});
        keywords.Add("automatic fire", () => {StartAutomaticFiring();});
        keywords.Add("keep firing", () => {StartAutomaticFiring();});
        keywords.Add("keep shooting", () => {StartAutomaticFiring();});

        keywords.Add("stop", () => {StopAutomaticFiring();});
        keywords.Add("stop fire", () => {StopAutomaticFiring();});
        keywords.Add("stop firing", () => {StopAutomaticFiring();});
        keywords.Add("stop shooting", () => {StopAutomaticFiring();});
        
        keywordRecognizer = new KeywordRecognizer(keywords.Keys.ToArray());
        keywordRecognizer.OnPhraseRecognized += KeyWordRecognizerOnPhraseRecognized;
        keywordRecognizer.Start();
    }

    void KeyWordRecognizerOnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        System.Action keywordAction;

        if (keywords.TryGetValue(args.text, out keywordAction))
        {
            keywordAction.Invoke();
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