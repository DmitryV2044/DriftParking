using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Checkpointing
{

    public class Checkpoint : MonoBehaviour
    {
        [SerializeField] protected Vector2 _size;
        [SerializeField] protected Color _defaultColor;
        [SerializeField, Range(0, 2)] protected float _lineWidth;
        [SerializeField, Range(0, 10)] protected int _cornerSmoothness;

        protected LineRenderer _checkpointRenderer;

        private void OnValidate()
        {
            _checkpointRenderer = GetComponentInChildren<LineRenderer>();
            _checkpointRenderer.endWidth = _lineWidth;
            _checkpointRenderer.startWidth = _lineWidth;

            _checkpointRenderer.startColor = _defaultColor;
            _checkpointRenderer.endColor = _defaultColor;

            _checkpointRenderer.numCornerVertices = _cornerSmoothness;

            _checkpointRenderer.SetPositions(new Vector3[]{
                new Vector3(_size.x, _size.y),
                new Vector3(_size.x,0),
                new Vector3(0,0),
                new Vector3(0,_size.y),
            });

        }
    }

}
