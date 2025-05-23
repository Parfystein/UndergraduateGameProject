using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundSlideshow : MonoBehaviour
{
    [SerializeField] private Sprite[] backgrounds;
    [SerializeField] private Image imageA;
    [SerializeField] private Image imageB;
    [SerializeField] private float switchInterval = 5f;
    [SerializeField] private float fadeDuration = 1.5f;

    private int currentIndex = 0;
    private bool showingA = true;

    private void Start()
    {
        if (backgrounds.Length == 0) return;

        currentIndex = Random.Range(0, backgrounds.Length);

        imageA.sprite = backgrounds[currentIndex];
        imageA.color = Color.white;
        imageB.color = new Color(1, 1, 1, 0); 

        StartCoroutine(SlideshowLoop());
    }   

    private IEnumerator SlideshowLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            currentIndex = (currentIndex + 1) % backgrounds.Length;
            Image fadeOut = showingA ? imageA : imageB;
            Image fadeIn = showingA ? imageB : imageA;

            fadeIn.sprite = backgrounds[currentIndex];

            float t = 0f;
            while (t < fadeDuration)
            {
                float alpha = t / fadeDuration;
                fadeIn.color = new Color(1, 1, 1, alpha);
                fadeOut.color = new Color(1, 1, 1, 1 - alpha);
                t += Time.deltaTime;
                yield return null;
            }

            fadeIn.color = Color.white;
            fadeOut.color = new Color(1, 1, 1, 0);

            showingA = !showingA;
        }
    }
}
