using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingScreen : MonoBehaviour
{

    private static LoadingScreen instance;

    public static LoadingScreen GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        instance = this;
    }


    public GameObject visuals;

    private float fadeDuration = 0.5f; // Duration of the fade-out in seconds

    private Graphic[] graphics; // Store references to all child Graphics (UI components)

    private void Start()
    {
        // Get all child Graphics components (UI components like Image and Text)
        graphics = GetComponentsInChildren<Graphic>();
    }

    public void StartFadeOutAndDisable()
    {
        StartCoroutine(FadeOutAndDisableCoroutine());
    }

    private IEnumerator FadeOutAndDisableCoroutine()
    {
        float elapsedTime = 0f;
        Color[] originalColors = new Color[graphics.Length];

        // Store the original colors of child Graphics
        for (int i = 0; i < graphics.Length; i++)
        {
            originalColors[i] = graphics[i].color;
        }

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);

            // Apply the fade effect to each child Graphic
            for (int i = 0; i < graphics.Length; i++)
            {
                graphics[i].color = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, alpha);
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure all child Graphics are fully faded out
        for (int i = 0; i < graphics.Length; i++)
        {
            graphics[i].color = new Color(originalColors[i].r, originalColors[i].g, originalColors[i].b, 0f);
        }

        // Disable the GameObject after fading out
        if (visuals.activeSelf)
        {
            visuals.SetActive(false);
            PlayerMovement.GetInstance().enableMovement = true;
        }
    }
}
