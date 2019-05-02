using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

/// <summary>
/// This class contains the script to teleport to the home spot
/// </summary>
public class HomeSpot : MonoBehaviour {
	private GameObject playArea;

	/// <summary>
	/// Teleport to the home spot
	/// </summary>
	public void MoveToHomeSpot(){
		playArea = GameObject.Find("VRTK/PlayAreaScripts");
		playArea.GetComponent<VRTK_HeightAdjustTeleport>().Teleport(gameObject.transform, gameObject.transform.position, null, false);
	}
}
