using NaughtyAttributes;
using Scripts.Effects;
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
        [SerializeField, ReadOnly, BoxGroup("Info")] private float _speed;


        private TireTrailController _tireTrail;

        [Inject]
        private void Construct(InputHandler inputHandler, TireTrailController tireTrail)
        {
            _inputHandler = inputHandler;
            _currentTraction = _config.DefaultTraction;
            _tireTrail = tireTrail;
            _speed = _config.Speed;
        }

        private void OnEnable()
        {
            _inputHandler.HandbrakeStarted += SetHandbrake;
            _inputHandler.HandbrakeFinished += ReleaseHandbrake;
        }

        private void Update()
        {
            Drive(_inputHandler.Input);
        }

        private void Drive(Vector2 direction)
        {

            _moveForce += _speed * Time.deltaTime * transform.forward;
            transform.position += _moveForce * Time.deltaTime;

            float steerInput = direction.x;
            transform.Rotate(_config.RotationSpeed * _moveForce.magnitude * steerInput * Time.deltaTime * Vector3.up);

            _moveForce *= _config.Drag;
            _moveForce = Vector3.ClampMagnitude(_moveForce, _speed);

            _moveForce = Vector3.Lerp(_moveForce.normalized, transform.forward, _currentTraction * Time.deltaTime) * _moveForce.magnitude;

            if (_inputHandler.IsHandbraking)
                _currentTraction = Mathf.Lerp(_currentTraction, _config.DriftingTraction, Time.deltaTime * _config.StateChangingSmoothness);

            else
                _currentTraction = Mathf.Lerp(_currentTraction, _config.DefaultTraction, Time.deltaTime * _config.StateChangingSmoothness);
        }

        private void SetHandbrake()
        {
            _isHandbraking = true;
            _tireTrail.EnableTrail();
            if (_config.AffectSpeedOnHandbreaking)
                _speed = _config.SpeedOnHandbraking;
        }

        private void ReleaseHandbrake()
        {
            _isHandbraking = false;
            _speed = _config.Speed;
            _tireTrail.DisableTrail();

        }

        private void OnDisable()
        {
            _inputHandler.HandbrakeStarted -= SetHandbrake;
            _inputHandler.HandbrakeFinished -= ReleaseHandbrake;


        }

    }

}

