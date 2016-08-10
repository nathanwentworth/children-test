using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		if (other.gameObject.tag == "Key") {
			if (other.gameObject.name == "BlueKey") {
				gameManager.m.BlueKeyObtained = true;
				other.gameObject.SetActive(false);
			}
		}
		if (other.gameObject.tag == "Door") {
			if (other.gameObject.name == "BlueDoor") {
				if (gameManager.m.BlueKeyObtained == true) {
					other.gameObject.SetActive(false);
				}
			}
		}
	}

}
