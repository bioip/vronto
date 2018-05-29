using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class TouchedInput : MonoBehaviour {

	public InputField input;
	public string label;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		VRTK_InteractableObject io = GetComponent(typeof(VRTK_InteractableObject)) as VRTK_InteractableObject;
		if(io.IsTouched()){
			//Debug.Log("is touched");
			input.text = label;
		}
	}
}
