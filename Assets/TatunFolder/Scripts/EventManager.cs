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
    [SerializeField] LightningEffect lightningEffect;
    [SerializeField] GameObject creeperPrefab;
    [SerializeField] CreeperActions creeperActions;

    [Header("Creeper Anchor Points")]
    [SerializeField] Transform creeperAnchor1;
    [SerializeField] Transform creeperAnchor2;
    [SerializeField] Transform creeperAnchor3;

    [Header("Wall Hole Trigger Colliders")]
    [SerializeField] GameObject wallHoleTriggerCollider1;
    [SerializeField] GameObject wallHoleTriggerCollider2;
    [SerializeField] GameObject wallHoleTriggerCollider3;

    [Header("Environment Trigger Colliders")]
    [SerializeField] GameObject roadTriggerCollider;
    [SerializeField] GameObject doorTriggerCollider;
    [SerializeField] GameObject bathroomTriggerCollider;

    [Header("Booleans")]
    bool phonePickedUp = false;

    float freeRoamTimer = 1f;

    // Gates: StartSequence, PickedUpPhone, FreeRoamSequence, RoadSequence, FreeRoamSequence, etc.

    private void Awake()
    {
        lightningEffect = FindAnyObjectByType<LightningEffect>();
        cameraFade = FindAnyObjectByType<CameraFade>();
        scareDisplay = FindAnyObjectByType<ScareDisplay>();
        creeperActions = FindAnyObjectByType<CreeperActions>();

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
        //debugText.text = sequenceName + " done";
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
        creeperPrefab.SetActive(true);
        AudioManager.instance.PlayLightningSound();
        lightningEffect.playLightningEffect();
        yield return new WaitForSeconds(3f);
        AudioManager.instance.PlayWomanScream();
        // Start window sequence animation
        yield return new WaitForSeconds(10f);
        AudioManager.instance.PlayLightningSound();
        lightningEffect.playLightningEffect();
        creeperPrefab.SetActive(false);
        // Lights out
        progressGates.Add("WindowSequenceDone");
        
        while (!progressGates.Contains("WindowSequenceDone"))
        {
            yield return null;
        }

        debugText.text = "Window sequence done";
        wallHoleTriggerCollider1.SetActive(true);
        yield return new WaitForSeconds(2f);
        lightningEffect.playLightningEffect();
        yield return new WaitForSeconds(3f);
        lightningEffect.TurnOffLights();
        // Wait for when the player checks the hole in the wall

        while (!progressGates.Contains("FreeroamSequenceDone"))
        {
            yield return null;
        }
        wallHoleTriggerCollider1.SetActive(false);

        debugText.text = "Freeroam sequence done";
        // Move creeper to the first anchor point on the road, enable road collider trigger
        creeperPrefab.SetActive(true);
        creeperPrefab.transform.position = creeperAnchor1.position;
        roadTriggerCollider.SetActive(true);

        while (!progressGates.Contains("CreeperSeenOnRoadDone"))
        {
            yield return null;
        }
        roadTriggerCollider.SetActive(false);
        debugText.text = "Creeper seen on road";
        yield return new WaitForSeconds(5f);
        creeperActions.StartRunning();
        yield return new WaitForSeconds(5f);
        lightningEffect.PlayLightningAfterBlackOut();
        creeperPrefab.SetActive(false);
        progressGates.Add("RoadSequenceDone");

        while (!progressGates.Contains("RoadSequenceDone"))
        {
            yield return null;
        }
        debugText.text = "Road sequence done, starting free roam";
        // Freeroam for N minutes enable all needed here
        scareDisplay.StartScareDisplay();
        creeperPrefab.SetActive(true);
        creeperPrefab.transform.position = creeperAnchor2.position;
        yield return new WaitForSeconds(5f);
        AudioManager.instance.PlayCreeperSound();
        yield return new WaitForSeconds(8f);
        AudioManager.instance.StopCreeperSound();
        lightningEffect.PlayLightningAfterBlackOut();
        yield return new WaitForSeconds(5f);
        AudioManager.instance.PlayDoorBellSound();
        doorTriggerCollider.SetActive(true);
        // Instantiate creeper image at the door

        while (!progressGates.Contains("PlayerLookedThroughDoorDone"))
        {
            yield return null;
        }
        lightningEffect.TurnOnLights();
        doorTriggerCollider.SetActive(false);
        debugText.text = "Player looked through door";
        yield return new WaitForSeconds(5f);
        bathroomTriggerCollider.SetActive(true);
        AudioManager.instance.LoopShowerSound();
        creeperPrefab.transform.position = creeperAnchor3.position;
        // Water turns on in the bathroom, instantiate creeper in bathroom behind shower curtain.
        // Wait for player to open curtain in bathroom
        while (!progressGates.Contains("PlayerEnteredBathroomDone"))
        {
            yield return null;
        }
        AudioManager.instance.StopShowerSound();

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
