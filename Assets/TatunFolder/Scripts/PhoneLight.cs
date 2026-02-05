using UnityEngine;

public class PhoneLight : MonoBehaviour
{

    public bool isLightOn = true;
    public GameObject lightObject;

    public void ToggleLight()
    {
        if (isLightOn)
        {
            lightObject.SetActive(false);
            isLightOn = false;
        }
        else
        {
            lightObject.SetActive(true);
            isLightOn = true;
        }
    }
}