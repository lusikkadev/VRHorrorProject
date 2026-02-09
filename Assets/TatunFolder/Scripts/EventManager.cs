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
        AudioManager.instance.StartWindAndThunder();
        AudioManager.instance.PlayPhoneRing();
        cameraFade.StartCoroutine(cameraFade.StartGameFadeCoroutine());
        // Wake up audio etc.
        debugText.text = "Start sequence done";
        yield return new WaitForSeconds(3f);

        while (!progressGates.Contains("PickedUpPhoneDone"))
        {
            debugText.text = "Waiting for phone pickup";
            yield return null;
        }

        debugText.text = "Picked Up Phone";
        AudioManager.instance.StopPhoneRing();
        yield return new WaitForSeconds(5f);
        AudioManager.instance.PlayWomanScream();
        yield return new WaitForSeconds(2f);
        // Start window sequence

        while (!progressGates.Contains("WindowSequenceDone"))
        {
            Debug.Log("Waiting for window sequence to be done");
            yield return null;
        }

        // freeroam whatever

        while (!progressGates.Contains("FreeroamSequenceDone"))
        {
            yield return null;
        }

    }
}
