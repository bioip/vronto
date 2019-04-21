using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class HomeSpot : MonoBehaviour {
	private GameObject playArea;

	public void MoveToHomeSpot(){
		playArea = GameObject.Find("VRTK/PlayAreaScripts");
		playArea.GetComponent<VRTK_HeightAdjustTeleport>().Teleport(gameObject.transform, gameObject.transform.position, null, false);
	}
}
