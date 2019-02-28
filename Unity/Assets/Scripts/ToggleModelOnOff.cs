using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleModelOnOff : MonoBehaviour {

	private GameObject model;

	// Use this for initialization
	void Start () {
		model = this.gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		/*
		if(Input.GetKeyDown(KeyCode.O)){
			Debug.Log("Set model state");
			model.SetActive(!model.activeSelf);
		}
		*/
	}

	public void ModelOnOff(){
		model.SetActive(!model.activeSelf);
	}
}
