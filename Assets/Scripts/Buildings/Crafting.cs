using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Crafting : MonoBehaviour {

    public Building building;
    public CraftingRecipie currentRecipie;

    public void Craft()
    {
        currentRecipie = CraftingRecipie.ironIngot;

        ItemContainer[] a = new ItemContainer[currentRecipie.requiredItems.Length];

        int i = 0;
        foreach (ItemList item in currentRecipie.requiredItems)
        {
            a[i] = ItemContainer.FindItemContainer(building.InputContainers, item);
            //ItemContainer.UpdateValue()

            i++;
        }

        


    }

}
