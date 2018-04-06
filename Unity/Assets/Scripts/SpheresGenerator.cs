using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpheresGenerator : MonoBehaviour {

    private int[] objects;
    public CSV CSVManager;

	// Use this for initialization
	void Start () {


        Debug.Log(CSVManager.NumRows());

        
        for(int i = 0; i < CSVManager.NumRows(); i++)
        {
            GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp.transform.position = new Vector3(float.Parse(CSVManager.GetRowList()[i].X), float.Parse(CSVManager.GetRowList()[i].Y), 
                                                float.Parse(CSVManager.GetRowList()[i].Z));

        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
