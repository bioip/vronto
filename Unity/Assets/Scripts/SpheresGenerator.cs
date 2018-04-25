using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;

public class SpheresGenerator : MonoBehaviour {

    private int[] objects;
    public CSV CSVManager;

	// Use this for initialization
	void Start () {


        Debug.Log(CSVManager.NumRows());

        // Generating 64 Spheres for visualization purposes
        for(int i = 0; i <= 507; i += 169)
        {
            for(int j = i; j <= i + 39; j += 13)
            {
                for(int k = j; k <= j + 3; k++)
                {
                    //generate spheres according to coord in csv file
                    GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
                    sp.transform.parent = transform;
                    sp.transform.position = new Vector3(float.Parse(CSVManager.GetRowList()[k].X), float.Parse(CSVManager.GetRowList()[k].Y), 
                                                        float.Parse(CSVManager.GetRowList()[k].Z));
                    sp.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
                    
                    //disable shadow
                    MeshRenderer mr = sp.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
                    mr.shadowCastingMode = ShadowCastingMode.Off;
                    

                    //add Rigidbody component
                    Rigidbody rb = sp.AddComponent(typeof(Rigidbody)) as Rigidbody;
                    rb.useGravity = false;
                    rb.isKinematic = true;

                    //add VRTK component
                    VRTK_InteractableObject io = sp.AddComponent(typeof(VRTK_InteractableObject)) as VRTK_InteractableObject;
                    VRTK_FixedJointGrabAttach fjga = sp.AddComponent(typeof(VRTK_FixedJointGrabAttach)) as VRTK_FixedJointGrabAttach;
                    VRTK_SwapControllerGrabAction scga = sp.AddComponent(typeof(VRTK_SwapControllerGrabAction)) as VRTK_SwapControllerGrabAction;
                    VRTK_InteractHaptics ih = sp.AddComponent(typeof(VRTK_InteractHaptics)) as VRTK_InteractHaptics;
                    io.holdButtonToGrab = false;
                    io.isGrabbable = true;
                    io.touchHighlightColor = Color.yellow;
                    io.grabAttachMechanicScript = fjga;
                    io.secondaryGrabActionScript = scga;

                    //attach text to spheres
                    ObjectText ot = sp.AddComponent(typeof(ObjectText)) as ObjectText;
                    ot.description = CSVManager.GetRowList()[k].Description;
                }
            }
            

        }
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
