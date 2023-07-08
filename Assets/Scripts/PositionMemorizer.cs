using UnityEngine;

public class PositionMemorizer : MonoBehaviour, IRestartable
{
    private Vector3 _position;
    private Vector3 _rotation;

    private void Awake()
    {
        _position = transform.position;
        _rotation = transform.rotation.eulerAngles;
    }

    public void Restart() => RestorePosition();

    private void RestorePosition()
    {
        transform.position = _position;
        transform.rotation = Quaternion.Euler(_rotation);
    }

}
