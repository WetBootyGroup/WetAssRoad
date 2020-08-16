using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Effects
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioObject : MonoBehaviour
    {
        private bool _audioStarted = false;
        private AudioSource _audioSource;
        private AudioArgument _audioArgument;

        void Update()
        {
            if (_audioStarted)
            {
                transform.Translate(0f, 0f, -_audioArgument.moveSpeed * Time.deltaTime);
                
                if (!_audioSource.isPlaying)
                {
                    Destroy(gameObject);
                }
            }
        }

        public void StartAudio(AudioArgument argument)
        {
            _audioSource = GetComponent<AudioSource>();
            _audioSource.volume = Random.Range(
                Math.Min(argument.minRandomVolume, argument.maxRandomVolume),
                Math.Max(argument.minRandomVolume, argument.maxRandomVolume));
            _audioSource.pitch = Random.Range(
                Math.Min(argument.minRandomPitch, argument.maxRandomPitch),
                Math.Max(argument.minRandomPitch, argument.maxRandomPitch));
            _audioArgument = argument;
            _audioSource.Play();
            _audioStarted = true;
        }
    }

    [Serializable]
    public class AudioArgument
    {
        [Range(0f,1f)]
        public float maxRandomVolume = 1f;
        [Range(0f,1f)]
        public float minRandomVolume = 1f;
        [Range(0f,2f)]
        public float maxRandomPitch = 1f;
        [Range(0f,2f)]
        public float minRandomPitch = 1f;

        [Range(0f, 100f)]
        public float moveSpeed = 5f;
    }
}
