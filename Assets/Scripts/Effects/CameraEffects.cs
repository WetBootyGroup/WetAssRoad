using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Effects
{
    public class CameraEffects : MonoBehaviour
    {
        public float transitionSpeed = 1f;

        [Header("Required")] public Transform target;
        public Vector3 cameraOffset = Vector3.zero;
        
        private ShakeState _shakeState;
        private float _shakeEndTime = 0f;
        private float _sqrShakeSpeed = 1f;

        private ShakeArgument _shakeArgument = new ShakeArgument();
        private Vector3 _shakeOffset;

        private enum ShakeState
        {
            NoShake,
            GoToNoise,
            NoiseShake,
            GoToNoShake
        }

        private void Start()
        {
            Assert.IsNotNull(target, "No target in CameraEffect");
            _sqrShakeSpeed = transitionSpeed * transitionSpeed;
        }

        void Update()
        {
            // optimizable
            float sampleY = _shakeArgument.bounceMax * Mathf.PerlinNoise(0, Time.time * _shakeArgument.bounceSpeed);
            Vector3 shakeVector = (sampleY * Vector3.up) - _shakeOffset;
            Vector3 cameraResting = target.position + cameraOffset;
            Vector3 cameraRelativeTarget = cameraResting + shakeVector;
            
            switch (_shakeState)
            {
                case ShakeState.NoShake:
                    transform.position = cameraResting;
                    break;
                
                case ShakeState.GoToNoise:
                    transform.position = Vector3.MoveTowards(
                        transform.position, 
                        cameraRelativeTarget, transitionSpeed);
                    Debug.Log(_shakeArgument.bounceSpeed);

                    if (_sqrShakeSpeed > Vector3.SqrMagnitude(transform.position - cameraRelativeTarget))
                    {
                        _shakeState = ShakeState.NoiseShake;
                    }
                    break;
                
                case ShakeState.NoiseShake:
                    transform.position = cameraRelativeTarget;
                    if (Time.time > _shakeEndTime)
                    {
                        _shakeState = ShakeState.GoToNoShake;
                    }
                    break;
                
                case ShakeState.GoToNoShake:
                    transform.position = Vector3.MoveTowards(transform.position, cameraResting, transitionSpeed);

                    if (_sqrShakeSpeed > Vector3.SqrMagnitude(transform.position - cameraResting))
                    {
                        _shakeState = ShakeState.NoShake;
                    }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // bounce up and down and rotate on z axis
        public void Shake(ShakeArgument argument)
        {
            if (_shakeState == ShakeState.NoShake || _shakeState == ShakeState.GoToNoShake)
            {
                _shakeEndTime = Time.time;
            }

            Debug.Log(argument.bounceSpeed);
            _shakeArgument = argument;
            _shakeEndTime = Time.time + Random.Range(
                argument.minDuration,
                argument.maxDuration);
            _shakeState = ShakeState.GoToNoise; // race condition vs Update solution
            _shakeOffset = new Vector3(0, argument.bounceMax/4); // todo: improve offset
            _sqrShakeSpeed = argument.bounceSpeed * argument.bounceSpeed;
        }
    }
}
