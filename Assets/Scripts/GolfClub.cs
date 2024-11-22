using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
    public Transform clubTransform;
    public Transform gripTransform;
    public float swingSpeed = 5f;  // Adjusted for smoother transitions
    public float swingRange = 45f; // Adjusted for realistic swing range

    private Vector3 initialClubRotation;
    private bool isSwinging = false; // Tracks if the club is swinging

    private void Start()
    {
        initialClubRotation = clubTransform.rotation.eulerAngles;
    }

    private void Update()
    {
        // Check if the mouse button is clicked or held
        if (Input.GetMouseButton(0)) // Left mouse button (0), Right mouse button (1)
        {
            if (!isSwinging)
                isSwinging = true; // Start swinging

            HandleSwing();
        }
        else
        {
            isSwinging = false; // Stop swinging
        }
    }

    private void HandleSwing()
    {
        Vector3 mousePosition = Input.mousePosition;

        // Convert mouse position to world space
        Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, Camera.main.transform.position.z - gripTransform.position.z));

        Vector3 direction = worldMousePos - gripTransform.position;
        direction.Normalize();

        // Calculate swing amount and clamp it
        float swingAmount = Mathf.Clamp(direction.x * swingRange, -swingRange, swingRange);

        // Calculate target rotation
        Quaternion targetRotation = Quaternion.Euler(initialClubRotation.x + swingAmount, initialClubRotation.y, initialClubRotation.z);

        // Smoothly rotate the club
        clubTransform.rotation = Quaternion.Lerp(clubTransform.rotation, targetRotation, Time.deltaTime * swingSpeed);
    }
}

