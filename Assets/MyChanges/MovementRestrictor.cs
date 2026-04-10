using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Locomotion;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Movement;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Teleportation;
using UnityEngine.XR.Interaction.Toolkit.Locomotion.Turning;

public class MovementRestrictor : MonoBehaviour
{
    [SerializeField]
    private MovementDetector detector;

    private LocomotionProvider[] _providers;

    void Awake()
    {
        _providers = this.GetComponentsInChildren<LocomotionProvider>();
    }

    void Update()
    {
        foreach (var provider in _providers)
            provider.enabled = !detector.IsInsideWall;
    }
}
