using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Dither : MonoBehaviour
{
	public Material mat;
	public Color[] colors ;
	void Awake()
	{
         mat.SetColorArray("_DitherColors", colors);
	}
    void OnRenderImage(RenderTexture source, RenderTexture dest)
    {
      Graphics.Blit(source, (RenderTexture)null, mat);
    }
}
