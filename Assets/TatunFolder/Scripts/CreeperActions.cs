using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreeperActions : MonoBehaviour
{
    [SerializeField] Animator creeperAnim;
    [SerializeField] float creeperSpeed = 10f;
    [SerializeField] bool isRunning = false;


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

    public IEnumerator RunningCoroutine()
    {
        creeperAnim.SetTrigger("Stare");
        yield return new WaitForSeconds(5f);
        creeperAnim.SetTrigger("Run");
        isRunning = true;
    }

    public void StopRunning()
        {
            isRunning = false;
            creeperAnim.SetTrigger("Idle");
    }
}
