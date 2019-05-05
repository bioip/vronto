using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class contains the script to utilize the searchbar
/// </summary>
public class SearchBar : MonoBehaviour
{

    public Dropdown DropdownList;   // The UI dropdown list
    private List<Dropdown.OptionData> m_Messages;   // The list of messages in the dropdown list
    private InputField input;   // The UI inputfield
    private List<string> labels = new List<string>();   // The list of all labels
    private List<string> m_dropdownList;    // The list of all items in the dropdown list
    private string outputString;    // The output string of the keyboard
    private bool matched;   // Whether we have a matching item for the search
    public GameObject SphereGenerator;  // The sphere generator gameobject

    // Use this for initialization
    void Start()
    {
        m_Messages = DropdownList.options;
        input = gameObject.GetComponent(typeof(InputField)) as InputField;
        foreach (Dropdown.OptionData message in m_Messages)
        {
            labels.Add(message.text.ToUpper().Trim());
        }
        matched = false;

        m_dropdownList = SphereGenerator.GetComponent<SpheresGenerator>().m_dropdownList;

        Debug.Log(m_dropdownList.IndexOf("head"));
    }

    /// <summary>
    /// When the user click the key on the keyboard, find the matching item in the list
    /// </summary>
    public void ClickKey()
    {
        if (input.text != "")
        {
            matched = false;
            foreach (string label in m_dropdownList)
            {
                if (label.ToUpper().Trim().StartsWith(input.text.Trim()))
                {
                    if (!matched)
                    {
                        DropdownList.value = labels.IndexOf(label);
                        int startIndex = m_dropdownList.IndexOf(label);
                        int range = Mathf.Min(20, m_dropdownList.Count - startIndex);
                        DropdownList.ClearOptions();
                        DropdownList.AddOptions(m_dropdownList.GetRange(startIndex, range));
                        DropdownList.value = 0;
                        matched = true;
                    }
                }
            }
        }
    }

    public void Enter()
    {


    }
}
