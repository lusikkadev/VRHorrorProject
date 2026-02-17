using UnityEngine;
using UnityEngine.XR;

public class PeepholeController : MonoBehaviour {
    // TODO: in VR have to check for closer eye and use that position!
    [SerializeField] Transform userCam;
    [SerializeField] bool usingVR = false;
    [SerializeField] float eyeOffsetWorldMax = 0.1f;
    [SerializeField] bool useCamTilt = true;
    [SerializeField] bool useSpherizeOffset = false;
    [SerializeField] bool useSpherizeCenter = false;
    [SerializeField] float camTiltMaxDegrees = 10f;
    [SerializeField] float spherizeOffsetMax = 0.3f;
    [SerializeField] float spherizeCenterMax = 0.3f;
    [SerializeField] bool useDebugOffsetInput = false;
    [SerializeField][Range(-0.2f, 0.2f)] float debugOffsetInputX = 0f;
    [SerializeField][Range(-0.2f, 0.2f)] float debugOffsetInputY = 0f;
    [SerializeField] Transform peepholeCam;
    Material mat;
    Quaternion camInitialRot;

    void Start() {
        mat = GetComponent<Renderer>().material;
        camInitialRot = peepholeCam.localRotation;
    }

    void Update() {
        Vector2 localEyePos = Vector2.zero;

        if (usingVR) {
            // select eye closer to peephole axis
            Vector3 leftEyeLocal = InputTracking.GetLocalPosition(XRNode.LeftEye);
            Vector3 rightEyeLocal = InputTracking.GetLocalPosition(XRNode.RightEye);
            Matrix4x4 m = userCam.GetComponent<Camera>().cameraToWorldMatrix;
            Vector3 leftEyeWorld = m.MultiplyPoint(leftEyeLocal);
            Vector3 rightEyeWorld = m.MultiplyPoint(rightEyeLocal);
            var localEyePosLeft = (Vector2)transform.InverseTransformPoint(leftEyeWorld);
            var localEyePosRight = (Vector2)transform.InverseTransformPoint(rightEyeWorld);
            localEyePos = localEyePosLeft.magnitude < localEyePosRight.magnitude ?
                          localEyePosLeft : localEyePosRight;
        } else { // normal non-VR camera
            var eyePos = userCam.position;
            localEyePos = (Vector2)transform.InverseTransformPoint(eyePos);
        }

        if (useDebugOffsetInput)
            localEyePos = new Vector2(debugOffsetInputX, debugOffsetInputY);
        float x = Mathf.InverseLerp(-eyeOffsetWorldMax, eyeOffsetWorldMax, localEyePos.x);
        x = x * 2 - 1;
        float y = Mathf.InverseLerp(-eyeOffsetWorldMax, eyeOffsetWorldMax, localEyePos.y);
        y = y * 2 - 1;
        var normalizedEyeOffset = new Vector2(x, y);

        // could also try Mathf.Smoothstep(0,1,xy)?

        // remap to slight hole camera pan/tilt and/or shader effects
        if (useCamTilt) {
            var camTilts = -normalizedEyeOffset * camTiltMaxDegrees;
            peepholeCam.localRotation = Quaternion.Euler(-camTilts.y,
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
