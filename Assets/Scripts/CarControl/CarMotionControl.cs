using NaughtyAttributes;
using Scripts.InputHandling;
using UnityEngine;
using Zenject;

namespace Scripts.CarMotion
{
    public class CarMotionControl : MonoBehaviour
    {
        [SerializeField, Expandable] CarMotionConfig _config;

        private InputHandler _inputHandler;
        [SerializeField, ReadOnly, BoxGroup("Info")]private Vector3 _moveForce;
        [SerializeField, ReadOnly, BoxGroup("Info")] private bool _isHandbraking;
        [SerializeField, ReadOnly, BoxGroup("Info")] private float _currentTraction;


        [Inject]
        private void Construct(InputHandler inputHandler)
        {
            _inputHandler = inputHandler;
            _currentTraction = _config.DefaultTraction;
        }

        private void OnEnable()
        {
            _inputHandler.HandbrakeStateChanged += SetHandbrake;
            //_inputHandler.OnTurnBegan += HandleTurn;
            //_inputHandler.OnTurnEnded += EndTurn;
        }

        private void Update()
        {
            Drive(_inputHandler.Input);
        }

        private void Drive(Vector2 direction)
        {

            _moveForce += _config.Speed * Time.deltaTime * transform.forward;
            transform.position += _moveForce * Time.deltaTime;

            float steerInput = direction.x;
            transform.Rotate(_config.RotationSpeed * _moveForce.magnitude * steerInput * Time.deltaTime * Vector3.up);

            _moveForce *= _config.Drag;
            _moveForce = Vector3.ClampMagnitude(_moveForce, _config.Speed);

            _moveForce = Vector3.Lerp(_moveForce.normalized, transform.forward, _currentTraction * Time.deltaTime) * _moveForce.magnitude;

            if (_isHandbraking)
                _currentTraction = Mathf.Lerp(_currentTraction, _config.DriftingTraction, Time.deltaTime * _config.StateChangingSmoothness);

            else
                _currentTraction = Mathf.Lerp(_currentTraction, _config.DefaultTraction, Time.deltaTime * _config.StateChangingSmoothness);
        }

        private void SetHandbrake(bool state)
        {
            _isHandbraking = state;

        }
        //private void HandleTurn()
        //{
        //    _initialDirection = transform.forward.normalized;
        //    _driftForceTween.Kill();
        //    _inputHandler.OnTick += CalculateRotationDelta;
        //}

        //private void CalculateRotationDelta(Vector2 direction)
        //{
        //    _rotationDelta = Vector3.Angle(_initialDirection, transform.forward);
        //    if (_rotationDelta > _config.DriftActivationAngle || _isDrifting)
        //    {
        //        _isDrifting = true;
        //        _currentDriftForce = _config.DriftEffectForce;

        //    }
        //}

        private void OnDisable()
        {
            _inputHandler.HandbrakeStateChanged -= SetHandbrake;

            //_inputHandler.OnTurnBegan -= HandleTurn;
        }

    }

}

