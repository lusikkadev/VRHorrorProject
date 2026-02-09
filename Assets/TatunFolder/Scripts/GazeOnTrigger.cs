using UnityEngine;

public class GazeOnTrigger : MonoBehaviour
{

    [SerializeField] TMPro.TextMeshProUGUI debugText;

    public void GazedON()
    {
        debugText.text = "Gazed ON";
    }
}
