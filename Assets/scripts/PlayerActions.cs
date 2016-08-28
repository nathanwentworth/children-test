using UnityEngine;
using System.Collections;

public class PlayerActions : MonoBehaviour {

	void OnTriggerEnter(Collider other) {
		string tag = other.gameObject.tag;

		if (tag.StartsWith("Key") || tag.StartsWith("Door")) {
			string tagNum = tag.Substring(tag.Length - 1);
			int tagNumVal = int.Parse(tagNum);

			if (tag.StartsWith("Key")) {
				if (tagNumVal > gameManager.m.KeyLevelObtained) {
					print ("Obtained a higher access level keycard, can now access level " + tagNumVal + " doors.");
					gameManager.m.KeyLevelObtained = tagNumVal;
				} else if (tagNumVal == gameManager.m.KeyLevelObtained) {
					print ("You already have this level of keycard.");
				} else {
					print ("This keycard is of a lower access level than what you currently have.");
				}
				other.gameObject.SetActive(false);
			}

			if (tag.StartsWith("Door")) {
				if (tagNumVal <= gameManager.m.KeyLevelObtained) {
					print ("Opened level " + tagNumVal + " door.");
					other.gameObject.SetActive(false);
				} else {
					print ("Door cannot be opened, this requires level " + tagNumVal + " access.\nYou only have level " + gameManager.m.KeyLevelObtained + " access.");
				}
			}
		}
	}

}
