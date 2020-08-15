using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlTypes;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

public class SplatParticle : MonoBehaviour
{
    [Description("Max random object translation on the screen")]
    public Vector3 maxTranslation = new Vector3(5,5,5);
    
    public bool autoStart = false;
    public float spawnDuration = 1.0f;
    public float lifespanDuration = 5.0f;
    public float dripSpeed = 1.0f;
    
    private Transform _cameraTransform;
    private Vector3 _targetRelativePosition;
    private Vector3 _startRelativePosition;
    private State _state = State.Inactive;
    private float _timeStart;
    private float _timeDistracting;
    private float _dripDisplacement;
    private float _timeDead;

    private SpriteRenderer _spriteRenderer;
    
    private const float ZValue = 22;
    private const float StartYValue = -22;

    private enum State
    {
        Inactive,
        Spawning,
        Distracting,
        Dead
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
        if (autoStart)
        {
            StartEffect();   
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        float elapsedTime;
        
        switch (_state)
        {
            case State.Inactive:
                break;
            
            case State.Spawning:
                elapsedTime = Time.time - _timeStart;
                
                transform.position = Vector3.Lerp(_startRelativePosition,
                    _targetRelativePosition,
                    elapsedTime / spawnDuration);
                // todo: replace lerp
                // todo: replace with speed varying spawnDuration
                // calc by t = d / v
                
                if (elapsedTime > spawnDuration)
                {
                    _timeStart = _timeDistracting;
                    _state = State.Distracting;
                    transform.position = _targetRelativePosition;
                    // todo
                }
                break;
            
            case State.Distracting:
                elapsedTime = Time.time - _timeDistracting;
                
                _dripDisplacement = Time.deltaTime * dripSpeed;
                transform.position -= new Vector3(
                    0,
                    _dripDisplacement,
                    0);
                _spriteRenderer.color = new Color(1f,1f,1f,(1 - elapsedTime/lifespanDuration));

                if (elapsedTime > lifespanDuration)
                {
                    _timeStart = _timeDistracting;
                    _state = State.Dead;
                    // todo
                }
                break;

            case State.Dead:
                Destroy(gameObject);
                break;
            
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void StartEffect()
    {
        if (_cameraTransform == null)
        {
            // this is not recommended
            // set camera transform via initiator (EffectsManager)
            Camera camera = Camera.main;
            Assert.IsNotNull(camera);
            _cameraTransform = camera.transform;
        }
        
        _targetRelativePosition = new Vector3(
                maxTranslation.x * Random.value * (Random.value > 0.5 ? -1 : 1),
                maxTranslation.y * Random.value * (Random.value > 0.5 ? -1 : 1),
                ZValue
                );
        _startRelativePosition = new Vector3(0, StartYValue, ZValue);

        _timeStart = Time.time;
        _timeDistracting = _timeStart + spawnDuration;
        _timeDead = _timeDistracting + lifespanDuration;
        _state = State.Spawning;
    }

    public void SetCameraTransform(Transform cameraTransform)
    {
        _cameraTransform = cameraTransform;
    }
}
