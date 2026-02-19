using UnityEngine;
using UnityEngine.Animations;



public class MakeSnowball : MonoBehaviour
{
    [SerializeField]
    public Vector3 Spawnpoint;
    [SerializeField]
    public GameObject SnowballPrefab;
    [SerializeField]
    public GameObject SpawnPointObject;
    

    private void Start()
    {
        
        
    }

    private void Update()
    {

        Spawnpoint = SpawnPointObject.transform.position;
        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(SnowballPrefab, Spawnpoint , Quaternion.identity);

            // https://discussions.unity.com/t/how-to-add-child-gameobjects-to-parent-in-script/73903
            // https://discussions.unity.com/t/deleting-specific-child-object/572491
        }

    }


}
