using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the script to turn the model on/off
/// </summary>
public class ToggleModelOnOff : MonoBehaviour
{

    private GameObject model;

    // Use this for initialization
    void Start()
    {
        model = this.gameObject.transform.GetChild(0).gameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Turn the model on/off
    /// </summary>
    public void ModelOnOff()
    {
        model.SetActive(!model.activeSelf);
    }
}
