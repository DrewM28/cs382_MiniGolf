using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolfClub : MonoBehaviour
{
	public Transform clubTransform;
	public Transform gripTransform;
	public float swingSpeed = 100f;
	public float swingRange = 100f;
	private Vector3 initialGripPosition;
	private Vector3 initialClubRotation;
	private bool isHovering = false;


	private void Start()
	{
		initialClubRotation = clubTransform.rotation.eulerAngles;
		initialGripPosition = gripTransform.position;

	}	


	private void Update()
	{
		//if (isHovering)
		{
			Vector3 mousePosition = Input.mousePosition;
			Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(mousePosition);

			Vector3 direction = worldMousePos - gripTransform.position;

			direction.Normalize();
			
			float swingAmount = direction.x * swingRange;

			Quaternion newRotation = Quaternion.Euler(initialClubRotation.x + swingAmount , initialClubRotation.y, initialClubRotation.z);

			clubTransform.rotation = newRotation;
		}
	}
	private void OnMouseEnter()
	{
		isHovering = true;
	}

	private void OnMouseExit()
	{
		isHovering = false;
	}
}
