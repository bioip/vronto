using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

/// <summary>
/// This class contains the script to interact with the spheres pointed by the pointer
/// </summary>
public class TouchedInput : MonoBehaviour
{

    public InputField input;    // The inputfield for the currently pointed sphere's label
    public Dropdown input_dropdown; // The UI dropdown list
    public string label;    // The sphere's label
    public GameObject rightController;  // The right hand controller
    public GameObject sp;   // The sphere
    public List<string> labelList;  // The list of all labels
    public List<string> m_dropdownList; // The list of all labels in the dropdown list
    public Text currSelected;   // The UI text for currently grabbed/selected sphere label

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
            if (!input_dropdown.GetComponent<InteractWithLabeledSphere>().showingSet)
            {
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
            //output the currently grabbed/selected sphere
            currSelected.text = label;
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
