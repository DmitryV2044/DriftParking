using NaughtyAttributes;
using Scripts.Effects;
using Scripts.InputHandling;
using UnityEngine;
using Zenject;

namespace Scripts.CarMotion
{
    public class CarMotionController : MonoBehaviour, IRestartable
    {
        [SerializeField, Expandable] CarMotionConfig _config;

        private InputHandler _inputHandler;
        [SerializeField, ReadOnly, BoxGroup("Info")] private Vector3 _moveForce;
        [SerializeField, ReadOnly, BoxGroup("Info")] private bool _isHandbraking;
        [SerializeField, ReadOnly, BoxGroup("Info")] private float _currentTraction;
        [SerializeField, ReadOnly, BoxGroup("Info")] private float _speed;
        [SerializeField, ReadOnly, BoxGroup("Info")] private float _rotationSpeed;

        private Rigidbody _rigidbody;
        private TireTrailController _tireTrail;

        public float Speed => _speed;
        public bool IsCrashed = false;

        [Inject]
        private void Construct(InputHandler inputHandler, TireTrailController tireTrail)
        {
            _inputHandler = inputHandler;
            _currentTraction = _config.DefaultTraction;
            _tireTrail = tireTrail;
            _speed = _config.Speed;
            _rotationSpeed = _config.RotationSpeed;
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void OnEnable()
        {
            _inputHandler.HandbrakeStarted += SetHandbrake;
            _inputHandler.HandbrakeFinished += ReleaseHandbrake;
        }

        public void Drive()
        {
            if (IsCrashed) return;
            Vector2 direction = _inputHandler.Input;
            _moveForce += _speed * Time.deltaTime * transform.forward;
            // _rigidbody.MovePosition(_rigidbody.position + _moveForce * Time.deltaTime);
            transform.position += _moveForce * Time.deltaTime;

            float steerInput = direction.x;
            // _rigidbody.MoveRotation(_rigidbody.rotation * Quaternion.Euler(_rotationSpeed * _moveForce.magnitude * steerInput * Time.deltaTime * Vector3.up));
            transform.Rotate(_rotationSpeed * _moveForce.magnitude * steerInput * Time.deltaTime * Vector3.up);

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
            if (_config.AffectRotationSpeedOnHandbreaking)
                _rotationSpeed = _config.RotationSpeedOnHandbraking;
        }

        private void ReleaseHandbrake()
        {
            _isHandbraking = false;
            _speed = _config.Speed;
            _rotationSpeed = _config.RotationSpeed;
            _tireTrail.DisableTrail();

        }

        private void OnDisable()
        {
            _inputHandler.HandbrakeStarted -= SetHandbrake;
            _inputHandler.HandbrakeFinished -= ReleaseHandbrake;
        }

        public void Restart()
        {
            _speed = _config.Speed;
            _rotationSpeed = _config.RotationSpeed;
            _moveForce = Vector3.zero;
            _currentTraction = _config.DefaultTraction;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

}

