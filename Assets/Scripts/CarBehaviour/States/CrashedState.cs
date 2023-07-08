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
        public CrashedState(StateController controller, UIInteractor interactor) : base(controller)
        {
            _interactor = interactor;
        }

        public override void Enter()
        {
            Debug.Log("Crashed");
            _interactor.ShowDialogue<LoseDialogue>();
        }
    }

}

