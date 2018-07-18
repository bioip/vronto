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
    public List<GameObject> spheres = new List<GameObject>();
    public List<string> labelList = new List<string>();
    public List<string> m_dropdownList = new List<string>();
    public InputField PageInput;
    public InputField labelInput;
    public Dropdown DropDownList;

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
                m_dropdownList.Sort();
                DropDownList.AddOptions(m_dropdownList);
                return;
            }
            //generate spheres according to coord in csv file
            GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp.transform.parent = transform;
            sp.transform.position = new Vector3(float.Parse(CSVManager.GetRowList()[i].X), float.Parse(CSVManager.GetRowList()[i].Y), 
                                                float.Parse(CSVManager.GetRowList()[i].Z));
            sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sp.name = CSVManager.GetRowList()[i].Description;
            sp.tag = "Sphere";
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
            VRTK_TrackObjectGrabAttach toga = sp.AddComponent(typeof(VRTK_TrackObjectGrabAttach)) as VRTK_TrackObjectGrabAttach;
            VRTK_SwapControllerGrabAction scga = sp.AddComponent(typeof(VRTK_SwapControllerGrabAction)) as VRTK_SwapControllerGrabAction;
            VRTK_InteractHaptics ih = sp.AddComponent(typeof(VRTK_InteractHaptics)) as VRTK_InteractHaptics;
            io.holdButtonToGrab = false;
            io.isGrabbable = true;
            io.touchHighlightColor = Color.yellow;
            io.grabAttachMechanicScript = toga;
            
            //set detach distance
            toga.detachDistance = 5.0f;

            //use precision grab
            toga.precisionGrab = true;
            
            io.secondaryGrabActionScript = scga;

            //show the label in HUD when touched
            TouchedInput ti = sp.AddComponent(typeof(TouchedInput)) as TouchedInput;
            ti.input = labelInput;
            ti.label = CSVManager.GetRowList()[i].Description;
            ti.sp = sp;
            ti.labelList = labelList;
            ti.input_dropdown = DropDownList;
            ti.m_dropdownList = m_dropdownList;

            //add connectSphere component
            ConnectSpheres cs = sp.AddComponent(typeof(ConnectSpheres)) as ConnectSpheres;
            cs.spheres = spheres;

            //Add the label to the dropdown list
            m_dropdownList.Add(CSVManager.GetRowList()[i].Description);


        }
        m_dropdownList.Sort();
        DropDownList.AddOptions(m_dropdownList);
    }

    private void deactivate_spheres() {
        foreach (GameObject sphere in spheres) 
        {
            Destroy(sphere);
        }
        spheres.Clear();
        m_dropdownList.Clear();
        DropDownList.ClearOptions();
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

    public void showLabelList() {
        foreach (string label in labelList){
            Debug.Log("label = " + label);
        }
    }
}
