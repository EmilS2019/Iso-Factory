using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Crafting : MonoBehaviour {

    public Building building;
    public CraftingRecipie currentRecipie= CraftingRecipie.ironIngot;

    public void Craft()
    {

        ItemContainer[] a = new ItemContainer[currentRecipie.RequiredItems.Length];
        ItemContainer b = null;

        int i = 0;
        foreach (ItemList item in currentRecipie.RequiredItems)
        {
            a[i] = ItemContainer.FindItemContainer(building.InputContainers, item);
            ItemContainer.UpdateValue(-1, a[i]);

            b = ItemContainer.FindItemContainer(building.OutputContainers, ItemList.IronIngot);
        }
        b.item = currentRecipie.FinishedProduct;
        ItemContainer.UpdateValue(currentRecipie.AmountProduced, b);
        i++;
    }
}
