using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable][CreateAssetMenu]
public class ItemList : ScriptableObject
{
    /*
     ----------------------------------------------------------------------------------------
     |This file is for storing the different kinds of Ores and Items that exist in this game|
     ----------------------------------------------------------------------------------------
    */

    public enum ResourceType
    {
        Nothing,
        Iron_Ore,
        Iron_Ingot,
        Iron_Gear
    }
    public ResourceType CurrentResourceType;
    public GameObject ItemObject;
    public bool Stackable;
    public int MaxStack;
    public GameObject Hierarchy;


    public ItemList(ResourceType _resourceType, GameObject _itemObject, int _MaxStack, Transform _Hierarchy)
    {
        CurrentResourceType = _resourceType;
        ItemObject = _itemObject;
        MaxStack = _MaxStack;

    }

    public ItemList(ResourceType _resourceType)
    {
        CurrentResourceType = _resourceType;
    }

    //I use a seperate folder because it won't allow me to do it here unless it's static for some reason.  

    public static ItemList IronOre = new ItemList(ResourceType.Iron_Ore, ItemReferenceList.ironOreGO, 100, HierarchyManager.IronOre);
    public static ItemList Nothing = new ItemList(ResourceType.Nothing);
    public static ItemList IronIngot = new ItemList(ResourceType.Iron_Ingot, ItemReferenceList.ironIngotGO, 100, HierarchyManager.IronIngot);
    public static ItemList IronGear = new ItemList(ResourceType.Iron_Gear, ItemReferenceList.ironOreGO, 100, HierarchyManager.Items);
    
}
