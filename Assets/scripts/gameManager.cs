using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	public static gameManager m = null;

	private bool blueKeyObtained = false;
	public bool BlueKeyObtained {
		get {return blueKeyObtained;}
		set {blueKeyObtained = value;}
	}

	private bool playerSeenBySecurityCam = false;
	public bool PlayerSeenBySecurityCam {
		get {return playerSeenBySecurityCam;}
		set {playerSeenBySecurityCam = value;}
	}

	void Awake() {
		if (m == null) {
			m = this;
		} else if (m != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);

	}
}
