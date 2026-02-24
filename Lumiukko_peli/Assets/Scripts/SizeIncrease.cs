using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using System;

public class SizeIncrease : MonoBehaviour
{
    [SerializeField]
    public Rigidbody rb;
    [SerializeField]
    private GameObject sphere;
    [SerializeField]
    private Vector3 scaleChange, positionChange;
    [SerializeField]
    private Vector3 SnowballMovement;
  //  [SerializeField]
  //  public float magnitude;
    


    void Start()
    {
        Rigidbody rigidbody = GetComponent<Rigidbody>();
        rb = GetComponent<Rigidbody>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        enabled = true;
       

        if (rb.linearVelocity.magnitude > 1)
        {
            scaleChange = new Vector3(0.0004f, 0.0004f, 0.0004f);
            positionChange = new Vector3(0.000f, 0.00020f, 0.000f);
            rb.mass += 0.002f;

        }
        // https://stackoverflow.com/questions/69831739/how-can-i-get-a-component-of-a-rigidbodies-velocity
        // https://docs.unity3d.com/6000.3/Documentation/ScriptReference/Rigidbody2D-linearVelocity.html

        //     if (transform.hasChanged)
        //      {
        //         scaleChange = new Vector3(0.01f, 0.01f, 0.01f);
        //         positionChange = new Vector3(0.0f, 0.005f, 0.0f);
        //         transform.hasChanged = false;
        //    }
        sphere.transform.localScale += scaleChange;
        sphere.transform.position += positionChange;

        // Move upwards when the sphere hits the floor or downwards
        // when the sphere scale extends 1.0f.
        if (sphere.transform.localScale.y < 0.1f || sphere.transform.localScale.y > 1.0f)
        {
            scaleChange = -scaleChange;
            positionChange = -positionChange;
           transform.hasChanged = false;
        }
        transform.hasChanged = false;




    }



}
