using UnityEngine;

public class DisableStopMakingBall : MonoBehaviour
{
    StopMakingBall m_StopMakingBall;

    private void Update()
    {
        if(gameObject.tag == "SnowBallTag")
        {
         m_StopMakingBall.enabled = false;


    }

    }




}
