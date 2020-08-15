using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

public class TestEffectsManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EffectsManager effectsManager = GetComponent<EffectsManager>();
        PuddleArgument argument = new PuddleArgument();
        effectsManager.ProducePuddleEffect(argument);
    }
}
