using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HierarchyManager : MonoBehaviour
{

    public Transform _ItemCanvas;
    public static Transform ItemCanvas;

    public Transform _Items;
    public static Transform Items;
    public Transform _IronOre;
    public static Transform IronOre;
    public Transform _IronIngot;
    public static Transform IronIngot;

    public Transform _Buildings;
    public static Transform Buildings;
    public Transform _Conveyor;
    public static Transform Conveyor;

    void Start()
    {
        ItemCanvas = _ItemCanvas;
        Items = _Items;
        IronOre = _IronOre;
        IronIngot = _IronIngot;
        Buildings = _Buildings;
        Conveyor = _Conveyor;

        
    }
}

public interface ITest
{
    int a { get; set; }
}