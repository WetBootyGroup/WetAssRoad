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

        private void Awake()
        {
            if (Camera.main != null)
            {
                _cameraTransform = Camera.main.transform;
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
                PuddleParticle script = Instantiate(puddleEffectPrefab, _cameraTransform)
                    .GetComponent<PuddleParticle>();
                script.ApplyEffect(argument);
                script.StartEffect();
            }
        }
    }

    // better used as struct
    public class PuddleArgument
    {
        // todo: if need arguments to change effects put it here
        // If count = 0, the effects manager decides the number of splats randomly
        public int Count = -1;
        
        // Max duration of splat in seconds
        public float maxDuration = 10.0f;
        
        // Do not touch
        public EffectsManager effectsManager = null;
    }
}
