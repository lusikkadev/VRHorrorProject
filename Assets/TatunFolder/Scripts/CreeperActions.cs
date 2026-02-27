using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperActions : MonoBehaviour
{
    [SerializeField] Animator creeperAnim;
    [SerializeField] GameObject player;
    [SerializeField] float creeperSpeed = 10f;
    [SerializeField] bool isRunning = false;

    Coroutine currentRunCoroutine;

    private void Awake()
    {
        if (creeperAnim == null)
            creeperAnim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (isRunning)
        {
            transform.position += transform.forward * creeperSpeed * Time.deltaTime;
        }
    }

    public void StartTwitching()
    {
        creeperAnim.SetTrigger("Twitch");
    }

    public IEnumerator RunningCoroutine()
    {
        isRunning = false;

        creeperAnim.ResetTrigger("Idle");
        creeperAnim.ResetTrigger("Run");
        creeperAnim.ResetTrigger("Stare");

        creeperAnim.SetTrigger("Stare");
        yield return new WaitForSeconds(5f);
        creeperAnim.SetTrigger("Run");
        isRunning = true;
    }

    public void StopRunning()
    {
        if (currentRunCoroutine != null)
        {
            StopCoroutine(currentRunCoroutine);
            currentRunCoroutine = null;
        }

        isRunning = false;

        creeperAnim.ResetTrigger("Run");
        creeperAnim.ResetTrigger("Stare");

        creeperAnim.SetTrigger("Idle");
    }

    public void SetToIdle()
    {
        if (currentRunCoroutine != null)
        {
            StopCoroutine(currentRunCoroutine);
            currentRunCoroutine = null;
        }
        creeperAnim.ResetTrigger("Run");
        creeperAnim.ResetTrigger("Stare");
        creeperAnim.ResetTrigger("Twitch");

        creeperAnim.SetTrigger("Idle");
    }

    public void StartRunning()
    {
        if (currentRunCoroutine != null)
        {
            StopCoroutine(currentRunCoroutine);
        }
        currentRunCoroutine = StartCoroutine(RunningCoroutine());
    }

}
