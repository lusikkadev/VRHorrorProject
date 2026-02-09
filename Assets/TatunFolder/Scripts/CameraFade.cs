using System.Collections;
using UnityEngine;

public class CameraFade : MonoBehaviour
{
    [SerializeField] Material headBoxMaterial;
    [SerializeField] float fadeDuration = 0.3f;
    [SerializeField] float startFadeDuration = 10f;

    private void Awake()
    {
        headBoxMaterial = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        //StartCoroutine(StartGameFadeCoroutine());
    }
    private void OnTriggerEnter(Collider other)
    {
        int layer = LayerMask.NameToLayer("WallsAndObjects");
        // if the collided one is on layer WallsAndObjects
        if (other.gameObject.layer == layer)
        {
            StartCoroutine(FadeToBlackCoroutine());
            //Color color = headBoxMaterial.color;
            //headBoxMaterial.color = new Color(color.r, color.g, color.b, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        int layer = LayerMask.NameToLayer("WallsAndObjects");

        if (other.gameObject.layer == layer)
        {
            StopCoroutine(FadeToBlackCoroutine());
            StartCoroutine(FadeToViewCoroutine());
            //Color color = headBoxMaterial.color;
            //headBoxMaterial.color = new Color(color.r, color.g, color.b, 0f);
        }

    }

    private IEnumerator FadeToBlackCoroutine()
    {
        // change the headbox material color alpha to 1 over fadeDuration seconds
        Color initialColor = headBoxMaterial.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 1f);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            headBoxMaterial.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeToViewCoroutine()
    {
        Color initialColor = headBoxMaterial.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            headBoxMaterial.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }


    public IEnumerator StartGameFadeCoroutine()
    {
        // fade in from black to view
        Color initialColor = headBoxMaterial.color;
        Color targetColor = new Color(initialColor.r, initialColor.g, initialColor.b, 0f);
        float elapsedTime = 0f;
        yield return new WaitForSeconds(5f);
        while (elapsedTime < startFadeDuration)
        {
            headBoxMaterial.color = Color.Lerp(initialColor, targetColor, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}