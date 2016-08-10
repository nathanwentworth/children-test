using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Transform player;
	public Transform cam;
	public Rigidbody rb;

	private float speed = 4f;
	public float rotationSpeed = 50f;
	public float jumpStrength = 5f;

	public float mouseSensitivity = 100.0f;
	public float clampAngle = 80.0f;

	private float rotY = 0.0f;
	private float rotX = 0.0f;
 

	private float
	yRotation,
	xRotation;

	private Vector3 dir;
	private Vector3 forward;
	private Vector3 right;
	private Quaternion rotation;
	private bool grounded;

	void Start() {
		rb = GetComponent<Rigidbody>();
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		dir = Vector3.zero;
		forward = cam.TransformDirection(Vector3.forward);
		forward.y = 0f;
	}
	
	void FixedUpdate() {

		if (Physics.Raycast(transform.position, Vector3.down, 1.1f)) {
			grounded = true;
		} else {
			grounded = false;
		}

		if (grounded) {
			speed = 4;
		} else {
			speed = 3;
		}

		forward = forward.normalized;
		right = new Vector3(forward.z, 0.0f, -forward.x);


		float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    bool crouch = Input.GetButton("Crouch");
    bool jump = Input.GetButton("Jump");

    dir.x = h;
    dir.z = v;

    dir = cam.transform.TransformDirection(dir);
    dir.y = 0.0f;

    float mouseX = Input.GetAxis("Mouse X");
		float mouseY = -Input.GetAxis("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
		transform.rotation = localRotation;

    Movement(h, v, crouch, jump);
	}

	void Movement(float h, float v, bool crouch, bool jump) {
		player.transform.Translate(v * forward * speed * Time.deltaTime);
		player.transform.Translate(h * right * speed * 0.75f * Time.deltaTime);
		if (grounded && jump) {
			rb.AddForce(transform.up * jumpStrength);
		}
		if (h > 0) {
			player.transform.Rotate(-h * Vector3.up * rotationSpeed * Time.deltaTime);
		}
	}
}
