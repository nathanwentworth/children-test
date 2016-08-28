using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	public Transform player;
	public Transform cam;
	public Rigidbody rb;
	public GameObject pausePanel;

	public float moveSpeed = 4f,
	rotationSpeed = 50f,
	jumpStrength = 5f,
	mouseSensitivity = 100.0f,
	clampAngle = 80.0f;

	private float rotY = 0.0f;
	private float rotX = 0.0f;
 

	private float
	yRotation,
	xRotation;

	private bool
	grounded,
	allowPlayerMovement;

	private Vector3
	dir,
	forward,
	right;

	private Quaternion rotation;


	void Start() {
		rb = GetComponent<Rigidbody>();
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		dir = Vector3.zero;
		forward = cam.TransformDirection(Vector3.forward);
		forward.y = 0f;
		Lock(true);
	}
	
	void FixedUpdate() {

		if (Physics.Raycast(transform.position, Vector3.down, 1.1f)) {
			grounded = true;
		} else {
			grounded = false;
		}

		if (grounded) {
			moveSpeed = 4;
		} else {
			moveSpeed = 3;
		}

		forward = forward.normalized;
		right = new Vector3(forward.z, 0.0f, -forward.x);


		float h = Input.GetAxis("Horizontal");
    float v = Input.GetAxis("Vertical");
    bool crouch = Input.GetButton("Crouch");
    bool jump = Input.GetButton("Jump");
    bool pause = Input.GetButton("Cancel");

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

    Inputs(h, v, crouch, jump);
    if (!gameManager.m.Paused) {
    	allowPlayerMovement = true;
    }
    if (pause) {
  	  Lock(false);
    	Pause();
  	}
		if (Input.GetButton("Click")) {
			if (!gameManager.m.Paused) {
				Lock(true);				
			}
		}
	}

	void Inputs(float h, float v, bool crouch, bool jump) {
		if (allowPlayerMovement) {
			player.transform.Translate(v * forward * moveSpeed * Time.deltaTime);
			player.transform.Translate(h * right * moveSpeed * 0.75f * Time.deltaTime);
			if (grounded && jump) {
				rb.AddForce(transform.up * jumpStrength);
			}
			if (h > 0) {
				player.transform.Rotate(-h * Vector3.up * rotationSpeed * Time.deltaTime);
			}
		}
	}

	void Lock (bool lockState) {
		if (lockState) Cursor.lockState = CursorLockMode.Locked;
		else Cursor.lockState = CursorLockMode.None;
		Cursor.visible = !lockState;
	}


	void Pause() {
		if (!gameManager.m.Paused) {
			pausePanel.GetComponent<CanvasGroup>().alpha = 1;
			pausePanel.GetComponent<CanvasGroup>().interactable = true;
			Time.timeScale = 0;
			gameManager.m.Paused = true;
		} else {
			pausePanel.GetComponent<CanvasGroup>().alpha = 0;
			pausePanel.GetComponent<CanvasGroup>().interactable = false;
			Time.timeScale = 1;
			gameManager.m.Paused = false;
		}
	}
}
