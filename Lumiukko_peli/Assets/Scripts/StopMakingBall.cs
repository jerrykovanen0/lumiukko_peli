using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;

public class StopMakingBall : MonoBehaviour
{
    SizeIncrease LateUpdate;
    SizeIncrease sizeIncrease;
    MakeSnowball makeSnowball;
    Rigidbody Rigidbody;
    [SerializeField]
    public GameObject OtherBall;
    Vector3 OtherBallPosition;
    MakeSnowball newSnowball;
    

    public void Start()
    {
        sizeIncrease = GetComponent<SizeIncrease>();
        Rigidbody = GetComponent<Rigidbody>();

        sizeIncrease.enabled = true;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if(OtherBall.GetComponent("SizeIncrease") != null) 
            {
                sizeIncrease.enabled = false;
                Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
              //  newSnowball.transform.position = (OtherBallPosition.x, newSnowball.transform.position.y, OtherBallPosition.z);
            }
            else
            {
                sizeIncrease.enabled = false;
                Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
                OtherBallPosition = OtherBall.transform.position;
            }
                


            Debug.Log("Z key down");
        }
        
    }


}
