using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the script to manage the different UI pages
/// </summary>
public class MenuManager : MonoBehaviour
{

    public GameObject shelfPage;    // The UI page for shelf interactions
    public GameObject spherePage;   // The UI page for sphere interactions
    public GameObject modelPage;    // The UI page for model/grid interactions
    public GameObject homePage;     // The UI page for returning to home spot

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Navigate to the Shelf page
    /// </summary>
    public void OnShelfPage()
    {
        shelfPage.SetActive(true);
        spherePage.SetActive(false);
        modelPage.SetActive(false);
        homePage.SetActive(false);
    }

    /// <summary>
    /// Navigate to the Sphere page
    /// </summary>
    public void OnSpherePage()
    {
        shelfPage.SetActive(false);
        spherePage.SetActive(true);
        modelPage.SetActive(false);
        homePage.SetActive(false);
    }

    /// <summary>
    /// Navigate to the Model/Grid page
    /// </summary>
    public void OnModelPage()
    {
        shelfPage.SetActive(false);
        spherePage.SetActive(false);
        modelPage.SetActive(true);
        homePage.SetActive(false);
    }

    /// <summary>
    /// Navigate to the HomeSpot page
    /// </summary>
    public void OnHomePage()
    {
        shelfPage.SetActive(false);
        spherePage.SetActive(false);
        modelPage.SetActive(false);
        homePage.SetActive(true);
    }
}
