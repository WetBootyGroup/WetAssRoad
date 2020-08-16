using UnityEngine;
using UnityEngine.Serialization;

namespace Effects
{
    [RequireComponent(typeof(EffectsManager))]
    public class TestEffectsManager : MonoBehaviour
    {
        public ShakeArgument shakeArgument;
        public PuddleArgument puddleArgument;
        
        public EffectsManager effectManager;

        // Start is called before the first frame update
        void Start()
        {
            if (effectManager == null)
            {
                effectManager = GetComponent<EffectsManager>();
            }
        }
    }
}
