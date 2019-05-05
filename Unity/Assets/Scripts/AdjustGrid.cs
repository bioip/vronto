using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains scripts to adjust the size of the grid
/// </summary>
public class AdjustGrid : MonoBehaviour
{

    private float offset = 0.0f;    // The size offset

    private GameObject grid;    // The grid gameobject

    // Use this for initialization
    void Start()
    {
        this.offset = 2.5f;
        grid = this.gameObject.transform.GetChild(0).gameObject;
        AxisXYZ axes = grid.GetComponent(typeof(AxisXYZ)) as AxisXYZ;
        axes.AdjustOffset(this.offset);
        grid.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Adjust the grid size by changing the offset
    /// </summary>
    /// <param name="newOffset">The new size offset for the grid</param>
    public void AdjustOffset(float newOffset)
    {
        offset = newOffset;
        Debug.Log("adjust grid: offset = " + offset);
        grid.SetActive(false);
        AxisXYZ axes = grid.GetComponent(typeof(AxisXYZ)) as AxisXYZ;
        axes.AdjustOffset(this.offset);
        grid.SetActive(true);

    }
}
