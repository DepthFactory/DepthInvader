using UnityEngine;
using UnityEngine.UI;
using DesignPattern;
using System.Collections;

public class PauseMenuManager : Singleton<PauseMenuManager> {

	public GameObject goPauseMenu;
	private bool bIsGamePaused;

	public bool isGamePaused() { return bIsGamePaused; }

	private void Start () {
		bIsGamePaused = false;
		goPauseMenu.SetActive(bIsGamePaused);
	}

	private void Update () {
		if (Input.GetKeyDown (KeyCode.Space))
		{
			if(goPauseMenu.activeSelf == false)
				bIsGamePaused = true;
			else
				bIsGamePaused = false;
			goPauseMenu.SetActive(bIsGamePaused);
		}
	}
}
