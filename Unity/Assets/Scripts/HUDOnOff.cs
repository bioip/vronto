using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDOnOff : MonoBehaviour {

	private GameObject HUD;
	private GameObject keyboard;
	private GameObject input;
	private GameObject keys;
	private GameObject text;

	// Use this for initialization
	void Start () {
		HUD = this.gameObject.transform.GetChild(0).gameObject;
		keyboard = this.gameObject.transform.GetChild(1).gameObject;
		input = keyboard.transform.GetChild(1).gameObject;
		keys = keyboard.transform.GetChild(2).gameObject;
		text = keyboard.transform.GetChild(3).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleOnOff(){
		HUD.SetActive(!HUD.activeInHierarchy);
		keys.SetActive(!keys.activeInHierarchy);
		text.SetActive(!text.activeInHierarchy);
	}
}
