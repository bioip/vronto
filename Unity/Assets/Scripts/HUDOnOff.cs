using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

/// <summary>
/// This class contains the script to turn the HUD on/off
/// </summary>
public class HUDOnOff : MonoBehaviour
{

    private GameObject HUD;     // The HUD gameobject
    private GameObject keyboard;    // The keyboard gameobject
    private GameObject input;   // The input on the keyboard
    private GameObject keys;    // The keys on the keyboard
    private GameObject rightController;		// The right hand controller
    private GameObject leftController;		// The left hand controller
    private VRTK_ControllerEvents leftControllerEvents;		// The left hand controller events used to identify button pressed
    private VRTK_ControllerEvents rightControllerEvents;    // The right hand controller events used to identify button pressed
    private bool buttonPressed;     // Whther the button is pressed

    // Use this for initialization
    void Start()
    {
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
    void Update()
    {

        //Check if the user press the grip of the controller
        if (leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress) || rightControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.GripPress))
        {
            if (!buttonPressed)
            {
                ToggleOnOff();
            }
            buttonPressed = true;
        }
        else
        {
            buttonPressed = false;
        }
    }

    /// <summary>
    /// Turn the HUD on/off
    /// </summary>
    public void ToggleOnOff()
    {
        HUD.SetActive(!HUD.activeInHierarchy);
        keys.SetActive(!keys.activeInHierarchy);
    }
}
