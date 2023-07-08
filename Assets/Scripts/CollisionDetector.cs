using System;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public event Action<Collision> OnCollided;

    private void OnCollisionEnter(Collision collision) => OnCollided?.Invoke(collision);
}
