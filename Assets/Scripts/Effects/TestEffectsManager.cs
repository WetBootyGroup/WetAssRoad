using UnityEngine;

namespace Effects
{
    [RequireComponent(typeof(EffectsManager))]
    public class TestEffectsManager : MonoBehaviour
    {
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
