using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HierarchyManager : MonoBehaviour
{

    public Transform _ItemCanvas;
    public static Transform ItemCanvas;
    public Transform _IronOre;
    public static Transform IronOre;

    void Start()
    {
        ItemCanvas = _ItemCanvas;
        IronOre = _IronOre;
    }
}
