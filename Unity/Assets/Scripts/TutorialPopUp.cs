using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class TutorialPopUp : MonoBehaviour {

	public GameObject tutorialPage;
	protected GameObject rightController;
	protected GameObject leftController;
    protected VRTK_ControllerEvents leftControllerEvents;
	protected VRTK_ControllerEvents rightControllerEvents;
	protected bool active;
	protected bool buttonPressed;

	// Use this for initialization
	void Start () {
		active = true;
		buttonPressed = false;
		tutorialPage.SetActive(active);
		leftController = GameObject.FindGameObjectWithTag("LeftController");
		leftControllerEvents = leftController.GetComponent<VRTK_ControllerEvents>();
		rightController = GameObject.FindGameObjectWithTag("RightController");
		rightControllerEvents = rightController.GetComponent<VRTK_ControllerEvents>();
	}
	
	// Update is called once per frame
	void Update () {
		if(leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress) || rightControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress)){
			if(!buttonPressed){
				active = !active;
				tutorialPage.SetActive(active);
			}
			buttonPressed = true;

		}else{
			buttonPressed = false;
		}


	}
}
