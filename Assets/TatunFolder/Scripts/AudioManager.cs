using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // make a static instance of this class
    public static AudioManager instance;


    [Header("Audio Sources")]
    [SerializeField] AudioSource ambientSource;
    [SerializeField] GameObject phone;
    [SerializeField] GameObject womanScreamSource;
    [SerializeField] GameObject creeper;
    [SerializeField] GameObject door;
    [SerializeField] GameObject shower;

    [Header("Audio Clips")]
    [SerializeField] AudioClip phoneRingClip;
    [SerializeField] AudioClip windAndThunder;
    [SerializeField] AudioClip womanScream;
    [SerializeField] AudioClip thunderSound;
    [SerializeField] AudioClip creeperSound;
    [SerializeField] AudioClip doorBellSound;
    [SerializeField] AudioClip showerSound;
    [SerializeField] AudioClip showerStopSound;
    [SerializeField] AudioClip maggotSound;
    [SerializeField] AudioClip creepySFX1;
    [SerializeField] AudioClip creepySFX2;
    [SerializeField] AudioClip creepySFX3;

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

    public void LoopMaggotSound()
    {
        AudioSource audioSource = creeper.GetComponent<AudioSource>();
        audioSource.clip = maggotSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopMaggotSound()
    {
        AudioSource audioSource = creeper.GetComponent<AudioSource>();
        audioSource.Stop();
    }

    public void PlayCreepySFX(int index)
    {
        AudioSource audioSource = phone.GetComponent<AudioSource>();
        switch (index)
        {
            case 1:
                audioSource.PlayOneShot(creepySFX1);
                break;
            case 2:
                audioSource.PlayOneShot(creepySFX2);
                break;
            default:
                Debug.LogWarning("Invalid creepy SFX index: " + index);
                break;
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
        AudioSource audioSource = womanScreamSource.GetComponent<AudioSource>();
        audioSource.PlayOneShot(womanScream);
    }

    public void PlayLightningSound()
    {
        AudioSource audioSource = ambientSource;
        audioSource.PlayOneShot(thunderSound);
    }

    public void LoopCreeperSound()
    {
        AudioSource audioSource = creeper.GetComponent<AudioSource>();
        // creeper breathing loop start
        audioSource.clip = creeperSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopCreeperSound()
    {
        AudioSource audioSource = creeper.GetComponent<AudioSource>();
        audioSource.Stop();
    }

    public void PlayDoorBellSound()
    {
        AudioSource audioSource = door.GetComponent<AudioSource>();
        audioSource.PlayOneShot(doorBellSound);
    }

    public void LoopShowerSound()
    {
        AudioSource audioSource = shower.GetComponent<AudioSource>();
        audioSource.clip = showerSound;
        audioSource.Play();
        audioSource.loop = true;
    }

    public void StopShowerSound()
    {
        AudioSource audioSource = shower.GetComponent<AudioSource>();
        audioSource.Stop();
        audioSource.PlayOneShot(showerStopSound);
    }

}
