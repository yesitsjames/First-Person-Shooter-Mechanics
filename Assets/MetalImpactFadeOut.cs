using UnityEngine;
using System.Collections;

public class FadeOutAndDestroy : MonoBehaviour
{
    public float lifeTime = 3f;      // Time before fading starts
    public float fadeDuration = 1f;  // How long fade takes

    private Renderer objectRenderer;
    private Color originalColor;

    void Start()
    {
        objectRenderer = GetComponent<Renderer>();

        if (objectRenderer != null)
        {
            originalColor = objectRenderer.material.color;
            StartCoroutine(FadeAndDestroy());
        }
        else
        {
            Debug.LogWarning("No Renderer found on object!");
        }
    }

    IEnumerator FadeAndDestroy()
    {
        // Wait before starting fade
        yield return new WaitForSeconds(lifeTime);

        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            objectRenderer.material.color = new Color(
                originalColor.r,
                originalColor.g,
                originalColor.b,
                alpha
            );

            yield return null;
        }

        Destroy(gameObject);
    }
}