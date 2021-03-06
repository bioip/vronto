﻿using System.Collections;
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

/// <summary>
/// This class contains the script to generate the spheres using the given data file
/// </summary>
public class SpheresGenerator : MonoBehaviour
{

    public CSV CSVManager;  // The CSV manager for the data csv file
    public CSVRelationship CSVManagerRelationship;  // The CSV manager for the relationship data csv file
    public int pageNum = 1; // The current page number of the shelf
    public List<GameObject> spheres = new List<GameObject>();   // The list of spheres
    public List<string> labelList = new List<string>(); // The list of labels
    public List<string> m_dropdownList = new List<string>();    // The list of dropdown list items
    public InputField PageInput;    // The Inputfield for showing page number
    public InputField labelInput;   // The Inputfield for showing the selected sphere's label
    public Dropdown DropDownList;   // The UI dropdown list
    public Text currSelected;   // The text for currently grabbed/selected sphere label

    private List<string[]> rowData = new List<string[]>();  // The list for saving data as a csv file

    // Use this for initialization
    void Start()
    {
        SetDropdownList();
        generate_spheres();
        FormSet();
        PageInput.text = "P" + pageNum.ToString() + "/6";
    }

    /// <summary>
    /// Set up the dropdown list of the data
    /// </summary>
    private void SetDropdownList()
    {


        for (int i = 0; i < CSVManager.GetRowList().Count; i++)
        {
            m_dropdownList.Add(CSVManager.GetRowList()[i].Label);
        }

    }

    /// <summary>
    /// Generate the spheres and attach all the components
    /// </summary>
    private void generate_spheres()
    {


        for (int i = 0; i < 2448; i++)
        {

            //generate spheres according to coord in csv file
            GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sp.transform.parent = transform;
            sp.transform.position = new Vector3(float.Parse(CSVManager.GetRowList()[i].X), float.Parse(CSVManager.GetRowList()[i].Y),
                                                float.Parse(CSVManager.GetRowList()[i].Z));
            sp.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            sp.name = CSVManager.GetRowList()[i].Label;
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
            ti.label = CSVManager.GetRowList()[i].Label;
            ti.sp = sp;
            ti.labelList = labelList;
            ti.input_dropdown = DropDownList;
            ti.m_dropdownList = m_dropdownList;
            ti.currSelected = currSelected;

            //Add Parent attributes
            Parent parent = sp.AddComponent(typeof(Parent)) as Parent;
            parent.id = CSVManager.GetRowList()[i].ID;

            //add connectSphere component
            ConnectSpheres cs = sp.AddComponent(typeof(ConnectSpheres)) as ConnectSpheres;
            cs.spheres = spheres;

            if (i >= pageNum * 450)
            {
                sp.SetActive(false);
            }



        }

    }

    /// <summary>
    /// Form the set using the relationships data
    /// </summary>
    private void FormSet()
    {
        // form set based on part_of relationship
        foreach (GameObject sphere in spheres)
        {
            List<CSVRelationship.Row> AllID = CSVManagerRelationship.FindAll_ID(sphere.GetComponent<Parent>().id);
            foreach (CSVRelationship.Row row in AllID)
            {
                // part_of relatinoship
                if (row.Relationship == "BFO:0000050")
                {
                    GameObject targetSphere = spheres.Find(x => x.GetComponent<Parent>().id == row.Target_ID);
                    if (targetSphere != null)
                        targetSphere.GetComponent<Parent>().descendents.Add(sphere);
                }
            }
        }

        // modify set based on is_a relationship
        foreach (GameObject sphere in spheres)
        {
            List<CSVRelationship.Row> AllID = CSVManagerRelationship.FindAll_ID(sphere.GetComponent<Parent>().id);
            foreach (CSVRelationship.Row row in AllID)
            {
                // part_of relatinoship
                if (row.Relationship == "is_a")
                {
                    GameObject targetSphere = spheres.Find(x => x.GetComponent<Parent>().id == row.Target_ID);
                    if (targetSphere != null)
                    {
                        if (sphere.GetComponent<Parent>().descendents.Count < targetSphere.GetComponent<Parent>().descendents.Count)
                        {
                            sphere.GetComponent<Parent>().descendents = targetSphere.GetComponent<Parent>().descendents;
                        }
                        else
                        {
                            targetSphere.GetComponent<Parent>().descendents = sphere.GetComponent<Parent>().descendents;
                        }
                    }

                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {



    }

    /// <summary>
    /// Call when quitting the application
    /// </summary>
    void OnApplicationQuit()
    {
        Debug.Log("Application quit");
        SaveToCSV();
    }

    /// <summary>
    /// Go to the next page of the shelf
    /// </summary>
    public void NextPage()
    {
        pageNum += 1;
        if ((pageNum - 1) * 450 >= 2449)
        {
            pageNum = 1;
        }
        PageInput.text = "P" + pageNum.ToString() + "/6";

        for (int i = 0; i < spheres.Count; i++)
        {
            if (i >= (pageNum - 1) * 450 && i < pageNum * 450)
            {

                spheres[i].SetActive(true);
            }
            else if (spheres[i].tag != "SphereInModel")
            {
                spheres[i].SetActive(false);
            }

        }
    }

    public void showLabelList()
    {
        foreach (string label in labelList)
        {
            Debug.Log("label = " + label);
        }
    }

    /// <summary>
    /// Save the coordinates and placed data onto the saved_data file
    /// </summary>
    private void SaveToCSV()
    {
        Debug.Log("Start saving to csv file");
        string[] rowDataTmp = new string[6];
        rowDataTmp[0] = "ID";
        rowDataTmp[1] = "X";
        rowDataTmp[2] = "Y";
        rowDataTmp[3] = "Z";
        rowDataTmp[4] = "Label";
        rowDataTmp[5] = "Placed";
        rowData.Add(rowDataTmp);

        for (int i = 0; i < 2448; i++)
        {
            GameObject sp = spheres.Find(sphere => sphere.name == CSVManager.GetRowList()[i].Label);
            rowDataTmp = new string[6];
            rowDataTmp[0] = CSVManager.GetRowList()[i].ID;
            rowDataTmp[1] = "" + sp.transform.position.x;
            rowDataTmp[2] = "" + sp.transform.position.y;
            rowDataTmp[3] = "" + sp.transform.position.z;
            rowDataTmp[4] = CSVManager.GetRowList()[i].Label;

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
            if (i != length - 1)
                sb.Append(string.Join(delimiter, output[i]) + "\n");
            else
                sb.Append(string.Join(delimiter, output[i]));
        }

        string filePath = Application.dataPath + "/data/" + "Saved_data.csv";

        StreamWriter outStream = System.IO.File.CreateText(filePath);
        outStream.WriteLine(sb);
        outStream.Close();
    }
}
