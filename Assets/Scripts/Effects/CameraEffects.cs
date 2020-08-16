using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Assertions;
using Random = UnityEngine.Random;

namespace Effects
{
    /**
     * Q: Why are there squared versions for the speed?
     * A: Getting the distance between two vectors require a square root which is kinda expensive
     * when done frame by frame. So, we stick to square magnitudes and not magnitudes. The values get weird
     * between 0.0 and 1.0 but that's okay.
     */
    public class CameraEffects : MonoBehaviour
    {
        public float transitionSpeed = 1f;

        [Header("Required")] public Transform target;
        public Vector3 cameraOffset = Vector3.zero;
        
        private float _shakeEndTime = 0f;
        private float _sqrBounceSpeed = 1f;
        
        private BounceState _bounceState;
        private Vector3 _bounceOffset;

        private RotateState _rotateState;
        private float _sqrRotationSpeed;
        private float halfAngleOffset = 90f;
        
        private ShakeArgument _shakeArgument = new ShakeArgument();
        private Transform localTransform;

        private enum BounceState
        {
            Static,
            ToBouncing,
            Bouncing,
            ToStatic
        }

        private enum RotateState
        {
            Static,
            ToRotating,
            Rotating,
            ToStatic
        }

        private void Start()
        {
            Assert.IsNotNull(target, "No target in CameraEffect");
            _sqrBounceSpeed = transitionSpeed * transitionSpeed;
            localTransform = transform;
        }

        void Update()
        {
            // optimizable
            float sampleY = _shakeArgument.bounceMax * Mathf.PerlinNoise(0, Time.time * _shakeArgument.bounceSpeed);
            Vector3 bounceOffset = (sampleY * Vector3.up) - _bounceOffset;
            Vector3 cameraRestingPosition = target.position + cameraOffset;
            Vector3 cameraRelativeTargetPosition = cameraRestingPosition + bounceOffset;
            
            switch (_bounceState)
            {
                case BounceState.Static:
                    localTransform.position = cameraRestingPosition;
                    break;
                
                case BounceState.ToBouncing:
                    localTransform.position = Vector3.MoveTowards(
                        localTransform.position, 
                        cameraRelativeTargetPosition, transitionSpeed);

                    if (_sqrBounceSpeed > Vector3.SqrMagnitude(localTransform.position - cameraRelativeTargetPosition))
                    {
                        _bounceState = BounceState.Bouncing;
                    }
                    break;
                
                case BounceState.Bouncing:
                    localTransform.position = cameraRelativeTargetPosition;
                    if (Time.time > _shakeEndTime)
                    {
                        _bounceState = BounceState.ToStatic;
                    }
                    break;
                
                case BounceState.ToStatic:
                    localTransform.position = Vector3.MoveTowards(localTransform.position, cameraRestingPosition, transitionSpeed);

                    if (_sqrBounceSpeed > Vector3.SqrMagnitude(localTransform.position - cameraRestingPosition))
                    {
                        _bounceState = BounceState.Static;
                    }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }

            float sampleAngle = _shakeArgument.rotationMax 
                                * Mathf.PerlinNoise(Time.time * _shakeArgument.rotationSpeed, 0)
                                - halfAngleOffset;
            Quaternion targetRotation = Quaternion.AngleAxis(sampleAngle, localTransform.forward);

            switch (_rotateState)
            {
                case RotateState.Static:
                    break;
                
                case RotateState.ToRotating:
                    localTransform.rotation = Quaternion.RotateTowards(
                        localTransform.rotation,
                        targetRotation,
                        _shakeArgument.rotationSpeed);

                    if (_sqrRotationSpeed > Mathf.Abs(
                        localTransform.rotation.eulerAngles.z
                        - targetRotation.eulerAngles.z))
                    {
                        _rotateState = RotateState.Rotating;
                    }
                    break;
                
                case RotateState.Rotating:
                    localTransform.rotation = targetRotation;
                    if (Time.time > _shakeEndTime)
                    {
                        _rotateState = RotateState.ToStatic;
                    }
                    break;
                
                case RotateState.ToStatic:
                    localTransform.rotation = Quaternion.RotateTowards(
                        localTransform.rotation,
                        targetRotation,
                        _shakeArgument.rotationSpeed);

                    if (_sqrRotationSpeed >
                        Mathf.Abs(localTransform.rotation.eulerAngles.z))
                    {
                        localTransform.rotation = Quaternion.identity;
                        _rotateState = RotateState.Static;
                    }
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        // bounce up and down and rotate on z axis
        public void Shake(ShakeArgument argument)
        {
            if (_bounceState == BounceState.Static || _bounceState == BounceState.ToStatic)
            {
                _shakeEndTime = Time.time;
            }
            
            // time related calculations
            _shakeArgument = argument;
            _shakeEndTime = Time.time + Random.Range(
                argument.minDuration,
                argument.maxDuration);
            _bounceState = BounceState.ToBouncing; // race condition vs Update solution
            _rotateState = RotateState.ToRotating;
            
            // position calculations
            _bounceOffset = new Vector3(0, argument.bounceMax/4); // todo: improve offset
            _sqrBounceSpeed = argument.bounceSpeed * argument.bounceSpeed;
            
            // rotation calculations
            _sqrRotationSpeed = argument.rotationSpeed * argument.rotationSpeed;
            halfAngleOffset = argument.rotationMax / 2;

        }
    }
}
