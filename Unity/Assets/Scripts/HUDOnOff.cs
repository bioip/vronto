using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDOnOff : MonoBehaviour {

	private GameObject HUD;

	// Use this for initialization
	void Start () {
		HUD = this.gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ToggleOnOff(){
		HUD.SetActive(!HUD.activeSelf);
	}
}
