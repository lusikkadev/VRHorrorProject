using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class PhoneInteractions : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI phoneText;

    public void PhoneClicked()
    {
        phoneText.text = "CLICKED";
    }
}