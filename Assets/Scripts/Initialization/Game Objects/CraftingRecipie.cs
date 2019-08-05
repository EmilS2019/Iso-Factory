using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipie
{

    public ItemList[] RequiredItems { get; protected set; }
    public ItemList FinishedProduct { get; protected set; }
    public int AmountProduced { get; protected set; }

    private CraftingRecipie(ItemList required, int AmountRequired1, ItemList finished)
    {
        RequiredItems = new ItemList[1];
        RequiredItems[0] = required;
        FinishedProduct = finished;
        AmountProduced = 1;
    }

    public static CraftingRecipie ironIngot = new CraftingRecipie(ItemList.IronOre, 1, ItemList.IronIngot);
    public static CraftingRecipie ironGear = new CraftingRecipie(ItemList.IronOre, 1, ItemList.IronIngot);

}

[Serializable]
public struct ItemAmount
{
    public ItemList Item;
    public int Amount;
}

[CreateAssetMenu]
public class CraftingRecipiee : ScriptableObject
{
    public List<ItemAmount> Materials;
    public List<ItemAmount> Results;


}

