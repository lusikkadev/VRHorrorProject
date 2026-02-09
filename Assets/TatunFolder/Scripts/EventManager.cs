using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;

public class EventManager : MonoBehaviour
{
    [SerializeField] HashSet<string> progressGates = new HashSet<string>();

    [Header("References")]
    [SerializeField] CameraFade cameraFade;
    [SerializeField] ScareDisplay scareDisplay;
    [SerializeField] TMPro.TextMeshProUGUI debugText;

    [Header("Booleans")]
    bool phonePickedUp = false;

    // Gates: StartSequence, PickedUpPhone, FreeRoamSequence, RoadSequence, FreeRoamSequence, etc.

    private void Awake()
    {
        cameraFade = FindAnyObjectByType<CameraFade>();
        scareDisplay = FindAnyObjectByType<ScareDisplay>();

        //progressGates.Add("StartSequenceDone");
        //progressGates.Add("PickedUpPhoneDone");
        //progressGates.Add("WindowSequenceDone");
        //progressGates.Add("FreeroamSequenceDone");
    }

    public void SetSequenceDone(string sequenceName)
    {
        if (progressGates.Contains(sequenceName + "Done"))
        {
            return;
        }
        progressGates.Add(sequenceName + "Done");
    }

    public void PhoneFirstPickUp()
    {
        if (phonePickedUp)
        {
            return;
        }
        progressGates.Add("PickedUpPhoneDone");
        phonePickedUp = true;
    }

    private void Start()
    {
        StartCoroutine(RunGameScript());
    }

    IEnumerator RunGameScript()
    {
        while (!progressGates.Contains("StartSequenceDone"))
        {
            AudioManager.instance.StartWindAndThunder();
            cameraFade.StartCoroutine(cameraFade.StartGameFadeCoroutine());
            AudioManager.instance.PlayPhoneRing();
            // Wake up audio etc.
            debugText.text = "Start sequence done";
            yield return new WaitForSeconds(3f);
            progressGates.Add("StartSequenceDone");
            yield return null;
        }
        while (!progressGates.Contains("PickedUpPhoneDone"))
        {
            debugText.text = "Waiting for phone pickup";
            yield return null;
        }
        while (!progressGates.Contains("WindowSequenceDone"))
        {
            debugText.text = "Picked Up Phone";
            AudioManager.instance.StopPhoneRing();
            yield return new WaitForSeconds(5f);
            AudioManager.instance.PlayWomanScream();
            // thunder, neighbor building scream, animation.
            progressGates.Add("WindowSequenceDone");
            yield return null;
        }
        while (!progressGates.Contains("FreeroamSequenceDone"))
        {
            //scareDisplay.StartCoroutine(scareDisplay.ScareCoroutine());
            //yield return new WaitForSeconds(30f);
            Debug.Log("Waiting for freeroam sequence");
            yield return null;
        }
    }
}
