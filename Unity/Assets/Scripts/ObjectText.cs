using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectText : MonoBehaviour {

	public GameObject text;
	public TextMesh t;
	public string description;
    private Transform lookTowards;
    public GameObject player; 
	
	void Start () {
        // lookTowards = Camera.main.transform;
        // lookTowards = (Transform)GameObject.Find("VRTK").transform.Find("Player").Find("Body").Find("Camera (eye)");
        text = new GameObject();
        text.name = "Text";
		t = text.AddComponent<TextMesh>();
		t.text = description;
		t.anchor = TextAnchor.MiddleCenter;
		t.characterSize = 0.001f;
		t.fontSize = 200;
        t.color = Color.black;
        text.transform.parent = transform;
        text.transform.position = transform.position + new Vector3(0, 0.11f, 0);
        text.transform.rotation = Quaternion.Euler(0, 0, 0);
        // text.transform.rotation = Quaternion.FromToRotation(new Vector3(text.transform.position.x, 0, text.transform.position.z), new Vector3(lookTowards.position.x, 0, lookTowards.position.z));
    }
	
	void Update () {
        Debug.Log(player.transform.position);
	    // t.transform.position = transform.position + new Vector3(0, 0.11f, 0);
        // t.transform.rotation = Quaternion.FromToRotation(new Vector3(t.transform.position.x, 0, t.transform.position.z), new Vector3(lookTowards.transform.position.x, 0, lookTowards.transform.position.z));
    }
}
