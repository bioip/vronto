using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using VRTK;
using VRTK.GrabAttachMechanics;
using VRTK.SecondaryControllerGrabActions;
using UnityEngine.UI;

public class SpheresGenerator : MonoBehaviour
{

    private int[] objects;
    public CSV CSVManager;
    public CSVRelationship CSVManagerRelationship;
    private int sphere_offset = 0;
    public int pageNum = 1;
    public List<GameObject> spheres = new List<GameObject>();
    public List<string> labelList = new List<string>();
    public List<string> m_dropdownList = new List<string>();
    public InputField PageInput;
    public InputField labelInput;
    public Dropdown DropDownList;

    private List<string[]> rowData = new List<string[]>();

    // Use this for initialization
    void Start()
    {
        SetDropdownList();
        generate_spheres();
        FormSet();
        PageInput.text = "P" + pageNum.ToString() + "/6";
    }

    private void SetDropdownList(){
        

        for(int i = 0; i < CSVManager.GetRowList().Count; i++){
            m_dropdownList.Add(CSVManager.GetRowList()[i].Description);
        }

        m_dropdownList.Sort();
        DropDownList.AddOptions(m_dropdownList);
        Debug.Log("DropDownList size = " + DropDownList.options.Count);
    }

    private void generate_spheres()
    {


        for (int i = sphere_offset; i < sphere_offset + 450; i++)
        {
            if (i >= 2448)
            {

                //Add the label to the dropdown list
                /*
                foreach (GameObject sphere in spheres)
                {
                    m_dropdownList.Add(sphere.name);
                }

                m_dropdownList.Sort();
                DropDownList.AddOptions(m_dropdownList);
                */
                return;
            }

            if (spheres.Exists(x => x.name == CSVManager.GetRowList()[i].Description))
            {
                continue;
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

            //Add Parent attributes
            Parent parent = sp.AddComponent(typeof(Parent)) as Parent;
            parent.id = CSVManager.GetRowList()[i].ID;

            //add connectSphere component
            ConnectSpheres cs = sp.AddComponent(typeof(ConnectSpheres)) as ConnectSpheres;
            cs.spheres = spheres;


        }

        //Add the label to the dropdown list
        /*
        foreach (GameObject sphere in spheres)
        {
            m_dropdownList.Add(sphere.name);
        }

        m_dropdownList.Sort();
        DropDownList.AddOptions(m_dropdownList);
        */
    }

    private void FormSet(){
        // form set based on part_of relationship
        foreach(GameObject sphere in spheres){
            List<CSVRelationship.Row> AllID = CSVManagerRelationship.FindAll_ID(sphere.GetComponent<Parent>().id);
            foreach(CSVRelationship.Row row in AllID){
                // part_of relatinoship
                if(row.Relationship == "BFO:0000050"){
                    GameObject targetSphere = spheres.Find(x => x.GetComponent<Parent>().id == row.Target_ID);
                    if(targetSphere != null)
                        targetSphere.GetComponent<Parent>().descendents.Add(sphere);
                }
            }
        }

        // modify set based on is_a relationship
        foreach(GameObject sphere in spheres){
            List<CSVRelationship.Row> AllID = CSVManagerRelationship.FindAll_ID(sphere.GetComponent<Parent>().id);
            foreach(CSVRelationship.Row row in AllID){
                // part_of relatinoship
                if(row.Relationship == "is_a"){
                    GameObject targetSphere = spheres.Find(x => x.GetComponent<Parent>().id == row.Target_ID);
                    if(targetSphere != null){
                        if(sphere.GetComponent<Parent>().descendents.Count < targetSphere.GetComponent<Parent>().descendents.Count){
                            sphere.GetComponent<Parent>().descendents = targetSphere.GetComponent<Parent>().descendents;
                        }else{
                            targetSphere.GetComponent<Parent>().descendents = sphere.GetComponent<Parent>().descendents;
                        }
                    }
                        
                }
            }
        }
    }

    private void deactivate_spheres()
    {

        List<GameObject> spheresToKeep = new List<GameObject>();

        foreach (GameObject sphere in spheres)
        {

            if (sphere.tag == "SphereInModel")
            {

                spheresToKeep.Add(sphere);
                continue;

            }

            Destroy(sphere);
        }


        spheres.Clear();

        foreach (GameObject sphere in spheresToKeep)
        {
            spheres.Add(sphere);
        }

        spheresToKeep.Clear();

        /*
        m_dropdownList.Clear();
        DropDownList.ClearOptions();
        */
    }

    // Update is called once per frame
    void Update()
    {

        // For debug purpose
        /*
		if(Input.GetKeyDown(KeyCode.N)){
			Debug.Log("Next page");
			increment_offset();
		}
        */

    }

    void OnApplicationQuit()
    {
        Debug.Log("Application quit");
        SaveToCSV();
    }

    public void increment_offset()
    {
        deactivate_spheres();
        sphere_offset += 450;
        pageNum += 1;
        if (sphere_offset >= 2449)
        {
            sphere_offset = 0;
            pageNum = 1;
        }
        generate_spheres();
        FormSet();
        PageInput.text = "P" + pageNum.ToString() + "/6";
    }

    public void showLabelList()
    {
        foreach (string label in labelList)
        {
            Debug.Log("label = " + label);
        }
    }

    private void SaveToCSV()
    {
        Debug.Log("Start saving to csv file");
        string[] rowDataTmp = new string[6];
        rowDataTmp[0] = "URI";
        rowDataTmp[1] = "label";
        rowDataTmp[2] = "X";
        rowDataTmp[3] = "Y";
        rowDataTmp[4] = "Z";
        rowDataTmp[5] = "placed";
        rowData.Add(rowDataTmp);

        for (int i = 0; i < 2448; i++)
        {
            rowDataTmp = new string[6];
            rowDataTmp[0] = CSVManager.GetRowList()[i].ID;
            rowDataTmp[1] = CSVManager.GetRowList()[i].Description;
            rowDataTmp[2] = CSVManager.GetRowList()[i].X;
            rowDataTmp[3] = CSVManager.GetRowList()[i].Y;
            rowDataTmp[4] = CSVManager.GetRowList()[i].Z;
            GameObject sp = spheres.Find(sphere => sphere.name == CSVManager.GetRowList()[i].Description);
            if (sp != null && sp.tag == "SphereInModel")
            {
                rowDataTmp[5] = "" + true;
            }
            else
            {
                rowDataTmp[5] = "" + false;
            }
            rowData.Add(rowDataTmp);
        }

        string[][] output = new string[rowData.Count][];

        for (int i = 0; i < output.Length; i++)
        {
            output[i] = rowData[i];
        }

        int length = output.GetLength(0);
        string delimiter = ",";
        StringBuilder sb = new StringBuilder();

        for (int i = 0; i < length; i++)
        {
            sb.AppendLine(string.Join(delimiter, output[i]));
        }

        string filePath = Application.dataPath + "/data/" + "Saved_data.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
}
