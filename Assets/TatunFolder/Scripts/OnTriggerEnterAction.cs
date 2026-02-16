using UnityEngine;
using UnityEngine.Events;

public class OnTriggerEnterAction : MonoBehaviour
{
    [SerializeField] UnityEvent onTriggerEnter;

    private void OnTriggerEnter(Collider other)
    {
        // check only if the player enters then trigger
        if (other.CompareTag("Player"))
            onTriggerEnter.Invoke();
    }
}
