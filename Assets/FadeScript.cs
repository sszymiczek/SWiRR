using System.Collections;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Camera vrCamera;
    [SerializeField] private CanvasGroup fadeCanvasGroup; // on your Canvas

    [Header("Detection")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float detectionRadius = 0.12f; // head size approximation

    [Header("Fade Settings")]
    [SerializeField] private float fadeSpeed = 8f;   // how fast it fades in/out
    [SerializeField] private float fadeBuffer = 0.05f; // extra sphere offset before camera center hits wall

    private bool _isFading = false;
    private Coroutine _fadeCoroutine;

    void Awake()
    {
        fadeCanvasGroup.alpha = 0f;
    }
    void Update()
    {
        // Offset the check slightly forward so fade triggers before full clip
        Vector3 checkPosition = vrCamera.transform.position
                              + vrCamera.transform.forward * fadeBuffer;

        bool headInsideWall = Physics.CheckSphere(
            checkPosition,
            detectionRadius,
            wallLayer,
            QueryTriggerInteraction.Ignore  // ignore trigger colliders
        );

        if (headInsideWall && !_isFading)
        {
            SetFade(true);
        }
        else if (!headInsideWall && _isFading)
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

    // Optional: visualize the detection sphere in editor
    void OnDrawGizmosSelected()
    {
        if (vrCamera == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(vrCamera.transform.position, detectionRadius);
    }
}