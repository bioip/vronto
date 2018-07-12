using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class TouchedInput : MonoBehaviour {

	public InputField input;
	public Dropdown input_dropdown;
	public string label;
	public GameObject rightController;
	public GameObject sp;
	public List<string> labelList;
	public List<string> m_dropdownList;

	// Use this for initialization
	void Start () {
		rightController = GameObject.FindGameObjectWithTag("RightController");
	}
	
	// Update is called once per frame
	void Update () {
		VRTK_InteractableObject io = GetComponent(typeof(VRTK_InteractableObject)) as VRTK_InteractableObject;
		//Debug.Log("touchpadControl enabled = " + rightController.GetComponent<VRTK_TouchpadControl>().enabled);
		if(io.IsTouched()){
			//Debug.Log("is touched");

			//output sphere label to HUD inputfield
			input.text = label;
			input_dropdown.value = m_dropdownList.IndexOf(label);
		}

		if(io.IsGrabbed()){
			//rightController.GetComponent<VRTK_TouchpadControl>().enabled = true;
			//Debug.Log("touchpadControl status = " + rightController.GetComponent<VRTK_TouchpadControl>().enabled);
			
			//overide control object to sphere
			//rightController.GetComponent<VRTK_TouchpadControl>().controlOverrideObject = sp;
			
			//Debug.Log("sphere position: " + sp.transform.position);
			input.text = label;
			if(!labelList.Contains(label)){
				labelList.Add(label);
			}else{
				/*
				labelList.Remove(label);
				labelList.Add(label);
				*/
			}

		}else{
			//rightController.GetComponent<VRTK_TouchpadControl>().enabled = false;
		}
	}

}
