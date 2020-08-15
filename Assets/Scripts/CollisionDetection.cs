using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private void OnTriggerEnter (Collider other)
    {
        if (other.gameObject.CompareTag("Crashable")) {
            Debug.Log("Crashed into wet pussy/pothole/puddle");
        }
    }
}
