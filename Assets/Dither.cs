using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Dither : MonoBehaviour
{
	public Material mat;
	void Awake()
	{
          //if (!mat)
          //{
              //		mat = new Material(Shader.Find("Hidden/DitherShader"));
          //}
	}
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
      Graphics.Blit(source, (RenderTexture)null, mat);
    }
}
