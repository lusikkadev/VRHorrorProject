using UnityEngine;

public class PeepholeController : MonoBehaviour {
    // TODO: in VR have to check for closer eye and use that position!
    [SerializeField] Transform userCam;
    Transform cam;

    void Start() {
        cam = GetComponentInChildren<Camera>().transform;
    }

    void Update() {
        var eyePos = userCam.position; // TODO
        var localEyePos= transform.InverseTransformPoint(eyePos);
        var eyeOffset = (Vector2)localEyePos;
        // normalize offset
        // remap to super slight hole camera pan/tilt (& position shift?)


    }
}
