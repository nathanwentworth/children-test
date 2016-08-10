using UnityEngine;
using System.Collections;

public class SecurityCameraLook : MonoBehaviour {

	[SerializeField]
	private float totalTime = 3f;
	[SerializeField]
	private float waitTime = 2f;

	public GameObject camStatusLight;

	private Quaternion startRot = Quaternion.Euler(35, 45, 0);
	private Quaternion endRot = Quaternion.Euler(35, -45, 0);
	private Quaternion currRot;

	private float t;
	private float startTime;
	private bool reverse;
	private SphereCollider trigger;
	private bool cameraRotation;
	private bool rotateIsRunning;
	private IEnumerator rotate;

	void Start() {
		reverse = false;
		rotate = Rotate();
		trigger = GetComponent<SphereCollider>();
		cameraRotation = true;
		rotateIsRunning = false;
		StartCoroutine(rotate);
	}

	void OnTriggerStay(Collider target) {
		if (target.gameObject.tag == "Player") {
			Vector3 direction = target.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			if (angle < 30f) {
				cameraRotation = false;
				StopCoroutine(rotate);

				Quaternion rot = Quaternion.LookRotation(direction);
				transform.rotation = rot;

				RaycastHit hit;
				if (Physics.Raycast(transform.position, transform.forward, out hit)) {
					if (hit.collider.gameObject.tag == "Player") {
						gameManager.m.PlayerSeenBySecurityCam = true;
						camStatusLight.GetComponent<Renderer>().material.color = new Color(1, 0, 0, 1);
					}
				}
			}
		}
	}

	void OnTriggerExit(Collider target) {
		cameraRotation = true;
		rotateIsRunning = false;
		if (target.gameObject.tag == "Player") {
			camStatusLight.GetComponent<Renderer>().material.color = new Color(0, 1, 0, 1);
			if (!rotateIsRunning) {
				StartCoroutine(rotate);				
			}
		}
	}

	IEnumerator Rotate() {
		rotateIsRunning = true;
		t = 0;
		startTime = 0;
		startTime = Time.time;
		currRot = transform.localRotation;


		while (t < 1 && cameraRotation) {
			float timeSinceStart = Time.time - startTime;
			t = timeSinceStart / totalTime;
			if (reverse) {
				transform.localRotation = Quaternion.Slerp(currRot, startRot, t);
			} else {
				transform.localRotation = Quaternion.Slerp(currRot, endRot, t);				
			}
			yield return null;	
		}

		if (reverse) {
			reverse = false;
		} else {
			reverse = true;
		}
		yield return new WaitForSeconds(waitTime);
		rotateIsRunning = false;
		StartCoroutine(Rotate());
	}
}
