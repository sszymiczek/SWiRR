using System.Collections;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private CanvasGroup fadeCanvasGroup; // on your Canvas
    [SerializeField] private MovementDetector detector;

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 8f;   // how fast it fades in/out

    private bool _isFading = false;
    private Coroutine _fadeCoroutine;

    void Awake()
    {
        fadeCanvasGroup.alpha = 0f;
    }
    void Update()
    {
        if (detector.IsInsideWall && !_isFading)
        {
            SetFade(true);
        }
        else if (!detector.IsInsideWall && _isFading)
        {
            SetFade(false);
        }
    }

    private void SetFade(bool fadeOut)
    {
        _isFading = fadeOut;

        if (_fadeCoroutine != null)
            StopCoroutine(_fadeCoroutine);

        _fadeCoroutine = StartCoroutine(FadeCoroutine(fadeOut ? 1f : 0f));
    }

    private IEnumerator FadeCoroutine(float targetAlpha)
    {
        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha = Mathf.MoveTowards(
                fadeCanvasGroup.alpha,
                targetAlpha,
                fadeSpeed * Time.deltaTime
            );
            yield return null;
        }

        fadeCanvasGroup.alpha = targetAlpha;
    }
}