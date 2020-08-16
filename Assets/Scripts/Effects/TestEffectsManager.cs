using UnityEngine;
using UnityEngine.Serialization;

namespace Effects
{
    [RequireComponent(typeof(EffectsManager))]
    public class TestEffectsManager : MonoBehaviour
    {
        public EffectsManager effectManager;
        public ShakeArgument shakeArgument;
        public PuddleArgument puddleArgument;

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
