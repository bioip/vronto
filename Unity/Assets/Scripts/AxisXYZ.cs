using UnityEngine;
using System.Collections;

public class AxisXYZ : MonoBehaviour
{    
	static Material lineMaterial;
	public float AxisLength;

	static void CreateLineMaterial()
	{
		if (!lineMaterial)
		{
			// Unity has a built-in shader that is useful for drawing
			// simple colored things.
			Shader shader = Shader.Find("Hidden/Internal-Colored");
			lineMaterial = new Material(shader);
			lineMaterial.hideFlags = HideFlags.HideAndDontSave;
			// Turn on alpha blending
			lineMaterial.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
			lineMaterial.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
			// Turn backface culling off
			lineMaterial.SetInt("_Cull", (int)UnityEngine.Rendering.CullMode.Off);
			// Turn off depth writes
			lineMaterial.SetInt("_ZWrite", 0);
		}
	}

	// Will be called after all regular rendering is done
	public void OnRenderObject()
	{
		CreateLineMaterial();
		// Apply the line material
		lineMaterial.SetPass(0);

		GL.PushMatrix();
		// Set transformation matrix for drawing to
		// match our transform
		GL.MultMatrix(transform.localToWorldMatrix);

		// Draw lines
		GL.Begin(GL.LINES);
		//Draw X axis
		GL.Color(Color.red);
		GL.Vertex3(0, 0, 0);
		GL.Vertex3(AxisLength, 0.0f, 0.0f);
		for(float i = 0.0f; i <= AxisLength; i++){
			GL.Vertex3(i, 0, 0);
			GL.Vertex3(i, 0.5f, 0);
		}

		//Draw Y axis
		GL.Color(Color.green);
		GL.Vertex3(0, 0, 0);
		GL.Vertex3(0.0f, AxisLength, 0.0f);
		for(float i = 0.0f; i <= AxisLength; i++){
			GL.Vertex3(0, i, 0);
			GL.Vertex3(0, i, 0.5f);
		}
		for(float i = 0.0f; i <= AxisLength; i++){
			GL.Vertex3(0, i, 0);
			GL.Vertex3(0.5f, i, 0);
		}

		//Draw Z axis
		GL.Color(Color.blue);
		GL.Vertex3(0, 0, 0);
		GL.Vertex3(0.0f, 0.0f, AxisLength);
		for(float i = 0.0f; i <= AxisLength; i++){
			GL.Vertex3(0, 0, i);
			GL.Vertex3(0, 0.5f, i);
		}

		GL.End();
		GL.PopMatrix();
	}
}