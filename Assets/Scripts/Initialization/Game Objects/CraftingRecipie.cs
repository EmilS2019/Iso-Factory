using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftingRecipie
{

    public ItemList[] requiredItems { get; protected set; }
    public ItemList finishedProduct { get; protected set; }
    public int amountProduced { get; protected set; }

    private CraftingRecipie(ItemList required1, int AmountRequired1, ItemList finished)
    {
        requiredItems = new ItemList[1];
        requiredItems[0] = required1;
        finishedProduct = finished;
        amountProduced = 1;
    }

    public static CraftingRecipie ironIngot = new CraftingRecipie(ItemList.IronOre, 1, ItemList.IronIngot);

}



