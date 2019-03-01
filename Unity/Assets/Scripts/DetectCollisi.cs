using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class DetectCollisi : MonoBehaviour {

	private float x0 = 36.45f;
	private float y0 = -6.85f;
	private float z0 = 9.35f;
	private float offset = 2.5f;

	// Use this for initialization
	void Start () {
		Debug.Log("detection started");
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("update works");
	}

	//detect if sphere is within model
	void OnTriggerStay(Collider other){
		if(other.tag == "Sphere" || other.tag == "SphereInModel"){
			//Debug.Log("Triggered!!!");
			GameObject sphere = other.gameObject;

			sphere.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.green;
			sphere.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
			other.tag = "SphereInModel";

			//snapping to the grid

			float x = sphere.transform.position.x;
			float y = sphere.transform.position.y;
			float z = sphere.transform.position.z;

			float i = Mathf.Floor(Mathf.Abs((x - x0) / offset));
			float j = Mathf.Floor(Mathf.Abs((y - y0) / offset));
			float k = Mathf.Floor(Mathf.Abs((z - z0) / offset));

			sphere.transform.position = new Vector3( (x0 + offset*i) + offset/2, (y0 + offset*j) + offset/2, (z0 - offset*k) - offset/2);

		}
	}

	//detect if sphere has exited model
	void OnTriggerExit(Collider other){
		if(other.tag == "Sphere" || other.tag == "SphereInModel"){
			//Debug.Log("Trigger Exit");
			GameObject sphere = other.gameObject;

			sphere.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
			sphere.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
			other.tag = "Sphere";
		}
	}


	
}
