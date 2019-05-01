using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class HUDOnOff : MonoBehaviour {

	private GameObject HUD;
	private GameObject keyboard;
	private GameObject input;
	private GameObject keys;
	private GameObject rightController;
    private GameObject leftController;
    private VRTK_ControllerEvents leftControllerEvents;
    private VRTK_ControllerEvents rightControllerEvents;
	private bool buttonPressed;

	// Use this for initialization
	void Start () {
		HUD = this.gameObject.transform.GetChild(0).gameObject;
		keyboard = this.gameObject.transform.GetChild(1).gameObject;
		input = keyboard.transform.GetChild(1).gameObject;
		keys = keyboard.transform.GetChild(2).gameObject;

		leftController = GameObject.FindGameObjectWithTag("LeftController");
        leftControllerEvents = leftController.GetComponent<VRTK_ControllerEvents>();
        rightController = GameObject.FindGameObjectWithTag("RightController");
        rightControllerEvents = rightController.GetComponent<VRTK_ControllerEvents>();
		buttonPressed = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress) || rightControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress)){
			if(!buttonPressed){
				ToggleOnOff();
			}
			buttonPressed = true;
		}else{
			buttonPressed = false;
		}
	}

	public void ToggleOnOff(){
		HUD.SetActive(!HUD.activeInHierarchy);
		keys.SetActive(!keys.activeInHierarchy);
	}
}
