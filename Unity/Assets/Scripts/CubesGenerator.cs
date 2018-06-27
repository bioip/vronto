using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubesGenerator : MonoBehaviour {

	private List<GameObject> cubes = new List<GameObject>();
	private Mesh m_mesh;
	public GameObject refCube;
	public GameObject visual;
	public Vector3 originPos;

	// Use this for initialization
	void Start () {
		MeshFilter filter = visual.GetComponent(typeof(MeshFilter)) as MeshFilter;
		m_mesh = filter.mesh;



		for(int i = 0; i < 5; i++){
			for(int j = 0; j < 15; j++){
				for(int k = 0; k < 15; k++){
					GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
					cube.transform.parent = transform;
					//cube.transform.position = new Vector3(37.39f + i * 1.65f, -5.91f + j * 1.65f, 8.34f - k * 1.65f);
					cube.transform.position = originPos + new Vector3(i * 1.65f, j * 1.65f, -k * 1.65f);
					cube.transform.localScale = new Vector3(1.65f, 1.65f, 1.65f);
					cubes.Add(cube);
				}
			}
		}


		Mesh canonical = refCube.GetComponent<MeshFilter>().mesh;
		Vector3[] vertices = new Vector3[cubes.Count * canonical.vertexCount];
		int [] triangles = new int[cubes.Count * canonical.triangles.Length];


		int vcount = 0, tcount = 0, voffset = 0;
		for(int i = 0; i < cubes.Count; i++){
			Vector3 pos = cubes[i].transform.position;

			for(int j = 0; j < canonical.vertexCount; j++, vcount++){
				//set scale of cubes
				vertices[vcount] = visual.transform.InverseTransformPoint(1.65f*canonical.vertices[j] + pos);
			}

			for(int j = 0; j < canonical.triangles.Length; j++, tcount++){
				triangles[tcount] = canonical.triangles[j] + voffset;
			}

			voffset = vcount;
		}

		m_mesh.vertices = vertices;
		m_mesh.triangles = triangles;

		
		foreach(GameObject cube in cubes){
			Destroy(cube);
		}
		cubes.Clear();
		

		refCube.SetActive(false);

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
