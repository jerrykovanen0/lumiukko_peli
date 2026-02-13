using UnityEngine;
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
    private float minDistance;
    [SerializeField]
    Vector3 targetpos;
    [SerializeField]
    Vector3 sourcePos;

    private void LateUpdate()
    {
        if (Vector3.Distance(sourcePos, targetpos) <= minDistance)
        {
            transform.position = Vector3.Lerp(this.transform.position, _target.transform.position, _speed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(_target.transform.rotation.eulerAngles);
        }       //raycast try and look on that!! 
    }

}
//Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

// Physics.Raycast(ray, out hit)
//if (Physics.Raycast (ray, out hit, moveDistance, collisionMask, QueryTriggerInteraction.Collide))
// https://matteolopiccolo.medium.com/unity-raycast-for-check-distance-50814034a920 

