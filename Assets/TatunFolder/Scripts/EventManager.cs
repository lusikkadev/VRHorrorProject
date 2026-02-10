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

    float freeRoamTimer = 5f;

    // Gates: StartSequence, PickedUpPhone, FreeRoamSequence, RoadSequence, FreeRoamSequence, etc.

    private void Awake()
    {
        cameraFade = FindAnyObjectByType<CameraFade>();
        scareDisplay = FindAnyObjectByType<ScareDisplay>();

        //progressGates.Add("StartSequenceDone");
        //progressGates.Add("PickedUpPhoneDone");
        //progressGates.Add("WindowSequenceDone");
        //progressGates.Add("FreeroamSequenceDone");
        //progressGates.Add("CreeperSeenOnRoad");
        //progressGates.Add("RoadSequenceDone");
        //progressGates.Add("PlayerLookedThrougDoorDone");
        //progressGates.Add("BathroomSequenceDone");
        //progressGates.Add("EnteredBedroomDone");
        //progressGates.Add("PlayerChargingPhoneDone");
        //progressGates.Add("EndSequenceDone");
    }

    public void SetSequenceDone(string sequenceName)
    {
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
        // Wake up audio etc.
        debugText.text = "Start sequence done";
        yield return new WaitForSeconds(5f);

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
        // Start window sequence animation

        while (!progressGates.Contains("WindowSequenceDone"))
        {
            yield return null;
        }

        // Wait for when the player checks the hole in the wall

        while (!progressGates.Contains("FreeroamSequenceDone"))
        {
            yield return null;
        }

        // Instantiate creeper on road, wait for player to see it.

        while (!progressGates.Contains("CreeperSeenOnRoad"))
        {
            yield return null;
        }

        // Creeper on road sequence

        while (!progressGates.Contains("RoadSequenceDone"))
        {
            yield return null;
        }

        // Freeroam for N minutes
        scareDisplay.StartScareDisplay();

        yield return new WaitForSeconds(freeRoamTimer * 60f);
        // Instantiate creeper image at the door
        // Knock on door or pimpom

        while (!progressGates.Contains("PlayerLookedThrougDoorDone"))
        {
            yield return null;
        }

        // Door sequence
        yield return new WaitForSeconds(5f);
        // Water turns on in the bathroom, instantiate creeper in bathroom behind shower curtain.
        // Wait for player to open curtain in bathroom
        while (!progressGates.Contains("BathroomSequenceDone"))
        {
            yield return null;
        }
        // Phone rings/text message on phone
        // Instantiate creeper in bedroom closet, wait for player to enter bedroom

        while (!progressGates.Contains("EnteredBedroomDone"))
        {
            yield return null;
        }

        // Creeper closes the closet.
        // Lightning and thunder, phone dies
        
        while (!progressGates.Contains("PlayerChargingPhoneDone"))
        {
            yield return null;
        }

        // End sequence, phone turns on, creeper scare

        // Coroutine for endgame.
    }
}
