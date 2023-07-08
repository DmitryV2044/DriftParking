using System.Collections.Generic;
using UnityEngine;

namespace Scripts.General.StateMachine
{
    public abstract class StateController : MonoBehaviour
    {
        protected List<State> _states;
        protected State _currentState;

        protected void Update() => _currentState?.Update();

        public void ChangeState<S>() where S : State
        {
            _currentState?.Exit();
            _currentState = _states.Find(s => s is S);
            _currentState?.Enter();
        }
    }

}
