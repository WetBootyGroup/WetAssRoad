using UnityEngine;

namespace Effects
{
    public class CameraEffects : MonoBehaviour
    {
        private ShakeState _shakeState;
        private bool _isShaking = false;
        private float _shakeEndTime = 0f;
        private Vector2 _noiseScale = new Vector2(2f, 3f);
        private Vector2 _shakeSpeed = Vector2.one;

        private enum ShakeState
        {
            NoShake,
            GoToNoise,
            NoiseShake,
            GoToNoShake
        }

        void Update()
        {
            if (_isShaking)
            {
                float sampleX = _noiseScale.x * Mathf.PerlinNoise(Time.time * _shakeSpeed.x, 0);
                float sampleY = _noiseScale.y * Mathf.PerlinNoise(0, Time.time * _shakeSpeed.y);
                
                transform.position = new Vector3(sampleX, sampleY);
                
                if (Time.time > _shakeEndTime)
                {
                    transform.localPosition = Vector3.zero;
                    _isShaking = false;
                }
            }
        }

        public void Shake(ShakeArgument shakeArgument)
        {
            if (!_isShaking)
            {
                _shakeEndTime = Time.time;
            }

            _shakeEndTime = Time.time +Random.Range(
                                shakeArgument.minDuration,
                                shakeArgument.maxDuration);
            _isShaking = true; // race condition vs Update solution
            _noiseScale = shakeArgument.noiseScale;
            _shakeSpeed = shakeArgument.shakeSpeed;
        }
    }
}
