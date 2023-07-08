using UnityEngine;

namespace Scripts.General.StateMachine
{
    public abstract class StateController : MonoBehaviour
    {
        private State _currentState;
        private State _initialState;

        protected void Start()
        {
            ChangeState(_initialState);
        }

        protected void Update()
        {
            _currentState?.Update();
        }

        public void ChangeState(State newState)
        {
            _currentState?.Exit();
            _currentState = newState;
            _currentState?.Enter();
        }
    }

}
