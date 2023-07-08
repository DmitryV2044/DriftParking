using Scripts.General.StateMachine;
using Scripts.CarMotion;

namespace Scripts.CarBehaviour
{
    public class DrivingState : State
    {
        private CarMotionController _motionController;
        public DrivingState(StateController controller, CarMotionController motionController) : base(controller)
        {
            _motionController = motionController;
        }

        public override void Update()
        {
            _motionController.Drive();
        }
    }

}

