using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.Animations.Rigging;
using static UnityEngine.GraphicsBuffer;


public class testIK : MonoBehaviour
{
    [SerializeField]
    private Transform _target; // Where the hands need to go
    [SerializeField]
    private float _speed;
    [SerializeField]
    private Transform _source;
    [SerializeField]
    TwoBoneIKConstraint constraint;
    TwoBoneIKConstraint HandsIK;
    [SerializeField]
    private Transform Trigger;

    private void Awake()
    {
        constraint.data.targetPositionWeight = 0.0f;
        constraint.data.targetRotationWeight = 0.0f;
    }
    private void OnTriggerEnter(Collider other)
    {
        
        constraint.data.targetPositionWeight = 1.0f;
        constraint.data.targetRotationWeight = 1.0f;

        

    }
    private void OnTriggerExit(Collider other)
    {
        constraint.data.targetPositionWeight = 0.0f;
        constraint.data.targetRotationWeight = 0.0f;
    }


    private void LateUpdate()
    {
     //   constraint.data.targetPositionWeight = 0.7f;
      //  float targetWeight = Input.GetKey(KeyCode.Space) ? 1f : 0f;
      //  HandsIK.weight = Mathf.Lerp(HandsIK.weight, targetWeight, Time.deltaTime * 8f);
     //   HandsIK.weight = 0.7f;

 //      if (Input.GetKey(KeyCode.V))
 //       {
 //           constraint.data.targetPositionWeight = 1.0f;
 //           constraint.data.targetRotationWeight = 1.0f;
 //       }

 //       else
  //      {
  //          constraint.data.targetPositionWeight = 0.0f;
  //          constraint.data.targetRotationWeight = 0.0f;
  //      }

      //         if (Physics.Raycast(transform.position, Vector3.forward, 6.0f))
      //       {
       //          transform.position = Vector3.Lerp(this.transform.position, _target.transform.position, _speed * Time.deltaTime);
       //          transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
       //          constraint.data.targetPositionWeight = 1.0f;
      //            constraint.data.targetRotationWeight = 1.0f;
      //   }


     //   if(Collision.tag ==)


     //   else
      //  {
      //      constraint.data.targetPositionWeight = 0.0f;
     //       constraint.data.targetRotationWeight = 0.0f;
      //  }
     
        
    }

}
//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

// Physics.Raycast(ray, out hit)
//if (Physics.Raycast (ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
// https://matteolopiccolo.medium.com/unity-raycast-for-check-distance-50814034a920 

