using UnityEngine;
using UnityEngine.InputSystem;
public class LightningEffect : MonoBehaviour
{
    Animator animator;
    [SerializeField] GameObject LightsParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void playLightningEffect() {
        animator.Play("Lightning");
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

        if (Keyboard.current.enterKey.wasPressedThisFrame) {
            playLightningEffect();
        }
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) {
            TurnOnLights();
        }
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) {
            TurnOffLights();
        }
    }
}
