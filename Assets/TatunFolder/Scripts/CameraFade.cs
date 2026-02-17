using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraFade : MonoBehaviour
{
    [SerializeField] Image headBox;
    [SerializeField] Color startColor = new Color(0f, 0f, 0f, 1f);
    //[SerializeField] float fadeDuration = 0.3f;
    [SerializeField] float startFadeDelay = 10f;

    Coroutine fadeRoutine;

    private void Start()
    {
        headBox.color = startColor;
        StartCoroutine(StartGameFadeCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallsAndObjects"))
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            fadeRoutine = StartCoroutine(FadeToBlackCoroutine());
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("WallsAndObjects"))
        {
            if (fadeRoutine != null)
            {
                StopCoroutine(fadeRoutine);
            }
            fadeRoutine = StartCoroutine(FadeToViewCoroutine());
        }

    }

    private IEnumerator FadeToBlackCoroutine()
    {
        float alpha = headBox.color.a;
        Color startColor = headBox.color;
        while (alpha < 1f)
        {
            headBox.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            alpha += 0.8f * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeToViewCoroutine()
    {
        float alpha = headBox.color.a;
        Color startColor = headBox.color;
        while (alpha > 0f)
        {
            headBox.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            alpha -= 0.8f * Time.deltaTime;
            yield return null;
        }
    }

    public IEnumerator StartGameFadeCoroutine()
    {
        yield return new WaitForSeconds(startFadeDelay);
        float alpha = 1f;
        while (alpha > 0f)
        {
            headBox.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
            alpha -= 0.1f * Time.deltaTime;
            yield return null;
        }
    }

    public void EndGameToBlack()
    {
        StartCoroutine(EndGameFadeCoroutine());
    }

    private IEnumerator EndGameFadeCoroutine()
    {
        headBox.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
        yield return null;
    }
}