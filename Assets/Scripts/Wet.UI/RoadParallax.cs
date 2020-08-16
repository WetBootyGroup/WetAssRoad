using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Wet.UI
{
    public class RoadParallax : MonoBehaviour
    {
        public float intensity = 1f;
        public float magnitudeMax = 5f;
        public Transform movingReference;

        private Transform _localTransform;
        private float _xOffset;

        private void Start()
        {
            Assert.IsNotNull(movingReference, "Missing moving reference in " + name);
            _localTransform = transform;
            _xOffset = movingReference.position.x;
        }

        private void Update()
        {
            float val = Mathf.Clamp(
                (movingReference.position.x - _xOffset) * intensity,
                -magnitudeMax,
                magnitudeMax);
            Vector3 localPosition = _localTransform.localPosition;
            localPosition = new Vector3(
                 val,
                localPosition.y,
                 localPosition.z);
            _localTransform.localPosition = localPosition;
        }
    }
}
