using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterAction : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;
    [SerializeField] UnityEvent onTriggerStay;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            onTriggerEnter.Invoke();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
            onTriggerStay.Invoke();
    }
}