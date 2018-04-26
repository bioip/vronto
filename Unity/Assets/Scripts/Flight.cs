using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class Flight : MonoBehaviour
{

    public Transform head;
    public GameObject leftHand;
    public GameObject rightHand;
    public VRTK_ControllerEvents controllerEvents;
    private bool flightOn = false;

    private void Fly(bool flightOn)
    {
        if (flightOn)
        {
            Vector3 leftDir = leftHand.transform.position - head.position;
            Vector3 rightDir = rightHand.transform.position - head.position;

            Vector3 dir = leftDir + rightDir;

            head.position += dir;
            leftHand.transform.position += dir;
            rightHand.transform.position += dir;
        }
    }

    private void Start()
    {
        head = VRTK_DeviceFinder.HeadsetTransform();
        leftHand = VRTK_DeviceFinder.GetControllerLeftHand();
        rightHand = VRTK_DeviceFinder.GetControllerRightHand();
        controllerEvents = (controllerEvents != null ? controllerEvents : GetComponentInParent<VRTK_ControllerEvents>());
        if(controllerEvents == null)
        {
            Debug.Log("Cannot find controller events script in Parent");
        }
        else
        {
            Debug.Log("Found controller events script in Parent");
        }
    }

    private void Update()
    {
        // Debug.Log(leftHand.transform.position);
        // Debug.Log(rightHand.transform.position);
        // Debug.Log(head.position);
        if (controllerEvents.gripPressed)
        {
            Debug.Log("Flying");
            flightOn = !flightOn;
            Fly(flightOn);
        }
    }
}