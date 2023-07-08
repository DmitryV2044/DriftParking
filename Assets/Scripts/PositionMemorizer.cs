using System.Collections;
using System.Collections.Generic;
using Scripts.General;
using UnityEngine;
using Zenject;

public class PositionMemorizer : MonoBehaviour
{
    private Vector3 _position;
    private Vector3 _rotation;
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus)
    {
        _eventBus = eventBus;
    }

    private void Awake()
    {
        _position = transform.position;
        _rotation = transform.rotation.eulerAngles;
        _eventBus.Subscribe(GameEventType.Restarted, RestorePosition);
    }

    private void RestorePosition()
    {
        transform.position = _position;
        transform.rotation = Quaternion.Euler(_rotation);
    }

    private void OnDestroy()
    {
        _eventBus.UnSubscribe(GameEventType.Restarted, RestorePosition);
    }


}
