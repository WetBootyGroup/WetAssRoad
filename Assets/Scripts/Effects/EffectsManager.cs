using UnityEngine;
using UnityEngine.Serialization;

namespace Effects
{
    public class EffectsManager : MonoBehaviour
    {
        public GameObject puddleEffectPrefab;
        
        private Camera _camera;
    
        // Start is called before the first frame update
        void Start()
        {
            _camera = Camera.main;
        }

        public void ProducePuddleEffect(PuddleArgument puddleArgument)
        {
            
        }
    }

    // better used as struct
    public class PuddleArgument
    {
        // todo: if need arguments to change effects put it here
    }
}
