using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class pauseFunctions : MonoBehaviour {

	public GameObject pausePanel;

	public void LoadLevel(int index) {
		SceneManager.LoadScene(index);
	}
	public void UnPauseGame() {
		pausePanel.GetComponent<CanvasGroup>().alpha = 0;
		pausePanel.GetComponent<CanvasGroup>().interactable = false;
		Time.timeScale = 1;
		gameManager.m.Paused = false;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}
}
