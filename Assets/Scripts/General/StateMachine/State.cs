namespace Scripts.General.StateMachine
{
    public abstract class State
    {
        protected StateController _controller;
        public State(StateController controller) => _controller = controller;
        public virtual void Enter() { }
        public virtual void Update() { }
        public virtual void Exit() { }
    }
}

