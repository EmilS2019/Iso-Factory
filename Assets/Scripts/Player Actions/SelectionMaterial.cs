using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMaterial : MonoBehaviour {

    static Material original;

    public static Material Selection(MeshRenderer rend, string materialName)
    {
        original = rend.material;
        rend.material.shader = Shader.Find(materialName);
        rend.material.color -= new Color(0, 0, 0, 0.5f);

        return rend.material;
    }
    //Legacy Shaders/Transparent/Diffuse

    public static void ChangeShader(GameObject gameobject, string materialName)
    {
        gameobject.GetComponent<MeshRenderer>().material = Selection(gameobject.GetComponent<MeshRenderer>(), materialName);

        if (gameobject.transform.childCount > 0)
            for (int i = 0; i < gameobject.transform.childCount; i++)
            {
                if (gameobject.transform.GetChild(i).GetComponent<MeshRenderer>() != null)
                    gameobject.transform.GetChild(i).GetComponent<MeshRenderer>().material = Selection(gameobject.transform.GetChild(i).GetComponent<MeshRenderer>(), materialName);
            }
    }

}
