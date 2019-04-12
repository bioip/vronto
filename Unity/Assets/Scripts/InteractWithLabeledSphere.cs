﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class InteractWithLabeledSphere : MonoBehaviour
{

    private InputField input;
    private GameObject rightController;
    private IEnumerator coroutine;
    private GameObject sp;
    private Color[] colors;
    public List<string> labelList;
    private Dropdown DropDownList;
    public GameObject SphereGenerator;
    private bool showingSet;

    // Use this for initialization
    void Start()
    {
        input = gameObject.GetComponent(typeof(InputField)) as InputField;
        rightController = GameObject.FindGameObjectWithTag("RightController");
        colors = new Color[3] { Color.red, Color.magenta, Color.cyan };
        labelList = SphereGenerator.GetComponent<SpheresGenerator>().labelList;
        DropDownList = SphereGenerator.GetComponent<SpheresGenerator>().DropDownList;
        showingSet = false;
    }

    public void FetchSphere()
    {
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        if (sp != null)
        {
            sp.transform.position = rightController.transform.position + rightController.transform.forward;
        } else {
            int page = DropDownList.value / 450;
            int curPage = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
            for(int i = curPage; i <= page; i++){
                SphereGenerator.GetComponent<SpheresGenerator>().NextPage();
            }
            sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
            sp.transform.position = rightController.transform.position + rightController.transform.forward;
        }
    }

    public void LocateSphere()
    {
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        if (sp != null)
        {
            sp.tag = "Sphere_blinking";
            coroutine = Blink(3.0f, sp);
            StartCoroutine(coroutine);

            coroutine = Resize(3.0f, sp);
            StartCoroutine(coroutine);
        } else {
            int page = DropDownList.value / 450;
            int curPage = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
            for(int i = curPage; i <= page; i++){
                SphereGenerator.GetComponent<SpheresGenerator>().NextPage();
            }
            sp = GameObject.Find(DropDownList.options[DropDownList.value].text);

            sp.tag = "Sphere_blinking";
            coroutine = Blink(3.0f, sp);
            StartCoroutine(coroutine);

            coroutine = Resize(3.0f, sp);
            StartCoroutine(coroutine);
        }


    }

    private IEnumerator Blink(float waitTime, GameObject go)
    {
        int i = 0;
        float endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            go.GetComponent<VRTK_InteractableObject>().touchHighlightColor = colors[i % 3];
            go.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
            i++;
            yield return new WaitForSeconds(0.3f);
        }
        go.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
        go.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
        go.tag = "Sphere";
    }

    private IEnumerator Resize(float waitTime, GameObject go)
    {
        float endTime = Time.time + waitTime;
        bool enlarge = true;
        Vector3 originalScale = go.transform.localScale;
        while (Time.time < endTime)
        {
            if (enlarge)
            {
                go.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                enlarge = false;
            }
            else
            {
                go.transform.localScale = originalScale;
                enlarge = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
        go.transform.localScale = originalScale;
    }

    public void ShowSet(){
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        if(sp != null){
            showingSet = !showingSet;
            if(showingSet){
                List<GameObject> descendents = sp.GetComponent<Parent>().descendents;
                foreach(GameObject sphere in SphereGenerator.GetComponent<SpheresGenerator>().spheres){
                    if(descendents.Contains(sphere)){
                        sphere.SetActive(true);
                        sphere.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
                        sphere.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
                    }else{
                        sphere.SetActive(false);
                    }
                }

                sp.SetActive(true);
                sp.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
                sp.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);

            }else{
                int pageNum = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
                List<GameObject> spheres = SphereGenerator.GetComponent<SpheresGenerator>().spheres;
                for(int i = 0; i < spheres.Count; i++){
                    spheres[i].GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
                    if(i >= (pageNum - 1) * 450 && i < pageNum * 450){
                        spheres[i].SetActive(true);
                    }else if(spheres[i].tag != "SphereInModel"){
                        spheres[i].SetActive(false);
                    }

                }
            }
        }
    }

    void Update(){

        // Debug Use
        /* 
        if(Input.GetKeyDown(KeyCode.S)){
			Debug.Log("Show set");
			ShowSet();
		}
        */
    }
}
