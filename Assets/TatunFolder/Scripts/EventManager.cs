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
    [SerializeField] Animator creeperAnim;

    [Header("Creeper Anchor Points")]
    [SerializeField] Transform creeperAnchorRoad;
    [SerializeField] Transform creeperAnchorLivingRoom;
    [SerializeField] Transform creeperAnchorNeighbour;
    [SerializeField] Transform creeperAnchorShower;
    [SerializeField] Transform creeperAnchorBedroom;
    [SerializeField] Transform creeperAnchorEnd;
    [SerializeField] Transform creeperAnchorPeephole;

    [Header("Wall Hole Trigger Colliders")]
    [SerializeField] GameObject wallHoleTriggerCollider1;
    [SerializeField] GameObject wallHoleTriggerCollider2;
    [SerializeField] GameObject wallHoleTriggerCollider3;

    [Header("Environment Trigger Colliders")]
    [SerializeField] GameObject roadTriggerCollider;
    [SerializeField] GameObject doorTriggerCollider;
    [SerializeField] GameObject bathroomTriggerCollider;
    [SerializeField] GameObject livingRoomTriggerCollider;
    [SerializeField] GameObject endingTriggerCollider;

    [Header("Booleans")]
    bool phonePickedUp = false;

    private void Awake()
    {
        lightningEffect = FindAnyObjectByType<LightningEffect>();
        cameraFade = FindAnyObjectByType<CameraFade>();
        scareDisplay = FindAnyObjectByType<ScareDisplay>();
        creeperAnim = creeperPrefab.GetComponent<Animator>();
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
        yield return new WaitForSeconds(8f);

        AudioManager.instance.PlayPhoneRing();
        // Wake up audio etc.
        debugText.text = "Start sequence done";

        while (!progressGates.Contains("PickedUpPhoneDone"))
        {
            debugText.text = "Waiting for phone pickup";
            yield return null;
        }

        
        debugText.text = "Picked Up Phone";
        AudioManager.instance.StopPhoneRing();
        yield return new WaitForSeconds(5f);

        lightningEffect.playLightningEffect();
        creeperPrefab.SetActive(true);
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlayLightningSound();
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlayWomanScream();
        // Start window sequence animation
        yield return new WaitForSeconds(5f);

        lightningEffect.playLightningEffect();
        creeperPrefab.SetActive(false);
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlayLightningSound();
        // Lights out
        progressGates.Add("WindowSequenceDone");
        
        while (!progressGates.Contains("WindowSequenceDone"))
        {
            yield return null;
        }

        debugText.text = "Window sequence done";
        wallHoleTriggerCollider1.SetActive(true);
        yield return new WaitForSeconds(8f);

        lightningEffect.playLightningEffect();
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlayLightningSound();
        lightningEffect.TurnOffLights();
        // Wait for when the player checks the hole in the wall

        while (!progressGates.Contains("FreeroamSequenceDone"))
        {
            yield return null;
        }
        wallHoleTriggerCollider1.SetActive(false);
        StartCoroutine(lightningEffect.NeighborLightOff());
        debugText.text = "Freeroam sequence done";
        // Move creeper to the first anchor point on the road, enable road collider trigger
        creeperPrefab.SetActive(true);
        creeperPrefab.transform.position = creeperAnchorRoad.position;
        roadTriggerCollider.SetActive(true);

        while (!progressGates.Contains("CreeperSeenOnRoadDone"))
        {
            yield return null;
        }

        roadTriggerCollider.SetActive(false);
        debugText.text = "Creeper seen on road";
        yield return new WaitForSeconds(2f);
        debugText.text = "Starting creeper run";
        creeperActions.StartCoroutine(creeperActions.RunningCoroutine());
        yield return new WaitForSeconds(8f);

        creeperActions.StopRunning();
        lightningEffect.PlayLightningAfterBlackOut();
        yield return new WaitForSeconds(2f);

        AudioManager.instance.PlayLightningSound();
        progressGates.Add("RoadSequenceDone");

        while (!progressGates.Contains("RoadSequenceDone"))
        {
            yield return null;
        }
        creeperPrefab.transform.position = creeperAnchorLivingRoom.position;
        livingRoomTriggerCollider.SetActive(true);
        debugText.text = "Road sequence done, starting free roam";
        scareDisplay.StartScareDisplay();

        while (!progressGates.Contains("CreeperSeenInLivingRoomDone"))
        {
            yield return null;
        }

        debugText.text = "Creeper seen in living room";

        livingRoomTriggerCollider.SetActive(false);
        yield return new WaitForSeconds(3f);
        creeperPrefab.transform.position = creeperAnchorNeighbour.position;
        creeperPrefab.transform.rotation = creeperAnchorNeighbour.rotation;
        creeperActions.StartTwitching();
        yield return new WaitForSeconds(10f);

        debugText.text = "Starting creeper sounds";
        AudioManager.instance.LoopCreeperSound();
        yield return new WaitForSeconds(10f);

        
        debugText.text = "Starting maggot sounds";
        AudioManager.instance.StopCreeperSound();
        AudioManager.instance.LoopMaggotSound();
        yield return new WaitForSeconds(8f);

        debugText.text = "Preparing for doorbell";
        AudioManager.instance.StopMaggotSound();
        lightningEffect.PlayLightningAfterBlackOut();
        AudioManager.instance.PlayLightningSound();
        yield return new WaitForSeconds(10f);

        doorTriggerCollider.SetActive(true);
        AudioManager.instance.PlayDoorBellSound();
        creeperPrefab.transform.position = creeperAnchorPeephole.position;
        creeperPrefab.transform.rotation = creeperAnchorPeephole.rotation;

        while (!progressGates.Contains("PlayerLookedThroughDoorDone"))
        {
            yield return null;
        }

        // Start the door eye sequence.

        
        debugText.text = "Player looked through door";
        yield return new WaitForSeconds(5f);

        bathroomTriggerCollider.SetActive(true);
        creeperPrefab.transform.position = creeperAnchorShower.position;
        creeperPrefab.transform.rotation = creeperAnchorShower.rotation;
        AudioManager.instance.LoopShowerSound();
        creeperActions.StartTwitching();
        while (!progressGates.Contains("PlayerEnteredBathroomDone"))
        {
            yield return null;
        }
        doorTriggerCollider.SetActive(false);
        AudioManager.instance.StopShowerSound();
        yield return new WaitForSeconds(2f);
        AudioManager.instance.LoopCreeperSound();
        yield return new WaitForSeconds(8f);
        AudioManager.instance.StopCreeperSound();
        creeperPrefab.SetActive(false);
        yield return new WaitForSeconds(3f);
        progressGates.Add("BathroomSequenceDone");

        while (!progressGates.Contains("BathroomSequenceDone"))
        {
            yield return null;
        }
        creeperPrefab.SetActive(true);
        creeperPrefab.transform.position = creeperAnchorEnd.position;
        creeperPrefab.transform.rotation = creeperAnchorEnd.rotation;
        creeperActions.SetToIdle();
        lightningEffect.TurnOnLights();
        endingTriggerCollider.SetActive(true);
        while (!progressGates.Contains("CreeperEndingSeenDone"))
        {
            yield return null;
        }

        endingTriggerCollider.SetActive(false);
        yield return new WaitForSeconds(3f);
        lightningEffect.TurnOffLights();
        AudioManager.instance.LoopCreeperSound();
        yield return new WaitForSeconds(2f);
        lightningEffect.PlayLightningAfterBlackOut();
        AudioManager.instance.PlayLightningSound();
        yield return new WaitForSeconds(4f);
        creeperActions.StartCoroutine(creeperActions.RunningCoroutine());
        yield return new WaitForSeconds(8f);
        cameraFade.EndGameToBlack();

        while (!progressGates.Contains("PlayerCaughtDone"))
        {
            yield return null;
        }
        // End sequence, phone turns on, creeper scare

        // Coroutine for endgame.
    }
}
