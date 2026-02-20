using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;



public class MakeSnowball : MonoBehaviour
{
    [SerializeField]
    public Vector3 Spawnpoint;
    [SerializeField]
    public GameObject SnowballPrefab;
    [SerializeField]
    public GameObject SpawnPointObject;
    [SerializeField]
    public GameObject AttachedTarget;
    [SerializeField]
    public Transform parentTarget; 
  


    private void Start()
    {
        
        
    }

    private void Update()
    {

        Spawnpoint = SpawnPointObject.transform.position;
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject NewSnowball = Instantiate(SnowballPrefab, Spawnpoint , Quaternion.identity);
            NewSnowball.transform.Translate(0,0,0);
            AttachedTarget.transform.SetParent(NewSnowball.transform);
            AttachedTarget.transform.position = NewSnowball.transform.position;

            Debug.Log("X key down");

        }
        
    }


}
