using System.Collections;
using System.Linq;
using System.Collections.Generic;
using Scripts.General;
using UnityEngine;
using NaughtyAttributes;
using Zenject;

public class LevelRestarter : MonoBehaviour
{
    [SerializeField, ReadOnly] private List<IRestartable> _restartables;
    private EventBus _eventBus;

    [Inject]
    private void Construct(EventBus eventBus) => _eventBus = eventBus;

    private void Awake()
    {
        _restartables = new(FindObjectsOfType<MonoBehaviour>(true).OfType<IRestartable>());
    }
    private void OnEnable() => _eventBus.Subscribe(GameEventType.Restarted, Restart);

    private void Restart()
    {
        foreach (IRestartable r in _restartables)
            r.Restart();
    }

    private void OnDisable() => _eventBus.UnSubscribe(GameEventType.Restarted, Restart);
}
