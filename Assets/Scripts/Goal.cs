using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Renderer))]
public class Goal : MonoBehaviour
{
    static public bool goalMet = false;

    private void OnTriggerEnter( Collider other ) {
        GolfBall golfBall = other.GetComponent<GolfBall>();
        if( golfBall != null)
        {
            //Golf ball reaches the goal so we call goalMet()
            Goal.goalMet = true;

            Debug.Log("The golf ball reached the goal!");
        }
    }
}
