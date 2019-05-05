using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the data structure to hold the dependents of the item
/// </summary>
public class Parent : MonoBehaviour
{

    public List<GameObject> descendents = new List<GameObject>();   // The list of descendents of the item

    public string id;   // The HAO id of the item

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
