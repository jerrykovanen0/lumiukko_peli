using Unity.VisualScripting;
using UnityEngine;

public class FinishGame : MonoBehaviour
{
    [SerializeField]
    GameObject SnowBall;
    [SerializeField]
    Collider Collider;
    [SerializeField]
    Collider playerColl;
    [SerializeField]
    GameObject character;
    [SerializeField]
    GameObject Win;
    [SerializeField]
    GameObject Lose;

    private void OnTriggerEnter(Collider Collider)
    {
        if ((GameObject.FindWithTag("SnowBallTag")) != null)
        {

                Debug.Log("Voitit pelin!");
                Win.SetActive(true);
                character.SetActive(false);

             
        }
        else 
        {

                Debug.Log("Hävisit pelin");
                Lose.SetActive(true);
                character.SetActive(false);

        

        }


        
    }
}
