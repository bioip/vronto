using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// This class contains the script to turn the tutorial page on/off
/// </summary>
public class TutorialPopUp : MonoBehaviour
{

    public GameObject tutorialPage;     // The tutorial page
    protected GameObject rightController;   // The right hand controller
    protected GameObject leftController;    // The left hand controller
    protected VRTK_ControllerEvents leftControllerEvents;   // The left hand controller events used to identify button pressed
    protected VRTK_ControllerEvents rightControllerEvents;  // The right hand controller events used to identify button pressed
    protected bool active;  // Whether the tutorial page is active
    protected bool buttonPressed;   // Whether the button has been pressed

    // Use this for initialization
    void Start()
    {
        active = true;
        buttonPressed = false;
        tutorialPage.SetActive(active);
        leftController = GameObject.FindGameObjectWithTag("LeftController");
        leftControllerEvents = leftController.GetComponent<VRTK_ControllerEvents>();
        rightController = GameObject.FindGameObjectWithTag("RightController");
        rightControllerEvents = rightController.GetComponent<VRTK_ControllerEvents>();
    }

    // Update is called once per frame
    void Update()
    {
        // Detect if the user pressed the menu button on the controller
        if (leftControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress) || rightControllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.ButtonTwoPress))
        {
            if (!buttonPressed)
            {
                active = !active;
                tutorialPage.SetActive(active);
            }
            buttonPressed = true;

        }
        else
        {
            buttonPressed = false;
        }


    }
}
