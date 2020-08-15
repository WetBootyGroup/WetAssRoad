using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float playerspeed;

    public Transform groundTrans;


    // Start is called before the first frame update
    void Start()
    {
       float width = groundTrans.localScale.x;
       float center = width/2;
       print(center);
        
    }

    // Update is called once per frame
    void Update()
    {
    //if()
        //use for cat objects:
        //transform.Translate(new Vector3(0,0,cameraspeed * Time.deltaTime));

        if(Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(new Vector3(playerspeed * Time.deltaTime,0,0));
        }
        if(Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(new Vector3(-playerspeed * Time.deltaTime,0,0));
        }
     
    }
}

