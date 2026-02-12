using UnityEngine;
using UnityEngine.InputSystem;

public class SkyboxExposure : MonoBehaviour {
    [SerializeField] float decaySeconds = 1.0f;
    [SerializeField] float normalIntensity = 1.0f;
    [SerializeField] float eventIntensity = 2.0f;
    [SerializeField] bool useNoise = true;
    [SerializeField] float noiseScale = 5.0f;

    float secondsSinceEvent = Mathf.Infinity;
    Material skybox;

    public void TriggerNoise() {
        secondsSinceEvent = 0;
    }
    void Awake() {
        // use the camera's skybox if custom, otherwise the global skybox
        skybox = GetComponent<Skybox>()?.material ?? RenderSettings.skybox;
    }

    void Update() {
        // Debug input
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            TriggerNoise();

        secondsSinceEvent += Time.deltaTime;
        float t = Mathf.Clamp01(1 - secondsSinceEvent/decaySeconds);
        if (useNoise)
            t *= Mathf.PerlinNoise1D(Time.time * noiseScale);
        var intensity = Mathf.Lerp(normalIntensity, eventIntensity, t);
        skybox.SetFloat("_Exposure", intensity);
    }
}
