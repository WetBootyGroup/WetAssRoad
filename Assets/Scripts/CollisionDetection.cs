using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Crashable")) {
            ICrashable c = other.gameObject.GetComponent<ICrashable>();
            c.onCrash();
        }
    }
}
