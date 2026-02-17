using UnityEngine;

public class PeepholeController : MonoBehaviour {
    // TODO: in VR have to check for closer eye and use that position!
    [SerializeField] Transform userCam;
    [SerializeField] float eyeOffsetWorldMax = 0.1f;
    [SerializeField] bool useCamTilt = true;
    [SerializeField] bool useSpherizeOffset = false;
    [SerializeField] bool useSpherizeCenter = false;
    [SerializeField] float camTiltMaxDegrees = 10f;
    [SerializeField] float spherizeOffsetMax = 0.3f;
    [SerializeField] float spherizeCenterMax = 0.3f;

    [SerializeField][Range(-0.2f, 0.2f)] float debugOffsetInputX = 0f;
    [SerializeField][Range(-0.2f, 0.2f)] float debugOffsetInputY = 0f;
    Transform cam;
    Material mat;
    Quaternion camInitialRot;

    void Start() {
        cam = GetComponentInChildren<Camera>().transform;
        mat = GetComponent<Renderer>().material;
        camInitialRot = cam.localRotation;
    }

    void Update() {
        var eyePos = userCam.position; // TODO: closer eye position (VR)
        var localEyePos = (Vector2)transform.InverseTransformPoint(eyePos);
        localEyePos = new Vector2(debugOffsetInputX, debugOffsetInputY); // DEBUG
        float x = Mathf.InverseLerp(-eyeOffsetWorldMax, eyeOffsetWorldMax, localEyePos.x);
        x = x * 2 - 1;
        float y = Mathf.InverseLerp(-eyeOffsetWorldMax, eyeOffsetWorldMax, localEyePos.y);
        y = y * 2 - 1;
        var normalizedEyeOffset = new Vector2(x, y);

        // could also try Mathf.Smoothstep(0,1,xy)?

        // remap to slight hole camera pan/tilt and/or shader effects
        if (useCamTilt) {
            var camTilts = -normalizedEyeOffset * camTiltMaxDegrees;
            cam.localRotation = Quaternion.Euler(camTilts.y,
                                                 camTilts.x,
                                                 0) * camInitialRot;
        }
        if (useSpherizeOffset)
            mat.SetVector("_SpherizeOffset", 
                          Vector2.zero + normalizedEyeOffset * spherizeOffsetMax);
        if (useSpherizeCenter)
            mat.SetVector("_SpherizeCenter", 
                          Vector2.one * 0.5f + normalizedEyeOffset * spherizeCenterMax);

    }
}
