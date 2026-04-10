using System.Runtime.InteropServices;
using UnityEngine;

public class MovementDetector : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private Camera vrCamera;

    [SerializeField] private float buffer = 0.05f; // extra sphere offset hitting the wall

    [Header("Detection")]
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float detectionRadius = 0.12f; // head size approximation

    public bool IsInsideWall { get; private set; }

    // Update is called once per frame
    void Update()
    {
        // Offset the check slightly forward so fade triggers before full clip
        Vector3 checkPosition = vrCamera.transform.position
                              + vrCamera.transform.forward * buffer;

        IsInsideWall = Physics.CheckSphere(
            checkPosition,
            detectionRadius,
            wallLayer,
            QueryTriggerInteraction.Ignore  // ignore trigger colliders
        );
    }

    // Optional: visualize the detection sphere in editor
    void OnDrawGizmosSelected()
    {
        if (vrCamera == null) return;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(vrCamera.transform.position, detectionRadius);
    }
}
