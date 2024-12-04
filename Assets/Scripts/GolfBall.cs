using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfBall : MonoBehaviour
{
    private Rigidbody rigid;
    private List<float> deltas = new List<float>();
    static List<GolfBall> GOLFBALL = new List<GolfBall>();
    private Vector3 prevPos;

    public int lookbackCount = 10; // Number of frames to track speed
    public float hitForceMultiplier = 10f; // Multiplier for force application
    public float maxSpeed = 20f; // Maximum ball speed

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        prevPos = transform.position;
        GOLFBALL.Add(this); // Add to static list
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

        // Optional: Debug max delta
        float maxDelta = 0f;
        foreach (float delta in deltas)
        {
            if (delta > maxDelta) maxDelta = delta;
        }

        // Debug mode toggle
        bool debugMode = false; // Set to true for debug logging
        if (debugMode)
        {
            Debug.Log($"Max Delta: {maxDelta}");
        }

        // Update previous position
        prevPos = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log($"Collision detected with: {collision.gameObject.name}");
        if (collision.gameObject.CompareTag("GolfClub"))
        {
            Vector3 hitDirection = collision.contacts[0].normal * -1f; // Get impact direction
            ApplyHitForce(hitDirection);
        }
    }

    void ApplyHitForce(Vector3 hitDirection)
    {
        // Apply an impulse force to the ball
        rigid.AddForce(hitDirection.normalized * hitForceMultiplier, ForceMode.Impulse);

        // Clamp the ball's velocity to `maxSpeed`
        if (rigid.velocity.magnitude > maxSpeed)
        {
            rigid.velocity = rigid.velocity.normalized * maxSpeed;
        }
    }

    // private void OnDestroy()
    // {
    //     GOLFBALL.Remove(this); // Remove from static list
    // }

    static public void DESTROY_GOLFBALL()
    {
        foreach (GolfBall g in new List<GolfBall>(GOLFBALL))
        {
            if (g != null && g.gameObject != null)
            {
                Destroy(g.gameObject);
            }
        }
        GOLFBALL.Clear();
    }
}
