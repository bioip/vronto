using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class contains the script to manage the different UI pages
/// </summary>
public class MenuManager : MonoBehaviour {

	public GameObject shelfPage;
	public GameObject spherePage;
	public GameObject modelPage;
	public GameObject homePage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	/// <summary>
	/// Navigate to the Shelf page
	/// </summary>
	public void OnShelfPage(){
		shelfPage.SetActive(true);
		spherePage.SetActive(false);
		modelPage.SetActive(false);
		homePage.SetActive(false);
	}

	/// <summary>
	/// Navigate to the Sphere page
	/// </summary>
	public void OnSpherePage(){
		shelfPage.SetActive(false);
		spherePage.SetActive(true);
		modelPage.SetActive(false);
		homePage.SetActive(false);
	}

	/// <summary>
	/// Navigate to the Model/Grid page
	/// </summary>
	public void OnModelPage(){
		shelfPage.SetActive(false);
		spherePage.SetActive(false);
		modelPage.SetActive(true);
		homePage.SetActive(false);
	}

	/// <summary>
	/// Navigate to the HomeSpot page
	/// </summary>
	public void OnHomePage(){
		shelfPage.SetActive(false);
		spherePage.SetActive(false);
		modelPage.SetActive(false);
		homePage.SetActive(true);
	}
}
