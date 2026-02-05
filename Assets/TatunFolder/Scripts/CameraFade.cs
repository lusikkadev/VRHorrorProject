using System.Collections;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [SerializeField] Material headBoxMaterial;
    [SerializeField] float fadeDuration = 0.5f;

    private void Awake()
    {
        headBoxMaterial = GetComponent<Renderer>().material;
    }
    private void OnTriggerEnter(Collider other)
    {
        int layer = LayerMask.NameToLayer("WallsAndObjects");
        // if the collided one is on layer WallsAndObjects
        if (other.gameObject.layer == layer)
        {
            //StartCoroutine(FadeToBlackCoroutine());
            Color color = headBoxMaterial.color;
            headBoxMaterial.color = new Color(color.r, color.g, color.b, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int layer = LayerMask.NameToLayer("WallsAndObjects");

        if (other.gameObject.layer == layer)
        {
            // change the headbox material color alpha to 0 immediately
            Color color = headBoxMaterial.color;
            headBoxMaterial.color = new Color(color.r, color.g, color.b, 0f);
        }

    }

    //private IEnumerator FadeToBlackCoroutine()
    //{
    //    // change the headbox material color alpha to 1 over fadeDuration seconds
    //    Color initialColor = headBoxMaterial.color;
    //    Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
    //    float elapsedTime = 0f;
    //    while (elapsedTime < fadeDuration)
    //    {
    //        headBoxMaterial.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //}
}