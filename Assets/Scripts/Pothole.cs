using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pothole : MonoBehaviour, ICrashable
{
    public float moveSpeed;

    Transform carRef;
    ProgressManager progressManagerRef;

    void Start()
    {
        carRef = GameObject.Find("Car").transform;
        progressManagerRef = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();
    }

    void FixedUpdate () {
        transform.Translate(new Vector3(0,0,-1 * progressManagerRef.GetGameSpeed() * Time.deltaTime));

        if (Mathf.Abs(transform.position.z - carRef.position.z) <= 0.01) {//!!!!!!!!!!!!!!!!!!!!!!!!!
            Destroy(gameObject);
        }
    }

    public void onCrash () {
        Debug.Log("pothole crash");
    }
}
