using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Splitter : MonoBehaviour {

    public Building building;

    public void Split(ResourceScript rs, ItemContainer con)
    {
        ItemContainer a = ItemContainer.FindItemContainer(building.OutputContainers, rs.Item);

        if (a.item == ItemList.Nothing)
            a.item = rs.Item;

        ItemContainer.UpdateValue(++con.amount, a);
    }
}