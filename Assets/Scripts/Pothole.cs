using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pothole : MonoBehaviour, ICrashable
{
    public float moveSpeed;

    Transform carRef;

    void Start()
    {
        carRef = GameObject.Find("Car").transform;
    }

    void FixedUpdate () {
        transform.Translate(new Vector3(0,0,-1 * moveSpeed * Time.deltaTime));

        if (Mathf.Abs(transform.position.z - carRef.position.z) <= 0.01) {//!!!!!!!!!!!!!!!!!!!!!!!!!
            Destroy(gameObject);
        }
    }

    public void onCrash () {
        Debug.Log("pothole crash");
    }
}
