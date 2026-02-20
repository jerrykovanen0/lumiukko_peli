using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;

public class StopMakingBall : MonoBehaviour
{
    SizeIncrease LateUpdate;
    private SizeIncrease sizeIncrease;

    public void Start()
    {
        sizeIncrease = GetComponent<SizeIncrease>();
        sizeIncrease.enabled = true;
    }

    public void StopSizeVar()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            sizeIncrease.enabled = false;
            Debug.Log("Z key down");
        }
        
    }


}
