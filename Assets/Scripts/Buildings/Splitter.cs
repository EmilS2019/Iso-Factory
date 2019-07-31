using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Splitter : MonoBehaviour {

    public Building building;

    void Start()
    {   
        building = GetComponent<Building>();

    }

    public void Split()
    {
        if (building.itemContainerArrayInput != null)
        {
            foreach (ItemContainer f in building.itemContainerArrayInput)
            {
                building.itemContainerArrayOutput[Random.Range(0, building.itemContainerArrayOutput.Length)] = f;
                f.amount--;
                if (f.amount <= 0)
                {
                    f.item = ItemList.Nothing;
                    f.amount = 0;
                }
            }
        }
    }
}
