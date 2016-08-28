using UnityEngine;
using System.Collections;

public class gameManager : MonoBehaviour {

	// /// //// Key Status //// /// //
	private int keyLevelObtained = 0;
	public int KeyLevelObtained {
		get {return keyLevelObtained;}
		set {keyLevelObtained = value;}
	}


	// /// //// General Functions //// /// //
	private bool paused = false;
	public bool Paused {
		get {return paused;}
		set {paused = value;}
	}

	// /// //// Enemy States //// /// //
	private bool playerSeenBySecurityCam = false;
	public bool PlayerSeenBySecurityCam {
		get {return playerSeenBySecurityCam;}
		set {playerSeenBySecurityCam = value;}
	}

	private bool playerSeenByRobot = false;
	public bool PlayerSeenByRobot {
		get {return playerSeenByRobot;}
		set {playerSeenByRobot = value;} 
	}




	public static gameManager m = null;

	void Awake() {
		if (m == null) {
			m = this;
		} else if (m != this) {
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
}
