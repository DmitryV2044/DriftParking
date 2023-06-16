using System;
using UnityEngine;
using Zenject;
using static UnityEngine.InputSystem.InputAction;

namespace Scripts.InputHandling
{
    public class InputHandler : ITickable, IInitializable, IDisposable
    {
        public event Action<Vector2> OnTick;
        public event Action OnTurnBegan;
        public event Action OnTurnEnded;

        private PlayerControls _controls;
        private Vector2 _input;

        public InputHandler() { }

        public void Initialize()
        {
            _controls = new PlayerControls();
            _input = Vector2.zero;
            _controls.Enable();
            _controls.Driving.Rotation.performed += StartTurn;
            _controls.Driving.Rotation.canceled += EndTurn;
        }

        public void Tick()
        {
            _input = Vector2.right * _controls.Driving.Rotation.ReadValue<float>()
                + Vector2.up * _controls.Driving.Speed.ReadValue<float>();

            _input.Normalize();
            OnTick?.Invoke(_input);
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

            _controls.Disable();
            _controls.Dispose();
        }

    }

}

