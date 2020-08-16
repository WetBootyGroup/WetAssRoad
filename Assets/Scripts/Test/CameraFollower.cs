using System;
using UnityEngine;
using UnityEngine.Assertions;

namespace Test
{
    public class CameraFollower : MonoBehaviour
    {
        private Transform _cameraTransform;
        
        void Start()
        {
            Assert.IsNotNull(Camera.main, "No main camera found in CameraFollower for " + name);
            _cameraTransform = Camera.main.transform;
        }

        private void Update()
        {
            transform.position = _cameraTransform.position;
        }
    }
}
