﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pothole : MonoBehaviour, ICrashable
{
    public float moveSpeed;

    void FixedUpdate () {
        transform.Translate(new Vector3(0,0,-1 * moveSpeed * Time.deltaTime));
    }

    public void onCrash () {
        Debug.Log("pothole crash");
    }
}
