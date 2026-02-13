using Unity.VisualScripting;
using UnityEngine;

public class Player_movement : MonoBehaviour
{
    public float moveSpeed;
    [SerializeField]
    public int test;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
    }
}
