using NaughtyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.CarMotion 
{
    [Serializable]
    [CreateAssetMenu(menuName = "Config/CarMotion", fileName = "CarMotionConfig")]
    public class CarMotionConfig : ScriptableObject
    {
        [Min(0), BoxGroup("Speed")] public float Speed;

        [BoxGroup("Rotation"), DisableIf("_autoCalculateRotaionSpeed"), Min(0)] public float RotationSpeed;
        [SerializeField, BoxGroup("Rotation")] private bool _autoCalculateRotaionSpeed;
        [SerializeField, BoxGroup("Rotation"), ShowIf("_autoCalculateRotaionSpeed")] private float _autoCalculationCoeficient;

        [Range(0, 1), BoxGroup("Drifting")] public float DriftEffectForce;
        [Range(0, 5), BoxGroup("Drifting")] public float DriftEffectDecrementTime;
        [Range(0, 180), BoxGroup("Drifting")] public float DriftActivationAngle;

        private void OnValidate()
        {
            if (_autoCalculateRotaionSpeed)
            {
                RotationSpeed = Speed * _autoCalculationCoeficient;
            }
        }
    }

}

