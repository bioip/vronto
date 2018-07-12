using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class InteractWithLabeledSphere : MonoBehaviour {

	private InputField input;
	private GameObject rightController;
	private IEnumerator coroutine;
	private GameObject sp;
	private Color[] colors;
	public List<string> labelList;
	private Dropdown DropDownList;
	public GameObject SphereGenerator;

	// Use this for initialization
	void Start () {
		input = gameObject.GetComponent(typeof(InputField)) as InputField;
		rightController = GameObject.FindGameObjectWithTag("RightController");
		colors = new Color[3] {Color.red, Color.magenta, Color.cyan};
		labelList = SphereGenerator.GetComponent<SpheresGenerator>().labelList;
		DropDownList = SphereGenerator.GetComponent<SpheresGenerator>().DropDownList;
	}
	
	public void FetchSphere(){
		//sp = GameObject.Find(input.text);
		//Debug.Log("dropdownlist value = " + DropDownList.value);
		sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
		if(sp != null){
			sp.transform.position = rightController.transform.position;
		}
	}

	public void LocateSphere(){
		//sp = GameObject.Find(input.text);	
		//Debug.Log("dropdownlist value = " + DropDownList.value);
		sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
		if(sp != null){
			sp.tag = "Sphere_blinking";
			coroutine = Blink(2.0f);
			StartCoroutine(coroutine);
		}
		
		
	}

	private IEnumerator Blink(float  waitTime){
		int i = 0;
		float endTime = Time.time + waitTime;
		while(Time.time < endTime){
			sp.GetComponent<VRTK_InteractableObject>().touchHighlightColor = colors[i % 3];
			sp.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
			i++;
			yield return new WaitForSeconds(0.2f);
		}
		sp.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
		sp.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
		sp.tag = "Sphere";
	}

	public void lastLabel(){
		int currentLabelIndex = labelList.IndexOf(input.text);
		if(currentLabelIndex > 0){
			input.text = labelList[currentLabelIndex - 1];
		}
	}

	public void NextLabel(){
		int currentLabelIndex = labelList.IndexOf(input.text);
		if(currentLabelIndex < labelList.Count - 1){
			input.text = labelList[currentLabelIndex + 1];
		}
	}
}
