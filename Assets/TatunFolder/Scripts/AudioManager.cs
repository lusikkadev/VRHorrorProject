using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // make a static instance of this class
    public static AudioManager instance;


    [Header("Audio Sources")]
    [SerializeField] AudioSource ambientSource;
    [SerializeField] GameObject phone;
    [SerializeField] GameObject neighborApartment;
    [SerializeField] GameObject creeper;

    [Header("Audio Clips")]
    [SerializeField] AudioClip phoneRingClip;
    [SerializeField] AudioClip windAndThunder;
    [SerializeField] AudioClip womanScream;
    [SerializeField] AudioClip thunderSound;
    [SerializeField] AudioClip creeperSound;

    private void Awake()
    {
        // if instance is null, set it to this
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayPhoneRing()
    {
        AudioSource audioSource = phone.GetComponent<AudioSource>();
        audioSource.clip = phoneRingClip;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopPhoneRing()
    {
        AudioSource audioSource = phone.GetComponent<AudioSource>();
        audioSource.Stop();
    }

    public void StartWindAndThunder()
    {
        ambientSource.clip = windAndThunder;
        ambientSource.Play();
        ambientSource.loop = true;
    }

    public void StopWindAndThunder()
    {
        ambientSource.Stop();
    }

    public void PlayWomanScream()
    {
        AudioSource audioSource = neighborApartment.GetComponent<AudioSource>();
        audioSource.PlayOneShot(womanScream);
    }

}
