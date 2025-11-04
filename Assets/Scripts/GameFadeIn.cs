using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GameFadeScript : MonoBehaviour
{
    [SerializeField]
    private Image image;
    [SerializeField]
    private float FadeTime = 3f;

    void Start()
    {
        StartCoroutine(FadeIn(image, FadeTime));
    }

    public IEnumerator FadeIn(Image image, float timetoFade)
    {
        Color startColor = image.color;
        Color endColor = new Color(startColor.r, startColor.g, startColor.b, 1f); // fully visible

        // Start transparent
        startColor.a = 0f;
        image.color = startColor;

        float timer = 0f;
        while (timer < timetoFade)
        {
            timer += Time.deltaTime;
            image.color = Color.Lerp(startColor, endColor, timer / timetoFade);
            yield return null;
        }

        image.color = endColor;
    }
}