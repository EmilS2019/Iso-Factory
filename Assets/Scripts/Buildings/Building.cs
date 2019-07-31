using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    Mine mine;
    Splitter splitter;
    public Vector3 direction;
    public BuildingList building;
    public MouseManager Mousemanager { get; protected set; }

    public ItemContainer[] itemContainerArrayOutput;
    public ItemContainer[] itemContainerArrayInput;

    public void Start()
    { 
        Mousemanager = FindObjectOfType<MouseManager>();

        InstantiateItemContainers();

        //Gives certain instructions based on building type.
        switch (building.CurrentBuildingType)
        {
            case (BuildingList.BuildingTypes.Mine):
                mine = GetComponent<Mine>();
                break;

            case (BuildingList.BuildingTypes.Conveyor):
                break;

            case (BuildingList.BuildingTypes.Splitter):
                splitter = gameObject.AddComponent<Splitter>();
                splitter.building = this;
                break;

            default:
                break;
        }

        if (building.Outputs > 0)
            StartCoroutine(CheckOuput());

        if (building.Inputs > 0)
            StartCoroutine(CheckInputs());
    }

    //This script creates the containers for items, and creates as many as specified in the BuildingList script.
    void InstantiateItemContainers()
    {

        itemContainerArrayOutput = new ItemContainer[building.Outputs];
        itemContainerArrayInput = new ItemContainer[building.Inputs];

        //The two for loops fill out the Container array and seperates between outputs and inputs.

        for (int i = 0; i < itemContainerArrayOutput.Length; i++)
        {
            itemContainerArrayOutput[i] = new ItemContainer(ItemContainer.ContainerType.Output, ItemList.Nothing, i * 110);
        }
        for (int i = 0; i < itemContainerArrayInput.Length; i++)
        {
            itemContainerArrayInput[i] = new ItemContainer(ItemContainer.ContainerType.Input, ItemList.Nothing, i * 110);
        }
    }

    //This script controlls the output of the Building. It first scans to see if an item already exists, and if not then it creates
    //The output gameobject to be sent on conveyor belts to another building.
    Collider[] hit;
    public LayerMask onlyItems;
    IEnumerator CheckOuput()
    {

        hit = Physics.OverlapSphere(transform.position, 0.2f, onlyItems);

        if (hit.Length == 0)
        {
            for (int i = itemContainerArrayOutput.Length - 1; i >= 0; i--)
            {
                if (itemContainerArrayOutput[i].item.CurrentResourceType != ItemList.ResourceType.Nothing && itemContainerArrayOutput[i].amount > 0)
                {
                    GameObject item = Instantiate(itemContainerArrayOutput[i].item.ItemObject, transform.position - new Vector3(0, 0.25f), Quaternion.identity, HierarchyManager.IronOre);
                    item.GetComponent<ResourceScript>().Item = itemContainerArrayOutput[i].item;
                    ItemContainer.UpdateValue(--itemContainerArrayOutput[i].amount, itemContainerArrayOutput[i]);
                }
            }
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckOuput());
    }

    //TO-DO: In the prefab, add all inputs and put it under one empty object as their parent
    public GameObject[] inupts;

    IEnumerator CheckInputs()
    {
        foreach (GameObject input in inupts)
        {

            hit = Physics.OverlapSphere(input.transform.position, 0.2f, onlyItems);

            //if it spots an object near it's input (The red thing on the back of buildings)
            if (hit.Length > 0)
            {
                ResourceScript rs = hit[0].GetComponent<ResourceScript>();

                //Foreach 
                foreach (ItemContainer con in itemContainerArrayInput)
                {
                    //If the item containers resource matches the one that's being fed in, increase the amount by one and destroy the game object.
                    if (con.item.CurrentResourceType == rs.Item.CurrentResourceType && con.amount < con.item.MaxStack)
                    {
                        ItemContainer.UpdateValue(++con.amount, con);
                        break;
                    }
                    //Otherwise use an empty one
                    else if (con.item.CurrentResourceType == ItemList.ResourceType.Nothing)
                    {
                        con.item = rs.Item;
                        ItemContainer.UpdateValue(++con.amount, con);
                        break;
                    }
                }

                if (building.CurrentBuildingType == BuildingList.BuildingTypes.Splitter)
                    splitter.Split();

                Destroy(hit[0].gameObject);
            }
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckInputs());
    }

}