using NaughtyAttributes;
using System;
using UnityEngine;

namespace Scripts.CarMotion
{
    [Serializable]
    [CreateAssetMenu(menuName = "Config/CarMotion", fileName = "CarMotionConfig")]
    public class CarMotionConfig : ScriptableObject
    {
        [Min(0), BoxGroup("Movement")] public float Speed;

        [BoxGroup("Movement"), Range(0, 5)] public float RotationSpeed;
        [BoxGroup("Movement"), Range(0, 20)] public float DefaultTraction;

        [BoxGroup("Drifting"), Range(0,5)] public float StateChangingSmoothness;
        [BoxGroup("Drifting"), Range(0, 2)] public float Drag;
        [BoxGroup("Drifting"), Range(0, 20), Tooltip("Traction when handbraking")] public float DriftingTraction;
        [BoxGroup("Drifting"), ReadOnly, Header("Not implemented")] public bool ActivateDriftAtAngle;
        [BoxGroup("Drifting"), Range(0, 90), ShowIf("ActivateDriftAtAngle")] public float DriftActivationAngle;

    }

}

