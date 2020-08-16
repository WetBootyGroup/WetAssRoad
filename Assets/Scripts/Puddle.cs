using System.Collections;
using System.Collections.Generic;
using Effects;
using UnityEngine;

public class Puddle : MonoBehaviour, ICrashable
{
    public float moveSpeed;

    [Header("Effects")] public EffectsManagerLocator effectsManagerLocator;

    Transform carRef;
    ProgressManager progressManagerRef;
    
    private EffectsManager _effectsManager;

    void Start()
    {
        carRef = GameObject.Find("Car").transform;
        progressManagerRef = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();

        _effectsManager = effectsManagerLocator.GetEffectsManager();
    }

    void FixedUpdate () {
        transform.Translate(new Vector3(0,0,-1 * progressManagerRef.GetGameSpeed() * Time.deltaTime));

        if (Mathf.Abs(transform.position.z - carRef.position.z) <= 0.01) {//!!!!!!!!!!!!!!!!!!!!!!!!
            Destroy(gameObject);
        }
    }

    public void onCrash () {
        Debug.Log("puddle crash");
        
        // you can have your custom arguments, too!
        _effectsManager.ProducePuddleEffect();
    }
}
