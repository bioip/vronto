using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// This class contains the script to draw out the grid
/// </summary>
public class AxisXYZ : MonoBehaviour
{
    static Material lineMaterial;
    public float AxisLength;

    protected float offset;

    /// <summary>
    /// Create the line material for rendering the grid
    /// </summary>
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

    
    /// <summary>
    /// Render the grid. Will be called after all regular rendering is done
    /// </summary>
    public void OnRenderObject()
    {

        if (offset <= 0)
        {
            return;
        }

        CreateLineMaterial();
        // Apply the line material
        lineMaterial.SetPass(0);

        GL.PushMatrix();
        // Set transformation matrix for drawing to
        // match our transform
        GL.MultMatrix(transform.localToWorldMatrix);

        // Draw lines
        GL.Begin(GL.LINES);
        //Draw X axes
        GL.Color(Color.grey);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(AxisLength, 0.0f, 0.0f);
        for (float i = 0.0f; i <= AxisLength; i += offset)
        {
            for (float j = 0.0f; j <= AxisLength; j += offset)
            {
                GL.Vertex3(0, j, i);
                GL.Vertex3(AxisLength, j, i);
            }
        }


        //Draw Y axes
        GL.Color(Color.grey);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0.0f, AxisLength, 0.0f);
        for (float i = 0.0f; i <= AxisLength; i += offset)
        {
            for (float j = 0.0f; j <= AxisLength; j += offset)
            {
                GL.Vertex3(j, 0, i);
                GL.Vertex3(j, AxisLength, i);
            }
        }


        //Draw Z axes
        GL.Color(Color.grey);
        GL.Vertex3(0, 0, 0);
        GL.Vertex3(0.0f, 0.0f, AxisLength);
        for (float i = 0.0f; i <= AxisLength; i += offset)
        {
            for (float j = 0.0f; j <= AxisLength; j += offset)
            {
                GL.Vertex3(j, i, 0);
                GL.Vertex3(j, i, AxisLength);
            }
        }

        GL.End();
        GL.PopMatrix();
    }

    /// <summary>
    /// Adjust the size offset
    /// </summary>
    /// <param name="newOffset">The new size offset</param>
    public void AdjustOffset(float newOffset)
    {
        offset = newOffset;
        Debug.Log("offset = " + offset);
    }
}