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

        private RectTransform _localTransform;
        private float _xOffset;

        private void Start()
        {
            Assert.IsNotNull(movingReference, "Missing moving reference in " + name);
            _localTransform = GetComponent<RectTransform>();
            _xOffset = movingReference.position.x;
        }

        private void Update()
        {
            float val = Mathf.Clamp(
                (movingReference.position.x - _xOffset) * intensity,
                -magnitudeMax,
                magnitudeMax);
            _localTransform.anchoredPosition = new Vector2(
                 val,
                0);
        }
    }
}
