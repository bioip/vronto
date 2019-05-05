using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VRTK;

/// <summary>
/// This class contains the script to interact with spheres placed inside the model
/// </summary>
public class DetectCollisi : MonoBehaviour
{

    private float x0 = 36.45f;  // The starting x coordinate
    private float y0 = -6.85f;  // The starting y coordinate
    private float z0 = 9.35f;   // The starting z coordinate
    public float offset;    // The size offset

    public bool snappingOnOff;  // Whether the snapping is on/off

    public GameObject rightController;  // The right hand controller

    public Toggle snappingToggle;   // The UI toggle that shows if the snapping is on/off

    // Use this for initialization
    void Start()
    {
        Debug.Log("detection started");
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("update works");
        snappingToggle.isOn = snappingOnOff;
    }

    /// <summary>
    /// Turn snapping on/off
    /// </summary>
    public void ToggleSnappingOnOff()
    {
        snappingOnOff = !snappingOnOff;
    }

    /// <summary>
    /// Turn snapping off
    /// </summary>
    public void SnappingOff()
    {
        snappingOnOff = false;

    }

    /// <summary>
    /// Interact with spheres placed inside the model
    /// </summary>
    /// <param name="other">The collider that triggers</param>
    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Sphere" || other.tag == "SphereInModel")
        {
            GameObject sphere = other.gameObject;

            sphere.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.green;
            sphere.GetComponent<VRTK_InteractableObject>().ToggleHighlight(true);
            other.tag = "SphereInModel";

            GameObject grabbedObject = rightController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject();


            if (grabbedObject == null || rightController.GetComponent<VRTK_InteractGrab>().GetGrabbedObject().name != sphere.name)
            {
                //snapping to the grid

                float x = sphere.transform.position.x;
                float y = sphere.transform.position.y;
                float z = sphere.transform.position.z;

                //get the index of the target cell
                float i = Mathf.Floor(Mathf.Abs((x - x0) / offset));
                float j = Mathf.Floor(Mathf.Abs((y - y0) / offset));
                float k = Mathf.Floor(Mathf.Abs((z - z0) / offset));

                if (snappingOnOff)
                {
                    //get the coordinates of the 8 corners of the target cell

                    List<Vector3> corners = new List<Vector3>();

                    corners.Add(new Vector3(x0 + offset * i, y0 + offset * j, z0 - offset * k));
                    corners.Add(new Vector3(x0 + offset * (i + 1), y0 + offset * j, z0 - offset * k));
                    corners.Add(new Vector3(x0 + offset * (i + 1), y0 + offset * (j + 1), z0 - offset * k));
                    corners.Add(new Vector3(x0 + offset * i, y0 + offset * (j + 1), z0 - offset * k));
                    corners.Add(new Vector3(x0 + offset * i, y0 + offset * j, z0 - offset * (k + 1)));
                    corners.Add(new Vector3(x0 + offset * (i + 1), y0 + offset * j, z0 - offset * (k + 1)));
                    corners.Add(new Vector3(x0 + offset * (i + 1), y0 + offset * (j + 1), z0 - offset * (k + 1)));
                    corners.Add(new Vector3(x0 + offset * i, y0 + offset * (j + 1), z0 - offset * (k + 1)));

                    float minDistance = float.MaxValue;
                    Vector3 target = new Vector3(0f, 0f, 0f);

                    foreach (Vector3 v in corners)
                    {
                        if (Vector3.Distance(sphere.transform.position, v) < minDistance)
                        {
                            target = v;
                            minDistance = Vector3.Distance(sphere.transform.position, v);
                        }
                    }

                    sphere.transform.position = target;
                }



            }

        }
    }

    /// <summary>
    /// Interact with spheres that exit the model
    /// </summary>
    /// <param name="other"></param>
    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Sphere" || other.tag == "SphereInModel")
        {
            GameObject sphere = other.gameObject;

            sphere.GetComponent<VRTK_InteractableObject>().ToggleHighlight(false);
            sphere.GetComponent<VRTK_InteractableObject>().touchHighlightColor = Color.yellow;
            other.tag = "Sphere";
        }
    }

    /// <summary>
    /// Update grid size offset
    /// </summary>
    /// <param name="newOffset">The new size offset</param>
    public void UpdateOffset(float newOffset)
    {
        offset = newOffset;
    }



}
