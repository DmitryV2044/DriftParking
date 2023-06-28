using NaughtyAttributes;
using System;
using UnityEngine;

namespace Scripts.CarMotion
{
    [Serializable]
    [CreateAssetMenu(menuName = "Config/CarMotion", fileName = "CarMotionConfig")]
    public class CarMotionConfig : ScriptableObject
    {
        [field: SerializeField, Min(0), BoxGroup("Movement")] public float Speed { get; private set; }
        [field: SerializeField, BoxGroup("Movement")] public bool AffectSpeedOnHandbreaking { get; private set; }
        [field: SerializeField, Min(0), BoxGroup("Movement"), EnableIf("AffectSpeedOnHandbreaking")] public float SpeedOnHandbraking { get; private set; }


        [field: SerializeField, BoxGroup("Movement"), Range(0, 5)] public float RotationSpeed { get; private set; }
        [field: SerializeField, BoxGroup("Movement"), Range(0, 20)] public float DefaultTraction { get; private set; }

        [field: SerializeField, BoxGroup("Drifting"), Range(0,5)] public float StateChangingSmoothness { get; private set; }
        [field: SerializeField, BoxGroup("Drifting"), Range(0, 2)] public float Drag { get; private set; }
        [field: SerializeField, BoxGroup("Drifting"), Range(0, 20), Tooltip("Traction when handbraking")] public float DriftingTraction { get; private set; }
        [field: SerializeField, BoxGroup("Drifting"), ReadOnly, Header("Not implemented")] public bool ActivateDriftAtAngle { get; private set; }
        [field: SerializeField, BoxGroup("Drifting"), Range(0, 90), ShowIf("ActivateDriftAtAngle")] public float DriftActivationAngle { get; private set; }

    }

}

