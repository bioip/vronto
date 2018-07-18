using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SearchBar : MonoBehaviour {

	public Dropdown DropdownList;
	private List<Dropdown.OptionData> m_Messages;
	private InputField input;
	private List<string> labels = new List<string>();
	private string outputString;
	private bool matched;

	// Use this for initialization
	void Start () {
		m_Messages = DropdownList.options;
		input = gameObject.GetComponent(typeof(InputField)) as InputField;
		foreach(Dropdown.OptionData message in m_Messages){
			labels.Add(message.text.ToUpper().Trim());
		}
		matched = false;

	}
	
	public void ClickKey () {
		if(input.text != ""){
			//DropdownList.Show();
			//outputString = input.text;
			matched = false;
			foreach(string label in labels){
				if(label.StartsWith(input.text.Trim())){
					if(!matched){
						DropdownList.value = labels.IndexOf(label);
						matched = true;
					}
				}
			}
		}
	}

	public void Enter() {
		/*
		int findValue = labels.IndexOf(outputString);
		if(findValue > -1){
			DropdownList.value = findValue;
		}
		*/
	}
}
