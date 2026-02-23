using UnityEngine;

public class AnimatorAttach : MonoBehaviour
{

    [SerializeField]
    Animator animator;
    testIK testIK;

    public void Start()
    {
        animator = GetComponent<Animator>();
        testIK = GetComponent<testIK>();   
    }

    public void AnimatorIK()
    {
 //       if () {


  //      }
    }

}
