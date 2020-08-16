using UnityEngine;

namespace Effects
{
    [CreateAssetMenu(fileName = "EffectsManagerLocator", menuName = "ScriptableObject/Locator/EffectsManager")]
    public class EffectsManagerLocator : ScriptableObject
    {
        public GameObject effectsManagerPrefab;
        private EffectsManager _effectsManager;

        /// <summary>
        /// Finds effects manager
        /// </summary>
        /// <remarks>
        /// 1. Only use once per object. If you need the reference again,
        /// keep a local variable reference for this.
        /// 2. Do not use on or before Awake on any objects that
        /// are on the hierarchy as soon as the game starts
        /// </remarks>
        /// <returns>EffectsManager for the scene</returns>
        public EffectsManager GetEffectsManager()
        {
            if (_effectsManager == null)
            {
                _effectsManager = Instantiate(effectsManagerPrefab).GetComponent<EffectsManager>();
                Debug.LogWarning("Put effects manager prefab on the hierarchy to reduce delay!");
            }

            return _effectsManager;
        }

        internal void SetEffectsManager(EffectsManager effectsManager)
        {
            _effectsManager = effectsManager;
        }
    }
}
