using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
public class AttachToHand : MonoBehaviour
{
    public Transform attachPoint;
    private bool isAttached = false;
    public bool isGripping = false;

    [SerializeField]
    XRInputValueReader<float> m_GripInput = new XRInputValueReader<float>("Grip");
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void OnEnable()
    {
        m_GripInput?.EnableDirectActionIfModeUsed();
    }
    private void OnDisable()
    {
        m_GripInput?.DisableDirectActionIfModeUsed();
    }

    public void SnapToHand()
    {
        transform.position = attachPoint.position;
        transform.rotation = attachPoint.rotation;
        transform.SetParent(attachPoint);
        isAttached = true;
    }

    void Update()
    {
        if (isAttached && m_GripInput.ReadValue() > 0.1f)
            // Keep the object at zero of attach points local pos and rot
            transform.localPosition = Vector3.zero;
            transform.localRotation = Quaternion.identity;
        }
    }



