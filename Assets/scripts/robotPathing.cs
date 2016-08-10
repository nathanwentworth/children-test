using UnityEngine;
using System.Collections;

public class robotPathing : MonoBehaviour {

	public GameObject[] nodes;
	public GameObject player;
	public float totalTime = 2f;
	public float fov = 90f;

	private int nodeIndex;
	private float startTime;
	private bool pathingTime;
	private SphereCollider trigger;
	private bool playerFound;

	Vector3 startPos;

	void Start () {
		nodeIndex = 0;
		pathingTime = true;
		playerFound = false;
		trigger = GetComponent<SphereCollider>();
		startPos = transform.position;
	}
	
	void Update () {

		if (pathingTime) {
			Pathing();
		} else {
			Searching();
		}


	}

	void Searching() {
		startPos = transform.position;
		float timeSinceStart = Time.time - startTime;
		float t = timeSinceStart / totalTime;

		transform.position = Vector3.Lerp(startPos, player.transform.position, t);
	}

	void OnTriggerStay(Collider target) {
		if (target.gameObject.tag == "Player") {
			Vector3 direction = target.transform.position - transform.position;
			float angle = Vector3.Angle(direction, transform.forward);
			if (angle < fov * 0.5f) {
				RaycastHit hit;

				if (Physics.Raycast(transform.position + transform.up, direction.normalized, out hit, trigger.radius)) {
					if (hit.collider.gameObject.tag == "Player") {
						print("cool");
						playerFound = true;
						pathingTime = false;
					}

				}
			}
		}
	}

	void Pathing() {
		float timeSinceStart = Time.time - startTime;
		float t = timeSinceStart / totalTime;

		transform.position = Vector3.Lerp(startPos, nodes[nodeIndex].transform.position, t);

		if (Vector3.Distance(transform.position, nodes[nodeIndex].transform.position) < 0.05 && t > 0.99) {
			nodeIndex++;
			startPos = transform.position;
			startTime = Time.time;
			
			t = 0;
		}

		transform.position = new Vector3 (transform.position.x, 2, transform.position.z);

		if (nodeIndex > 4) {
			nodeIndex = 0;
		}
		
	}
}
