using System;
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
        public GameObject puddleEffectPrefab;

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
        }

        public void ProducePuddleEffect(PuddleArgument argument)
        {
            argument.effectsManager = this;
            
            if (argument.Count < 0)
            {
                argument.Count = Random.Range(1, noCountArgMax + 1);
            }

            for (int i = 0; i < argument.Count; i++)
            {
                PuddleParticle script = Instantiate(puddleEffectPrefab, _cameraTransform.position,
                        _cameraTransform.rotation, _cameraTransform)
                    .GetComponent<PuddleParticle>();
                script.ApplyEffect(argument);
                script.StartEffect();
            }
        }

        public void ProduceShakeEffect(ShakeArgument shakeArgument)
        {
            // todo: do shake effect
            _cameraEffects.Shake(new ShakeArgument());
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
        
        // Perlin noise scale
        public Vector2 noiseScale = new Vector2(3f, 5f);
        
        // Shake speed
        public Vector2 shakeSpeed = new Vector2(3f, 2f);
    }
}
