using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

public class TouchedInput : MonoBehaviour
{

    public InputField input;
    public Dropdown input_dropdown;
    public string label;
    public GameObject rightController;
    public GameObject sp;
    public List<string> labelList;
    public List<string> m_dropdownList;

    // Use this for initialization
    void Start()
    {
        rightController = GameObject.FindGameObjectWithTag("RightController");
    }

    // Update is called once per frame
    void Update()
    {
        VRTK_InteractableObject io = GetComponent(typeof(VRTK_InteractableObject)) as VRTK_InteractableObject;
        if (io.IsTouched())
        {

            //output sphere label to HUD inputfield when not showing set right now
            if(!input_dropdown.GetComponent<InteractWithLabeledSphere>().showingSet){
                input_dropdown.ClearOptions();
                input.text = label;
                input_dropdown.value = m_dropdownList.IndexOf(label);
                int value = m_dropdownList.IndexOf(label);
                int startIndex = Mathf.Max(0, value - 10);
                int range = Mathf.Min(20, m_dropdownList.Count - startIndex);
                input_dropdown.AddOptions(m_dropdownList.GetRange(value, range));
            }
            
        }

        if (io.IsGrabbed())
        {

            input.text = label;
            if (!labelList.Contains(label))
            {
                labelList.Add(label);
            }
            else
            {
            }

        }
        else
        {
            
        }
    }

}
