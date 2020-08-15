using UnityEngine;

namespace Effects
{
    public class CameraEffects : MonoBehaviour
    {
        private bool _isShaking = false;
        private float _shakeEndTime = 0f;
        private Vector2 noiseScale = new Vector2(2f, 3f);
    
        void Update()
        {
            if (_isShaking)
            {
                float sampleX = noiseScale.x * Mathf.PerlinNoise(Time.time, 0);
                float sampleY = noiseScale.y * Mathf.PerlinNoise(0, Time.time);
                
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
            noiseScale = shakeArgument.noiseScale;
        }
    }
}
