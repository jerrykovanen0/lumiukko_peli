using UnityEngine;
using UnityEngine.InputSystem;

public class Anotherplayermovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    InputAction moveAction;
    public int forward;
    public int backward;
    public int left;
    public int right; 


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        moveAction = InputSystem.actions.FindAction("Move");
    }

    void FixedUpdate()
    {

        Keyboard keyboard =


        Vector3 moveValue = moveAction.ReadValue<Vector3>();

        if 


    }

}


