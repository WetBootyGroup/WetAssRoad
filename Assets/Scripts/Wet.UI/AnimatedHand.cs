using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedHand : MonoBehaviour
{
    public float maxRefX = 8.6f;
    public float maxX = 10f;

    public Transform movingReference;
    private RectTransform _rectTransform;
    private Vector2 _referencePosition;

    private void Start()
    {
        _rectTransform = GetComponent<RectTransform>();
        _referencePosition = _rectTransform.anchoredPosition;
    }

    private void Update()
    {
        float clampVal = Mathf.Clamp(movingReference.position.x,
            -maxRefX, maxRefX);
        float percentage = clampVal / maxRefX;
        float lerpValue = Mathf.LerpUnclamped(0f, maxX, percentage);
        _rectTransform.anchoredPosition = _referencePosition
                                          + new Vector2(
                                              lerpValue,
                                              -Mathf.Abs(lerpValue)/2f);
    }
}
