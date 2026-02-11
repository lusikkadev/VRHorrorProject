using UnityEngine;
using UnityEngine.InputSystem;
public class LightningEffect : MonoBehaviour
{
    Animator animator;
    PlayerInput playerInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        playerInput = GetComponent<PlayerInput>();
    }

    public void playLightningEffect() {
        animator.Play("Lightning");
    }
    // Update is called once per frame
    void Update()
    {

        if (Keyboard.current.enterKey.wasPressedThisFrame) {
            playLightningEffect();
        }
    }
}
