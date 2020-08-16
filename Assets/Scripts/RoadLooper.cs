using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadLooper : MonoBehaviour
{
    public GameObject[] roadList;

    public float roadMoveSpeed;
    ProgressManager progressManagerRef;

    void Start()
    {
        progressManagerRef = GameObject.Find("ProgressManager").GetComponent<ProgressManager>();
    }

    void Update() {
        foreach (GameObject r in roadList) {
            r.transform.Translate(new Vector3(0,0, -1 * progressManagerRef.GetGameSpeed() * Time.deltaTime));

            if (r.transform.position.z <= -35) {
                Vector3 oldPos = r.transform.position;
                oldPos.z = 60;
                r.transform.position = oldPos;
            }
        }
    }
}
