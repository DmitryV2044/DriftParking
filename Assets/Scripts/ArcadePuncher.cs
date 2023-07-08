using UnityEngine;

public class ArcadePuncher : MonoBehaviour, IRestartable
{
    [SerializeField, Range(0, 100)] private float _forceModifier = 1f;
    private Rigidbody _rigidbody;

    private void Awake() => _rigidbody = GetComponent<Rigidbody>();

    public void Punch(Collision collision, float force) => _rigidbody.AddForce(-collision.contacts[0].normal * _forceModifier * force, ForceMode.Impulse);

    public void Restart()
    {
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
    }
}
