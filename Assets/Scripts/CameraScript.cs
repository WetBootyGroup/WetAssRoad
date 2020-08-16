using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float playerspeed;

    void OnCollisionEnter(Collision collision) 
{   
        if(collision.collider.tag == "Roadside") 
        {
            Debug.Log("HIT");
             GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
}
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
    //if()
        //use for cat objects:
        //transform.Translate(new Vector3(0,0,cameraspeed * Time.deltaTime));

        // if(Input.GetKey(KeyCode.RightArrow))
        // {
        //     GetComponent<Rigidbody>().velocity = new Vector3(playerspeed,0,0);
        // }
        // if(Input.GetKey(KeyCode.LeftArrow))
        // {
        //     GetComponent<Rigidbody>().velocity = new Vector3(-playerspeed,0,0);
        //     //transform.Translate(new Vector3(-playerspeed * Time.deltaTime,0,0));
        // }

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        GetComponent<Rigidbody>().velocity = new Vector3(playerspeed * horizontalInput,0,0);
    }
}

