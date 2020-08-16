using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    public class CameraEffects : MonoBehaviour
    {
        public float transitionSpeed = 1f;
        
        private ShakeState _shakeState;
        private float _shakeEndTime = 0f;
        private Vector2 _noiseScale = new Vector2(2f, 3f);
        private Vector2 _shakeSpeed = Vector2.one;
        private float _sqrShakeSpeed = 1f;
        private Vector3 _offset = Vector3.zero;

        private enum ShakeState
        {
            NoShake,
            GoToNoise,
            NoiseShake,
            GoToNoShake
        }

        private void Start()
        {

            _sqrShakeSpeed = transitionSpeed * transitionSpeed;
        }

        void Update()
        {
            // optimizable
            float sampleX = _noiseScale.x * Mathf.PerlinNoise(Time.time * _shakeSpeed.x, 0);
            float sampleY = _noiseScale.y * Mathf.PerlinNoise(0, Time.time * _shakeSpeed.y);
            Vector3 shakeVector = new Vector3(sampleX, sampleY) - _offset;
            
            switch (_shakeState)
            {
                case ShakeState.NoShake:
                    break;
                
                case ShakeState.GoToNoise:
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, shakeVector, transitionSpeed);

                    if (_sqrShakeSpeed > Vector3.SqrMagnitude(transform.localPosition - shakeVector))
                    {
                        _shakeState = ShakeState.NoiseShake;
                    }
                    break;
                
                case ShakeState.NoiseShake:
                    transform.localPosition = shakeVector - _offset;
                    if (Time.time > _shakeEndTime)
                    {
                        _shakeState = ShakeState.GoToNoShake;
                    }
                    break;
                
                case ShakeState.GoToNoShake:
                    transform.localPosition = Vector3.MoveTowards(transform.localPosition, Vector3.zero, transitionSpeed);

                    if (_sqrShakeSpeed > Vector3.SqrMagnitude(transform.localPosition))
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
            _offset = new Vector3(1f, 1f); // todo: improve offset
        }
    }
}
