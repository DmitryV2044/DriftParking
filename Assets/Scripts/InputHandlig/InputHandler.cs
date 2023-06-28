using System;
using UnityEngine;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace Scripts.InputHandling
{
    public class InputHandler : ITickable, IInitializable, IDisposable
    {
        public Vector2 Input { get; private set; }
        public bool IsHandbraking { get; private set; }

        public event Action<Vector2> OnTick;
        public event Action OnTurnBegan;
        public event Action OnTurnEnded;
        public event Action HandbrakeStarted;
        public event Action HandbrakeFinished;

        private PlayerControls _controls;

        public InputHandler() { }

        public void Initialize()
        {
            _controls = new PlayerControls();
            Input = Vector2.zero;
            _controls.Enable();
            _controls.Driving.Rotation.performed += StartTurn;
            _controls.Driving.Rotation.canceled += EndTurn;
            _controls.Driving.HandBrake.performed += HandleHandbrake;
            _controls.Driving.HandBrake.canceled += HandleHandbrake;
        }

        public void Tick()
        {
            Input = Vector2.right * _controls.Driving.Rotation.ReadValue<float>()
                + Vector2.up * _controls.Driving.Speed.ReadValue<float>();

            Debug.Log(IsHandbraking);
            Input.Normalize();
            OnTick?.Invoke(Input);
        }

        private void HandleHandbrake(CallbackContext context)
        {
            IsHandbraking = context.ReadValue<float>() > 0;
            if(IsHandbraking)
                HandbrakeStarted?.Invoke();
            else
                HandbrakeFinished?.Invoke();
        }

        private void StartTurn(CallbackContext context) 
        {
            OnTurnBegan?.Invoke();
        }

        private void EndTurn(CallbackContext context)
        {
            OnTurnEnded?.Invoke();
        }



        public void Dispose()
        {
            _controls.Driving.Rotation.performed -= StartTurn;
            _controls.Driving.Rotation.canceled -= EndTurn;
            _controls.Driving.HandBrake.performed -= HandleHandbrake;
            _controls.Driving.HandBrake.canceled -= HandleHandbrake;


            _controls.Disable();
            _controls.Dispose();
        }

    }

}

