using UnityEngine;
using System.Collections;

public class robotPathing : MonoBehaviour {

	public GameObject[] nodes;
	public GameObject player;
	public float pathTravelTime = 2f;
	public float fov = 90f;

	private float movementSpeed = 2f;
	private bool targeting;
	private Transform target;

	private int nodeIndex;
	private float startTime;
	private bool pathingTime;
	private SphereCollider trigger;

	Vector3 startPos;

	void Start () {
		nodeIndex = 0;
		pathingTime = true;
		trigger = GetComponent<SphereCollider>();
		startPos = transform.position;
	}
	
	void FixedUpdate () {

		if (pathingTime) {
			Pathing();
		} else {
			Searching();
		}

		Movement();
	}

	void Searching() {

	}

	void OnTriggerStay(Collider colliderTarget) {
		if (colliderTarget.gameObject.tag == "Player") {
			targeting = true;
			target = colliderTarget.GetComponent<Transform>();
			Vector3 direction = colliderTarget.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			if (angle < fov * 0.5f) {
				RaycastHit hit;

				if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, trigger.radius)) {
					if (hit.collider.gameObject.tag == "Player") {
						gameManager.m.PlayerSeenByRobot = true;
						movementSpeed = 3f;
						pathingTime = false;
					}
				}
			}
		}
	}

	void OnTriggerExit() {
		gameManager.m.PlayerSeenByRobot = false;
		pathingTime = true;
	}
 
	void Pathing() {
		target = nodes[nodeIndex].transform;
		targeting = true;

		if (Vector3.Distance(transform.position, nodes[nodeIndex].transform.position) < 0.5) {
			nodeIndex++;
		}

		if (nodeIndex > 4) {
			nodeIndex = 0;
		}
		
	}

	void Movement() {
		transform.Translate(movementSpeed * Vector3.forward * Time.deltaTime);
		if (targeting) {
			Vector3 relativePos = target.position - transform.position;
      Quaternion targetRotation = Quaternion.LookRotation(relativePos);
      transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 2f);
		} else {

		}
	}
}
