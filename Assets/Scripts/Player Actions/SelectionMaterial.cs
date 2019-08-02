using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMaterial : MonoBehaviour {
    

    public static Material Selection(MeshRenderer rend)
    {
        rend.material.shader = Shader.Find("Legacy Shaders/Transparent/Diffuse");
        rend.material.color += new Color(0,0,0,-0.5f);

        return rend.material;
    }
}
