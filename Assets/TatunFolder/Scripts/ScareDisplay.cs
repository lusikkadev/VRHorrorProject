using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScareDisplay : MonoBehaviour
{
    [SerializeField] Sprite originalSprite;
    [SerializeField] Sprite[] scareSprites;
    [SerializeField] float scareDuration = 0.5f;
    [SerializeField] float scareInterval = 10f;
    [SerializeField] Material originalMat;
    private void Start()
    {
        GetComponent<Image>().sprite = originalSprite;
        StartCoroutine(ScareCoroutine());
    }

    public void Scare()
    {
        StartCoroutine(ScareCoroutine());
    }

    public void StopScare()
    {
        StopAllCoroutines();
        GetComponent<Image>().sprite = originalSprite;
    }

    private IEnumerator ScareCoroutine()
    {
        while (true)
        {
            Sprite scareSprite = scareSprites[Random.Range(0, scareSprites.Length)];
            GetComponent<Image>().material = null;
            GetComponent<Image>().sprite = scareSprite;
            yield return new WaitForSeconds(scareDuration);
            GetComponent<Image>().material = originalMat;
            GetComponent<Image>().sprite = originalSprite;
            yield return new WaitForSeconds(scareInterval);
        }
    }



}
