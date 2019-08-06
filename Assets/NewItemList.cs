using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewItemList : MonoBehaviour {

    public static ItemList Nothing;
    public static ItemList IronOre;
    public static ItemList IronIngot;

    [SerializeField]
    public ItemList _IronIngot;
    [SerializeField]
    public ItemList _IronOre;
    [SerializeField]
    public ItemList _Nothing;

    void Start()
    {
        IronIngot = _IronIngot;
        IronOre = _IronOre;
        Nothing = _Nothing;
    }
}
