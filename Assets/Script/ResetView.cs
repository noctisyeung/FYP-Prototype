using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetView : MonoBehaviour
{
         private GvrViewer gr;
     void Start () {
         gr = new GvrViewer ();
     }
 
     void Update () {
         if (Input.GetMouseButtonDown(0)) {
            Debug.Log("Pressed");
            gr.Recenter ();
         }
 
     }
     }


   /* public Transform target;
    Quaternion prevRotation;

    void Start()
    {
        //set your first rotation as starting head rotation
        prevRotation = Camera.main.transform.rotation;
        Debug.Log(prevRotation);


    }

    void Update()
    {
        //on button push
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Pressed");
            Debug.Log(target.rotation);
            //doesn't have to reset to target, can reset to anything you need
            //such as Quaternion.identity
            Camera.main.transform.rotation = prevRotation;
            //target.rotation = prevRotation;
            Debug.Log(target.rotation);

        }
    }
}*/
