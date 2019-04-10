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

    // Use this for initialization
    void Start()
    {
        input = gameObject.GetComponent(typeof(InputField)) as InputField;
        rightController = GameObject.FindGameObjectWithTag("RightController");
        colors = new Color[3] { Color.red, Color.magenta, Color.cyan };
        labelList = SphereGenerator.GetComponent<SpheresGenerator>().labelList;
        DropDownList = SphereGenerator.GetComponent<SpheresGenerator>().DropDownList;
    }

    public void FetchSphere()
    {
        //sp = GameObject.Find(input.text);
        //Debug.Log("dropdownlist value = " + DropDownList.value);
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        Debug.Log("sp = " + sp);
        if (sp != null)
        {
            sp.transform.position = rightController.transform.position + rightController.transform.forward;
        } else {
            Debug.Log("dropdownlist value = " + DropDownList.value);
            int page = DropDownList.value / 450;
            int curPage = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
            for(int i = curPage; i <= page; i++){
                SphereGenerator.GetComponent<SpheresGenerator>().increment_offset();
                Debug.Log("next page");
            }
            Debug.Log("value = " + DropDownList.options[DropDownList.value].text);
            sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
            sp.transform.position = rightController.transform.position + rightController.transform.forward;
        }
    }

    public void LocateSphere()
    {
        //sp = GameObject.Find(input.text);	
        //Debug.Log("dropdownlist value = " + DropDownList.value);
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        Debug.Log("sp = " + sp);
        if (sp != null)
        {
            sp.tag = "Sphere_blinking";
            coroutine = Blink(3.0f);
            StartCoroutine(coroutine);

            coroutine = Resize(3.0f);
            StartCoroutine(coroutine);
        } else {
            int page = DropDownList.value / 450;
            int curPage = SphereGenerator.GetComponent<SpheresGenerator>().pageNum;
            for(int i = curPage; i <= page; i++){
                SphereGenerator.GetComponent<SpheresGenerator>().increment_offset();
                Debug.Log("next page");
            }
            Debug.Log("value = " + DropDownList.options[DropDownList.value].text);
            sp = GameObject.Find(DropDownList.options[DropDownList.value].text);

            sp.tag = "Sphere_blinking";
            coroutine = Blink(3.0f);
            StartCoroutine(coroutine);

            coroutine = Resize(3.0f);
            StartCoroutine(coroutine);
        }


    }

    private IEnumerator Blink(float waitTime)
    {
        int i = 0;
        float endTime = Time.time + waitTime;
        while (Time.time < endTime)
        {
            sp.GetComponent<VRTK_InteractableObject>().touchHighlightColor = colors[i % 3];
            sp.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
            i++;
            yield return new WaitForSeconds(0.3f);
        }
        sp.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
        sp.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
        sp.tag = "Sphere";
    }

    private IEnumerator Resize(float waitTime)
    {
        float endTime = Time.time + waitTime;
        bool enlarge = true;
        Vector3 originalScale = sp.transform.localScale;
        while (Time.time < endTime)
        {
            if (enlarge)
            {
                sp.transform.localScale = new Vector3(2.0f, 2.0f, 2.0f);
                enlarge = false;
            }
            else
            {
                sp.transform.localScale = originalScale;
                enlarge = true;
            }
            yield return new WaitForSeconds(0.3f);
        }
        sp.transform.localScale = originalScale;
    }

    public void ShowSet(){
        sp = GameObject.Find(DropDownList.options[DropDownList.value].text);
        if(sp != null){
            List<GameObject> descendents = sp.GetComponent<Parent>().descendents;
            foreach(GameObject descendent in descendents){
                if(descendent.tag != "SphereInModel"){
                    descendent.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
                    descendent.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
                }
            }
        }
    }
}
