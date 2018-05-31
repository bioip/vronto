using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VRTK;

public class PointerMovement : MonoBehaviour {

	private IEnumerator MoveObjectBackwards(GameObject ball, Vector3 dir) {
		for(int i = 0; i < 10; i++) {
			ball.transform.position -= dir;
			yield return null;
		}
	}	

	private IEnumerator MoveObjectForward(GameObject ball, Vector3 dir) {
		for(int i = 0; i < 10; i++) {
			ball.transform.position += dir;
			yield return null;
		}
	}
	
	// Update is called once per frame
	void Update () {
		GameObject grabbedObject = GetComponent<VRTK_InteractGrab>().GetGrabbedObject();
		 if (grabbedObject != null) {
			 var controllerEvents = GetComponent<VRTK_ControllerEvents>();
			 if (controllerEvents.IsButtonPressed(VRTK_ControllerEvents.ButtonAlias.TouchpadPress)) {
				 Vector2 direction = controllerEvents.GetTouchpadAxis();
				 if (direction.x >= -0.2f && direction.x <= 0.2f && direction.y <= -0.8f) {
					 MoveObjectForward(grabbedObject, new Vector3(0, 0, 0.2f));
				}
				 else if (direction.x >= -0.2f && direction.x <= 0.2f && direction.y >= 0.8f) {
					 MoveObjectBackwards(grabbedObject, new Vector3(0, 0, 0.2f));
				}
    		}
		}
	}
}