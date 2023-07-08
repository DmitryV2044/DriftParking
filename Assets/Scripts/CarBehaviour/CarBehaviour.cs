using Scripts.General.StateMachine;
using Zenject;
using Scripts.CarMotion;
using UnityEngine;
using Scripts.UI;

namespace Scripts.CarBehaviour
{
    public class CarBehaviour : StateController
    {
        private CollisionDetector _collisionDetector;
        private CarMotionController _motionController;
        private UIInteractor _uiInteractor;

        [Inject]
        private void Construct(UIInteractor uiInteractor)
        {
            _motionController = GetComponent<CarMotionController>();
            _collisionDetector = GetComponent<CollisionDetector>();
            _uiInteractor = uiInteractor;

            _states = new(){
                new DrivingState(this, _motionController),
                new CrashedState(this, _uiInteractor)
            };

        }

        private void OnEnable()
        {
            _collisionDetector.OnCollided += Collide;
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
        }

    }
}
