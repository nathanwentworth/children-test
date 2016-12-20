using UnityEngine;
using System.Collections;
using InControl;

public class PlayerMovement : MonoBehaviour {

	public Transform player;
	public Transform cam;
	public GameObject pausePanel;

	private Rigidbody rb;
	private Controls controls;

	public int InverseLook { get; set; }

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

	private Vector3 dir;

	private Quaternion rotation;

	private void OnEnable() {
		controls = Controls.DefaultBindings();
	}

	void Start() {
		InverseLook = -1;
		rb = GetComponent<Rigidbody>();
		Vector3 rot = transform.localRotation.eulerAngles;
		rotY = rot.y;
		rotX = rot.x;
		dir = Vector3.zero;
		Lock(true);
	}
	
	private void FixedUpdate() {

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

		dir = controls.Move;
		dir.z = dir.y;
    dir.y = 0.0f;

    print (dir);

    bool crouch = Input.GetButton("Crouch");
    bool jump = controls.Jump.IsPressed;
    bool interact = controls.Interact.IsPressed;
    bool pause = controls.Pause.IsPressed;


    float mouseX = controls.Look;
		float mouseY = InverseLook * Input.GetAxis("Mouse Y");

		rotY += mouseX * mouseSensitivity * Time.deltaTime;
		rotX += mouseY * mouseSensitivity * Time.deltaTime;

		rotX = Mathf.Clamp(rotX, -clampAngle, clampAngle);

		Quaternion localRotation = Quaternion.Euler(0.0f, rotY, 0.0f);
		transform.rotation = localRotation;

    Inputs(dir.normalized, crouch, jump, interact);

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

	void Inputs(Vector3 dir, bool crouch, bool jump, bool interact) {
		if (allowPlayerMovement) {
			player.transform.Translate(dir * moveSpeed * Time.deltaTime, cam.transform);
			// player.transform.Translate(dir.x * moveSpeed * 0.75f * Time.deltaTime, Camera.main.transform);
			if (grounded && jump) {
				rb.AddForce(transform.up * jumpStrength);
			}
			if (dir.x > 0) {
				player.transform.Rotate(-dir.z * Vector3.up * rotationSpeed * Time.deltaTime);
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
