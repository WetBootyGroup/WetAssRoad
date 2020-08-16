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
        private Vector2 _noiseScale = new Vector2(2f, 3f);
        private Vector2 _shakeSpeed = Vector2.one;
        private float _sqrShakeSpeed = 1f;
        private Vector3 _shakeOffset = Vector3.zero;

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
            float sampleX = _noiseScale.x * Mathf.PerlinNoise(Time.time * _shakeSpeed.x, 0);
            float sampleY = _noiseScale.y * Mathf.PerlinNoise(0, Time.time * _shakeSpeed.y);
            Vector3 shakeVector = new Vector3(sampleX, sampleY) - _shakeOffset;
            Vector3 cameraResting = target.position + cameraOffset;
            Vector3 cameraRelativeTarget = target.position + cameraOffset + shakeVector + _shakeOffset;
            
            switch (_shakeState)
            {
                case ShakeState.NoShake:
                    transform.position = cameraResting;
                    break;
                
                case ShakeState.GoToNoise:
                    transform.position = Vector3.MoveTowards(
                        transform.position, 
                        cameraRelativeTarget, transitionSpeed);

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

                    if (_sqrShakeSpeed > Vector3.SqrMagnitude(transform.position))
                    {
                        _shakeState = ShakeState.NoShake;
                    }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void Shake(ShakeArgument shakeArgument)
        {
            if (_shakeState == ShakeState.NoShake || _shakeState == ShakeState.GoToNoShake)
            {
                _shakeEndTime = Time.time;
            }

            _shakeEndTime = Time.time + Random.Range(
                shakeArgument.minDuration,
                shakeArgument.maxDuration);
            _shakeState = ShakeState.GoToNoise; // race condition vs Update solution
            _noiseScale = shakeArgument.noiseScale;
            _shakeSpeed = shakeArgument.shakeSpeed;
            // _offset = shakeArgument.noiseScale / 2;
            _shakeOffset = new Vector3(1f, 1f); // todo: improve offset
        }
    }
}
