using System.Collections.Generic;
using UnityEngine;

namespace Scripts.Effects
{
    public class TireTrailController : MonoBehaviour
    {
        private List<TrailRenderer> _trailRenderers;

        private void Awake()
        {
            _trailRenderers = new(GetComponentsInChildren<TrailRenderer>());
            _trailRenderers.ForEach((renderer) => renderer.emitting = false);
        }

        public void EnableTrail()
        {
            _trailRenderers.ForEach((renderer) => renderer.emitting = true);
        }

        public void DisableTrail()
        {
            _trailRenderers.ForEach((renderer) => renderer.emitting = false);
        }

    }

}

