using Scripts.General.StateMachine;
using Scripts.CarMotion;
using Scripts.UI;
using Scripts.UI.Dialogues;
using UnityEngine;

namespace Scripts.CarBehaviour
{
    public class CrashedState : State
    {
        private CarMotionController _motionController;
        private UIInteractor _interactor;
        public CrashedState(StateController controller, UIInteractor interactor, CarMotionController carController) : base(controller)
        {
            _interactor = interactor;
            _motionController = carController;
        }

        public override void Enter()
        {
            _motionController.IsCrashed = true;
            _interactor.ShowDialogue<LoseDialogue>();
        }

        public override void Exit()
        {
            _motionController.IsCrashed = false;

        }
    }

}

