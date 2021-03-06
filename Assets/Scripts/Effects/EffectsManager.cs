﻿using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Effects
{
    public class EffectsManager : MonoBehaviour
    {
        [Tooltip("Count when no count applied in argument")]
        public int noCountArgMax = 5;

        [Header("Required")] 
        public EffectsManagerLocator locator;
        public GameObject puddleEffectPrefab;
        public GameObject catAudioPrefab;
        public GameObject puddleAudioPrefab;
        
        [Header("Arguments")]
        public AudioArgument catAudioArgument;
        public AudioArgument puddleAudioArgument;

        [Header("Optional arguments")] 
        public ShakeArgument shakeArgument;

        public PuddleArgument puddleArgument;

        private Transform _cameraTransform;
        private CameraEffects _cameraEffects;

        private void Awake()
        {
            if (Camera.main != null)
            {
                _cameraTransform = Camera.main.transform;
                _cameraEffects = _cameraTransform.GetComponent<CameraEffects>();
            }
            else
            {
                Debug.LogError("No camera found in effects manager");
            }
            
            Assert.IsNotNull(catAudioPrefab, "Cat Audio Argument is null in " + name);

            locator.SetEffectsManager(this);
        }

        private void OnDestroy()
        {
            locator.SetEffectsManager(null);
        }

        public void ProducePuddleEffect(PuddleArgument argument)
        {
            argument.effectsManager = this;
            
            if (argument.Count < 0)
            {
                argument.Count = Random.Range(1, noCountArgMax + 1);
            }

            AudioObject audioObject = Instantiate(puddleAudioPrefab)
                .GetComponent<AudioObject>();
            audioObject.StartAudio(catAudioArgument);
            
            for (int i = 0; i < argument.Count; i++)
            {
                PuddleParticle script = Instantiate(puddleEffectPrefab, _cameraTransform.position,
                        _cameraTransform.rotation, _cameraTransform)
                    .GetComponent<PuddleParticle>();
                script.ApplyEffect(argument);
                script.StartEffect();
            }
        }

        public void ProducePuddleEffect()
        {
            ProducePuddleEffect(puddleArgument);
        }

        public void ProduceShakeEffect(ShakeArgument argument)
        {
            _cameraEffects.Shake(argument);
        }

        public void ProduceShakeEffect()
        {
            _cameraEffects.Shake(shakeArgument);
        }

        public void ProduceCatSound(Transform source, AudioArgument argument)
        {
            AudioObject audioObject = Instantiate(catAudioPrefab, source.position, source.rotation)
                .GetComponent<AudioObject>();
            audioObject.StartAudio(argument);
        }

        public void ProduceCatSound(Transform source)
        {
            ProduceCatSound(source, catAudioArgument);
        }
    }

    // better used as struct
    [Serializable]
    public class PuddleArgument
    {
        // todo: if need arguments to change effects put it here
        // If count = 0, the effects manager decides the number of splats randomly
        public int Count = -1;
        
        // Max duration of splat in seconds
        public float maxDuration = 10.0f;
        
        // Max duration of splat in seconds
        public float minDuration = 5.0f;
        
        // Do not touch
        [HideInInspector]
        public EffectsManager effectsManager = null;
    }
    
    // better used as a struct
    [Serializable]
    public class ShakeArgument
    {
        // Min duration of shake
        public float minDuration = 1.0f;
        
        // Max duration of shake
        public float maxDuration = 5.0f;

        [Tooltip("Vertical displacement speed")]
        public float bounceSpeed = 5.0f;

        [Tooltip("Vertical displacement maximum")]
        public float bounceMax = 0.25f;

        [Tooltip("The max angular change via noise")]
        public float rotationSpeed = 1.0f;

        [Tooltip("The max rotation magnitude around the z-axis. Don't do max please")]
        [Range(0f,179f)]
        public float rotationMax = 15f;
    }
}
