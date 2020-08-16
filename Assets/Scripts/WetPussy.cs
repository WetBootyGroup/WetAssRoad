using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WetPussy : MonoBehaviour, ICrashable
{
    public float moveSpeed;
    
    Transform carRef;

    void Start()
    {
        carRef = GameObject.Find("Car").transform;
    }

    void FixedUpdate () {
        transform.Translate(new Vector3(0,0,-1 * moveSpeed * Time.deltaTime));

        if (Mathf.Abs(transform.position.z - carRef.position.z) <= 0.1) { //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
            Destroy(gameObject);
        }
    }

    public void onCrash () {
        Debug.Log("Pussy crash");
    }
}
