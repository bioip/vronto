using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : MonoBehaviour {

	public GameObject text;
	public TextMesh t;
	public string description;

	// Use this for initialization
	void Start () {
		text = new GameObject();
		t = text.AddComponent<TextMesh>();
		t.text = description;
		t.anchor = TextAnchor.MiddleCenter;
		t.characterSize = 0.001f;
		t.fontSize = 200;
	}
	
	// Update is called once per frame
	void Update () {
		t.transform.position = transform.position;
	}
}
