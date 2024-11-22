using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private Rigidbody rigid;
    private List<float> deltas = new List<float>();
    private Vector3 prevPos;

    public int lookbackCount = 10; // Number of frames to track speed
    public float hitForceMultiplier = 10f; // Multiplier for force application
    public float maxSpeed = 20f; // Maximum ball speed

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        prevPos = transform.position;
    }

    void FixedUpdate()
    {
        // Track movement deltas
        Vector3 deltaV3 = transform.position - prevPos;
        deltas.Add(deltaV3.magnitude);

        // Keep delta list size within `lookbackCount`
        while (deltas.Count > lookbackCount)
        {
            deltas.RemoveAt(0);
        }

        // Calculate max delta for debug purposes
        float maxDelta = 0f;
        foreach (float delta in deltas)
        {
            if (delta > maxDelta) maxDelta = delta;
        }

        // Optionally, debug log the maxDelta
        Debug.Log($"Max Delta: {maxDelta}");

        // Update previous position
        prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a golf club
        if (collision.gameObject.CompareTag("GolfClub"))
        {
            Vector3 hitDirection = collision.contacts[0].normal * -1f; // Get impact direction
            ApplyHitForce(hitDirection);
        }
    }

    void ApplyHitForce(Vector3 hitDirection)
    {
        // Apply an impulse force to the ball
        rigid.AddForce(hitDirection * hitForceMultiplier, ForceMode.Impulse);

        // Clamp the ball's velocity to `maxSpeed`
        if (rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }
}
