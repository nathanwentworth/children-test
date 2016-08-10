﻿using UnityEngine;
using System.Collections;

public class SmoothFollow : MonoBehaviour {
    
	public Transform target;
	public float speed = 3f;

	public float xOffset = 0f;
	public float yOffset = 0f;
	public float zOffset = -3f;
	public float rotationSpeed = 3.0f;
	private Vector3 targetVector;

	void FixedUpdate () {
		if (!target) return;

		transform.position = new Vector3(
			Mathf.Lerp(transform.position.x, target.transform.position.x + xOffset, Time.deltaTime * speed),
			Mathf.Lerp(transform.position.y, target.transform.position.y + yOffset, Time.deltaTime * speed),
			Mathf.Lerp(transform.position.z, target.transform.position.z + zOffset, Time.deltaTime * speed));

		float wantedRotationAngle = target.eulerAngles.y;
		float currentRotationAngle = transform.eulerAngles.y;
		currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, rotationSpeed * Time.deltaTime);

		var currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);
		transform.rotation = currentRotation;
		transform.position += currentRotation * Vector3.forward * zOffset;
	}
}
