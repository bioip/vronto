using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;
using UnityEngine.UI;

public class SpheresGenerator : MonoBehaviour {

    private int[] objects;
    public CSV CSVManager;
    private int sphere_offset = 0;
    private int pageNum = 1;
    private List<GameObject> spheres = new List<GameObject>();
    public InputField PageInput;
    public InputField labelInput;

	// Use this for initialization
	void Start () {
        generate_spheres();
        PageInput.text = "P" + pageNum.ToString() + "/6";
	}

	private void generate_spheres() {
        for(int i = sphere_offset; i < sphere_offset + 450; i++)
        {
            if(i >= 2448)
            {
                return;
            }
            //generate spheres according to coord in csv file
            GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp.transform.parent = transform;
            sp.transform.position = new Vector3(float.Parse(CSVManager.GetRowList()[i].X), float.Parse(CSVManager.GetRowList()[i].Y), 
                                                float.Parse(CSVManager.GetRowList()[i].Z));
            sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            spheres.Add(sp);

            //disable shadow
            MeshRenderer mr = sp.GetComponent(typeof(MeshRenderer)) as MeshRenderer;
            mr.shadowCastingMode = ShadowCastingMode.Off;
            
            //enable sphere collider isTrigger
            SphereCollider sc = sp.GetComponent(typeof(SphereCollider)) as SphereCollider;
            sc.isTrigger = false;

            //add Rigidbody component
            Rigidbody rb = sp.AddComponent(typeof(Rigidbody)) as Rigidbody;
            rb.useGravity = false;
            rb.isKinematic = true;

            //add VRTK component
            VRTK_InteractableObject io = sp.AddComponent(typeof(VRTK_InteractableObject)) as VRTK_InteractableObject;
            //VRTK_FixedJointGrabAttach fjga = sp.AddComponent(typeof(VRTK_FixedJointGrabAttach)) as VRTK_FixedJointGrabAttach;
            VRTK_TrackObjectGrabAttach toga = sp.AddComponent(typeof(VRTK_TrackObjectGrabAttach)) as VRTK_TrackObjectGrabAttach;
            VRTK_SwapControllerGrabAction scga = sp.AddComponent(typeof(VRTK_SwapControllerGrabAction)) as VRTK_SwapControllerGrabAction;
            VRTK_InteractHaptics ih = sp.AddComponent(typeof(VRTK_InteractHaptics)) as VRTK_InteractHaptics;
            io.holdButtonToGrab = false;
            io.isGrabbable = true;
            io.touchHighlightColor = Color.yellow;
            //io.grabAttachMechanicScript = fjga;
            io.grabAttachMechanicScript = toga;
            io.secondaryGrabActionScript = scga;

            /*
            //attach text to spheres
            ObjectText ot = sp.AddComponent(typeof(ObjectText)) as ObjectText;
            ot.description = CSVManager.GetRowList()[i].Description;
            */

            //show the label in HUD when touched
            TouchedInput ti = sp.AddComponent(typeof(TouchedInput)) as TouchedInput;
            ti.input = labelInput;
            ti.label = CSVManager.GetRowList()[i].Description;
            ti.sp = sp;

            //move grabbed object
            // MoveGrabbedObject mgo = sp.AddComponent(typeof(MoveGrabbedObject)) as MoveGrabbedObject;
        }
    }

    private void deactivate_spheres() {
        foreach (GameObject sphere in spheres) 
        {
            Destroy(sphere);
        }
        spheres.Clear();
    }

	// Update is called once per frame
	void Update () {
		
	}
    public void increment_offset() {
        deactivate_spheres();
        sphere_offset += 450;
        pageNum += 1;
        if(sphere_offset >= 2449)
        {
            sphere_offset = 0;
            pageNum = 1;
        }
        generate_spheres();
        PageInput.text = "P" + pageNum.ToString() + "/6";
    }
}
