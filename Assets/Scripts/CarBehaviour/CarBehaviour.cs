using Scripts.General.StateMachine;
using Zenject;
using Scripts.CarMotion;
using UnityEngine;
using Scripts.UI;
using Scripts.General;

namespace Scripts.CarBehaviour
{
    public class CarBehaviour : StateController
    {
        private CollisionDetector _collisionDetector;
        private CarMotionController _motionController;
        private UIInteractor _uiInteractor;

        private EventBus _eventBus;

        [Inject]
        private void Construct(UIInteractor uiInteractor, EventBus eventBus)
        {
            _motionController = GetComponent<CarMotionController>();
            _collisionDetector = GetComponent<CollisionDetector>();
            _uiInteractor = uiInteractor;

            _states = new(){
                new DrivingState(this, _motionController),
                new CrashedState(this, _uiInteractor)
            };

            _eventBus = eventBus;

        }

        private void OnEnable()
        {
            _collisionDetector.OnCollided += Collide;
            _eventBus.Subscribe(GameEventType.Restarted, ChangeState<DrivingState>);
        }

        private void Start() => ChangeState<DrivingState>();


        private void Collide(Collision collision)
        {
            Debug.Log("Collided " + collision.transform.GetComponent<Obstacle>());
            if (collision.transform.CompareTag("Punchable"))
            {
                collision.transform.GetComponent<ArcadePuncher>().Punch(collision, _motionController.Speed);
            }
            else if (collision.transform.GetComponent<Obstacle>() != null)
            {
                Debug.Log("Crashed!");
                ChangeState<CrashedState>();
                // collision.transform.GetComponent<Obstacle>();
            }
        }

        private void OnDisable()
        {
            _collisionDetector.OnCollided -= Collide;
            _eventBus.UnSubscribe(GameEventType.Restarted, ChangeState<DrivingState>);
        }

    }
}
