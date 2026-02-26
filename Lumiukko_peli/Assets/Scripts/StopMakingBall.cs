using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class StopMakingBall : MonoBehaviour
{
    

    SizeIncrease LateUpdate;
    SizeIncrease sizeIncrease;
    MakeSnowball makeSnowball;
    Rigidbody Rigidbody;
    GameObject OtherBall;
    MakeSnowball newSnowball;
    GameObject CurrentBall;
    Transform OtherBallPosition;
    private Transform target;
    float Test = 10;
    float HeightOfBall;

    public void Start()
    {
        sizeIncrease = GetComponent<SizeIncrease>();
        Rigidbody = GetComponent<Rigidbody>();
 //       OtherBall = GameObject.FindWithTag("SnowBallTag");
 //       OtherBall.transform.position = OtherBallPosition.transform.position;
        sizeIncrease.enabled = true;

    }

    public void Update()
    {

        

        if (Input.GetKeyDown(KeyCode.Z))
        {

            sizeIncrease.enabled = false;
            Rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
           

            Debug.Log("Z key down");


            if ((GameObject.FindWithTag("SnowBallTag")) != null)

            {
                Transform Pos = GameObject.FindWithTag("SnowBallTag").transform;
                float PosX = Pos.position.x;
                float PosY = Pos.position.y;
                float PosZ = Pos.position.z;
                HeightOfBall = PosY + 3;
                gameObject.transform.position = new Vector3(PosX, HeightOfBall, PosZ);
                Debug.Log("There is Ball already");
            }
            else
            {
                
                Debug.Log("No Ball yet");
            }
            gameObject.tag = "SnowBallTag";

        }
        
    }



}
