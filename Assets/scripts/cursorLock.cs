using UnityEngine;
using System.Collections;

public class cursorLock : MonoBehaviour {

	void Start () {
		Lock(true);
	}
	
	void Update () {
		if (Input.GetButton("Click")) {Lock(true);}
		else if (Input.GetButton("Cancel")) {Lock(false);}
	}

	void Lock (bool lockState) {
		if (lockState) Cursor.lockState = CursorLockMode.Locked;
		else Cursor.lockState = CursorLockMode.None;
		Cursor.visible = !lockState;
	}
}
