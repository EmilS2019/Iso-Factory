using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Mine : MonoBehaviour {


    public Building building;
    public ItemList resourceToMine;

    public LayerMask layermask;
    Ray ray;
    MouseManager mousemanager;

    void Start()
    {
        mousemanager = GetComponent<Building>().Mousemanager;
        ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, layermask))
        {
            if (hit.transform.gameObject.GetComponent<Tile>().currentTileType == Tile.TileTypes.Iron)
            {
                resourceToMine = ItemList.IronOre;
            }
            else
            {

                Destroy(gameObject);
            }
        }
        StartCoroutine(Mining(resourceToMine));
    }

    public float MiningTime;

    //Mines the resource that has been passed in.
    public IEnumerator Mining(ItemList resource)
    {
        yield return new WaitForSeconds(MiningTime);

        foreach (ItemContainer output in building.OutputContainers)
        {
            if (output.amount < resource.MaxStack)
            {
                if (output.item.CurrentResourceType == resource.CurrentResourceType && output.amount < resource.MaxStack)
                {
                    ItemContainer.UpdateValue(1, output);
                    break;
                }
                else if (output.item == ItemList.Nothing)
                {
                    output.item = resource;
                    ItemContainer.UpdateValue(1, output);
                    break;
                }
            }
        }

        StartCoroutine(Mining(resource));
    }
}