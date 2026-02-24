using UnityEngine;
using UnityEngine.Animations;
using System.Collections.Generic;
using UnityEngine.Animations.Rigging;
using System.Threading;
using System.Collections;


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

    Animator animator;


    private void Start()
    {

        animator = GetComponent<Animator>();
    }


    public void Update()
    {
       

        Spawnpoint = SpawnPointObject.transform.position;
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.SetTrigger("MakeBall 0");
            GameObject NewSnowball = Instantiate(SnowballPrefab, Spawnpoint , Quaternion.identity);
            NewSnowball.transform.Translate(0,0,0);
            AttachedTarget.transform.SetParent(NewSnowball.transform);
            AttachedTarget.transform.position = NewSnowball.transform.position;
            Debug.Log("X key down");
            

            
           


        }


    }


}
