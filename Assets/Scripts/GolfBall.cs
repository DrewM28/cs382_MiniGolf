using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private Rigidbody rigid;
    private List<float> deltas = new List<float>();
    private Vector3 prevPos;

    const int LOOKBACK_COUNT = 10;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        prevPos = new Vector3(1000,1000,0);
        deltas.Add(1000);

    }

    void FixedUpdate()
    {
        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add(deltaV3.magnitude);

        while (deltas.Count > LOOKBACK_COUNT)
        {
            deltas.RemoveAt(0);
        }
        
        float maxDelta = 0;

        foreach(float f in deltas)
        {
            if(f > maxDelta) maxDelta = f;
        }


    }
    // public float hitForceMultiplier = 2f;
    // public float maxSpeed = 20f;

    // // Start is called before the first frame update
    // void Start()
    // {
    //     ballRigidbody = GetComponent<Rigidbody>();
    // }

    // private void OnCollision(Collision collision)
    // {
    //     if (collision.gameObject.CompareTag("GolfClub"))
    //     {
    //         Vector3 hitDirection = collision.contacts[0].normal;

    //         ApplyHitForce(hitDirection);
    //     }
    // }

    // void ApplyHitForce(Vector3 hitDirection)
    // {
    //     ballRigidbody.AddForce(hitDirection * hitForceMultiplier, ForceMode.Impulse);

    //     if (ballRigidbody.velocity.magnitude > maxSpeed)
    //     {
    //         ballRigidbody.velocity = ballRigidbody.velocity * maxSpeed;
    //     }
    // }
    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
