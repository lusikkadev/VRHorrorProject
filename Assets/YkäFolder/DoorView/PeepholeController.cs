using UnityEngine;

public class PeepholeController : MonoBehaviour {
    // TODO: in VR have to check for closer eye and use that position!
    [SerializeField] Transform userCam;
    Transform cam;

    void Start() {
        cam = GetComponentInChildren<Camera>().transform;
    }

    void Update() {

    }
}
