using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	public void OnShelfPage(){
		shelfPage.SetActive(true);
		spherePage.SetActive(false);
		modelPage.SetActive(false);
		homePage.SetActive(false);
	}

	public void OnSpherePage(){
		shelfPage.SetActive(false);
		spherePage.SetActive(true);
		modelPage.SetActive(false);
		homePage.SetActive(false);
	}

	public void OnModelPage(){
		shelfPage.SetActive(false);
		spherePage.SetActive(false);
		modelPage.SetActive(true);
		homePage.SetActive(false);
	}

	public void OnHomePage(){
		shelfPage.SetActive(false);
		spherePage.SetActive(false);
		modelPage.SetActive(false);
		homePage.SetActive(true);
	}
}
