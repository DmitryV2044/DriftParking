using DG.Tweening;
using NaughtyAttributes;
using Scripts.InputHandling;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace Scripts.CarMotion
{
    public class CarMotionControl : MonoBehaviour
    {
        [SerializeField, Expandable] CarMotionConfig _config;
        [SerializeField, BoxGroup("Speed")] private float _speed;

        [SerializeField, ReadOnly, BoxGroup("Info")] private float _rotationDelta;
        [SerializeField, ReadOnly, BoxGroup("Info")] private Vector3 _dirftAffectionVelocity;
        [SerializeField, ReadOnly, BoxGroup("Info")] private Vector3 _initialDirection;
        [SerializeField, ReadOnly, BoxGroup("Info")] private float _currentDriftForce;
        [SerializeField, ReadOnly, BoxGroup("Info")] private bool _isDrifting = false;

        private InputHandler _inputHandler;
        private Tween _driftForceTween;

        [Inject]
        private void Construct(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
        }

        void OnEnable()
        {
            _inputHandler.OnTick += Drive;
            _inputHandler.OnTurnBegan += Turn;
            _inputHandler.OnTurnEnded += EndTurn;
        }

        private void Drive(Vector2 direction)
        {
            
            transform.position += _speed * Time.deltaTime * 
                (_dirftAffectionVelocity + transform.forward.normalized * (1-_currentDriftForce)).normalized;
            Debug.Log(_dirftAffectionVelocity);
            _dirftAffectionVelocity = _initialDirection.normalized * _currentDriftForce * transform.right.normalized.x;

            transform.Rotate(Vector3.up, _config.RotationSpeed * direction.x * Time.deltaTime);
        }

        private void Turn()
        {
            _initialDirection = transform.forward.normalized;
            _driftForceTween.Kill();
            _inputHandler.OnTick += HandleTurn;
            _currentDriftForce = _config.DriftEffectForce;
        }

        private void HandleTurn(Vector2 direction)
        {
            _rotationDelta = Vector3.Angle(_initialDirection, transform.forward);
            if (_rotationDelta > _config.DriftActivationAngle || _isDrifting)
            {
                _isDrifting = true;
                //_dirftAffectionVelocity = (_initialDirection.normalized * _driftEffectForce).normalized;

            }
        }

        private void EndTurn()
        {
            _inputHandler.OnTick -= HandleTurn;
            _rotationDelta = 0;
            _isDrifting = false;
            _driftForceTween = DOTween.To(() => _currentDriftForce, (float x) => _currentDriftForce = x, 0, _config.DriftEffectDecrementTime);
            _dirftAffectionVelocity = Vector2.zero;
        }

        private void OnDisable()
        {
            _inputHandler.OnTurnBegan -= Turn;
            _inputHandler.OnTurnEnded -= EndTurn;
            _inputHandler.OnTick -= Drive;
        }

    }

}

