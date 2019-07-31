using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemReferenceList : MonoBehaviour{

    
    public GameObject _ironOreGO;
    public static GameObject ironOreGO;

    
    public void Start()
    {
        ironOreGO = _ironOreGO;
    }

}
