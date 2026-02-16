using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
public class LightningEffect : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject LightsParent;
    [SerializeField] float decaySeconds = 1.0f;
    [SerializeField] float normalIntensity = 1.0f;
    [SerializeField] float eventIntensity = 2.0f;
    [SerializeField] bool useNoise = true;
    [SerializeField] float noiseScale = 5.0f;

    [SerializeField] Material skybox;
    float secondsSinceEvent = Mathf.Infinity;
    void Start()
    {
        animator = GetComponent<Animator>();
        skybox = RenderSettings.skybox;
    }
    public void TriggerNoise() {
        secondsSinceEvent = 0;
    }
    public void playLightningEffect() {
        animator.Play("Lightning");
        TriggerNoise();
    }

    public IEnumerator PlayLightningAfterBlackOut()
    {
        if (!LightsParent.activeSelf)
        {
            TurnOnLights();
        }
        animator.Play("Lightning");
        TriggerNoise();
        yield return new WaitForSeconds(3f);
        TurnOffLights();
    }

    public void TurnOffLights() {
        LightsParent.SetActive(false);
    }
    public void TurnOnLights() {
        LightsParent.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.enterKey.wasPressedThisFrame) 
            playLightningEffect();
        
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) 
            TurnOnLights();
        
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) 
            TurnOffLights();
        
        secondsSinceEvent += Time.deltaTime;
        float t = Mathf.Clamp01(1 - secondsSinceEvent / decaySeconds);
        if (useNoise)
            t *= Mathf.PerlinNoise1D(Time.time * noiseScale);
        var intensity = Mathf.Lerp(normalIntensity, eventIntensity, t);
        skybox.SetFloat("_Exposure", intensity);

    }
}
