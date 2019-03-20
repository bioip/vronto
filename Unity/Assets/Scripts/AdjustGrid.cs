using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustGrid : MonoBehaviour {

	private float offset = 0.0f;

	private GameObject grid;

	// Use this for initialization
	void Start () {
		grid = this.gameObject.transform.GetChild(0).gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void AdjustOffset(float newOffset){
		offset = newOffset;
		Debug.Log("adjust grid: offset = " + offset);
		grid.SetActive(false);
		AxisXYZ axes = grid.GetComponent(typeof(AxisXYZ)) as AxisXYZ;
		axes.AdjustOffset(this.offset);
		grid.SetActive(true);

	}
}
