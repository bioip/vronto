using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectSpheres : MonoBehaviour {
	public List<GameObject> spheres;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider other){
		if(other.tag == "Model"){
			
			foreach (GameObject sphere in spheres) {
				
				if(sphere.tag == "SphereInModel" && sphere.name != this.name){
					
					//same xz plane
					if(sphere.transform.position.y == transform.position.y) {

						DrawLine(transform.position, sphere.transform.position, Color.yellow, 2.0f);

					}

					//same yz plane
					if(sphere.transform.position.x == transform.position.x) {

						DrawLine(transform.position, sphere.transform.position, Color.yellow, 2.0f);
						
					}

					//same xy plane
					if(sphere.transform.position.z == transform.position.z) {

						DrawLine(transform.position, sphere.transform.position, Color.yellow, 2.0f);
						
					}

				}
			}
		}
	}

	void DrawLine(Vector3 start, Vector3 end, Color color, float duration = 0.2f)
	{
		GameObject myLine = new GameObject();
		myLine.transform.position = start;
		myLine.AddComponent<LineRenderer>();
		LineRenderer lr = myLine.GetComponent<LineRenderer>();
		lr.material = new Material(Shader.Find("Particles/Alpha Blended Premultiply"));
		lr.SetColors(color, color);
		lr.SetWidth(0.1f, 0.1f);
		lr.SetPosition(0, start);
		lr.SetPosition(1, end);
		GameObject.Destroy(myLine, duration);
	}

}
