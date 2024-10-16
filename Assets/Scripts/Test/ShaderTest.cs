using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaderTest : MonoBehaviour
{
    public Renderer Rd;
    //public Texture2D Texture2D;
    //public Texture2DArray Texture2DArray;
    //public Texture3D T3D;
    //public Cubemap CubeMap;

    private void Start()
    {
        InvokeRepeating(nameof(SetFloat), 0f, 0.5f);
    }
    private void SetFloat()
    {
        if (Rd.material.GetFloat("_Float_1") == 0f) 
            Rd.material.SetFloat("_Float_1", 1f); 
        else
            Rd.material.SetFloat("_Float_1", 0f);

    }
} // end of class