using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healther : MonoBehaviour, IRestartable
{
    [field: SerializeField, Range(1, 10)] public int Health { get; private set; }
    private int _initialHealth;

    public event Action OnDied;

    public void TakeDamage(int damage)
    {
        Health -= damage;

        if (Health <= 0)
        {
            OnDied?.Invoke();
        }
    }

    public void Restart()
    {
        Health = _initialHealth;
    }
}
