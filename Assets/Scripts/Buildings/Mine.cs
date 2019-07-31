using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Building))]
public class Mine : MonoBehaviour {


    Building building;
    public ItemList resourceToMine;

    public LayerMask layermask;
    Ray ray;
    MouseManager mousemanager;

    void Start()
    {
        mousemanager = GetComponent<Building>().Mousemanager;
        building = GetComponent<Building>();
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

    public IEnumerator Mining(ItemList resource)
    {
        yield return new WaitForSeconds(MiningTime);

        if (building.outputResource.Count < resource.MaxStack)
        {
            foreach (ItemContainer output in building.itemContainerArrayOutput)
            {
                //Debug.Log(output.item.CurrentResourceType + " has to be equal to either " + output.item.CurrentResourceType +
                    //" or nothing. "+ output.amount + " has to be less than "+ resource.MaxStack);
                

                if (output.item.CurrentResourceType == resource.CurrentResourceType && output.amount < resource.MaxStack)
                {
                    output.amount++;
                    ItemContainer.UpdateValue(output.amount, output);
                    break;
                }
                else if(output.item == ItemList.Nothing)
                {
                    output.item = resource;
                    ItemContainer.UpdateValue(1, output);
                    break;
                }
            }

            //Makes sure that the UI updates when the amount of materials update. 
            //TO-DO: Move this to Building script.
            if (mousemanager.SelectedObject == gameObject)
                mousemanager.ShowSelectedObjectUI(gameObject);

        }

        StartCoroutine(Mining(resource));
    }

}
