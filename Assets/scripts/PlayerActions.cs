using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	private void OnTriggerEnter(Collider other) {
		string tag = other.gameObject.tag;

		if (tag == "Key" || tag == "Door") {
			if (other.gameObject.GetComponent<LockAndKeyManager>() != null) {
        LockAndKeyManager val = other.gameObject.GetComponent<LockAndKeyManager>();
				if (val.key) {
          // is a key
					if (val.accessLevel > gameManager.m.KeyLevelObtained) {
						print ("Obtained a higher access level keycard, can now access level " + val.accessLevel + " doors.");
						gameManager.m.KeyLevelObtained = val.accessLevel;
					} else if (val.accessLevel == gameManager.m.KeyLevelObtained) {
						print ("You already have this level of keycard.");
					} else {
						print ("This keycard is of a lower access level than what you currently have.");
					}
          other.gameObject.SetActive(false);
				} else {
          // is a door
          if (val.accessLevel <= gameManager.m.KeyLevelObtained) {
            print ("Opened level " + val.accessLevel + " door.");
            other.gameObject.GetComponent<Animator>().SetTrigger("OpenDoor");
            // other.gameObject.SetActive(false);
          } else {
            print ("Door cannot be opened, this requires level " + val.accessLevel + " access.\nYou only have level " + gameManager.m.KeyLevelObtained + " access.");
          }
        }
			}
    }
	}

}
