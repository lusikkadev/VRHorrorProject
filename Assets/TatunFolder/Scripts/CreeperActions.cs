using UnityEngine;

public class CreeperActions : MonoBehaviour
{
    [SerializeField] Animator creeperAnim;
    [SerializeField] float creeperSpeed = 10f;
    [SerializeField] bool isRunning = false;

    [Header("Hat")]
    [SerializeField] GameObject hat;
    [SerializeField] Transform head;

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

    public void StartRunning()
    {
        isRunning = true;
        creeperAnim.SetTrigger("Run");
    }
}
