using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using VRTK;

public class ObjectText : MonoBehaviour {

	public GameObject text;
	public TextMesh t;
	public string description;
    private Transform lookTowards;

	void Start () {
        lookTowards = VRTK_DeviceFinder.HeadsetTransform();
        text = new GameObject();
        text.name = "Text";
		t = text.AddComponent<TextMesh>();
		t.text = description;
		t.anchor = TextAnchor.MiddleCenter;
		t.characterSize = 0.005f;
		t.fontSize = 200;
        t.color = Color.black;
        text.transform.parent = transform;
        text.transform.position = transform.position + new Vector3(0, 0.11f, 0);
        text.transform.rotation = Quaternion.Euler(0, 0, 0);
        /*
        try
        {
            text.transform.rotation = Quaternion.FromToRotation(new Vector3(text.transform.position.x, 0, text.transform.position.z), new Vector3(lookTowards.position.x, 0, lookTowards.position.z));
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Can't find head");
        }
        */
    }
	
	void Update () {
        /*
        lookTowards = VRTK_DeviceFinder.HeadsetTransform();
        // t.transform.position = transform.position + new Vector3(0, 0.11f, 0);
        try
        {
            text.transform.rotation = Quaternion.FromToRotation(new Vector3(text.transform.position.x, 0, text.transform.position.z), new Vector3(lookTowards.position.x, 0, lookTowards.position.z));
        }
        catch (NullReferenceException e)
        {
            Debug.Log("Can't find head");
        }
        */
    }
}
