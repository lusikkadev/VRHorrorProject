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
    [SerializeField] TMPro.TextMeshProUGUI clockText;
    [SerializeField] int hours = 01;
    [SerializeField] int minutes = 23;

    private void Start()
    {
        GetComponent<Image>().sprite = originalSprite;
        //StartCoroutine(ScareCoroutine());
    }

    private void Update()
    {
        // Update minutes on phone display starting at 01:23 and increasing every minute
        var timer = 0f;
        if (timer < 60f)
        {
            timer += Time.deltaTime;
            clockText.text = hours.ToString("D2") + ":" + minutes.ToString("D2");
            if (timer >= 60f)
            {
                minutes++;
                var elapsedTime = timer;
                timer -= elapsedTime;
            }
        }
    }

    public void StartScareDisplay()
    {
        StartCoroutine(ScareCoroutine());
    }

    private IEnumerator ScareCoroutine()
    {
        while (true)
        {
            scareInterval = Random.Range(5f, 15f);
            Sprite scareSprite = scareSprites[Random.Range(0, scareSprites.Length)];
            GetComponent<Image>().material = null;
            GetComponent<Image>().sprite = scareSprite;
            AudioManager.instance.PlayCreepySFX(Random.Range(1, 3));
            yield return new WaitForSeconds(scareDuration);
            GetComponent<Image>().material = originalMat;
            GetComponent<Image>().sprite = originalSprite;
            yield return new WaitForSeconds(scareInterval);
        }
    }



}
