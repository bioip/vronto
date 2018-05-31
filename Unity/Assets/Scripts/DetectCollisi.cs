using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DetectCollisi : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Debug.Log("detection started");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("update works");
	}

	void OnTriggerStay(Collider other){
		if(other.name == "Sphere"){
			//Debug.Log("Triggered!!!");
			other.gameObject.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.green;
			other.gameObject.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
		}
	}

	void OnTriggerExit(Collider other){
		if(other.name == "Sphere"){
			//Debug.Log("Trigger Exit");
			other.gameObject.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
			other.gameObject.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
		}
	}


	
}
