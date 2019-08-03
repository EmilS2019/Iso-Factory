using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    Mine mine;
    Splitter splitter;
    Crafting crafting;
    public Vector3 direction;
    public BuildingList building;
    public MouseManager Mousemanager { get; protected set; }

    public ItemContainer[] OutputContainers;
    public ItemContainer[] InputContainers;

    public float MiningTime;
    public float height;

    public void Start()
    {
        SelectionMaterial.ChangeTransparency(gameObject, "Standard");

        Mousemanager = FindObjectOfType<MouseManager>();

        InstantiateItemContainers();

        //Gives certain instructions based on building type.
        switch (building.CurrentBuildingType)
        {
            case (BuildingList.Types.Mine):
                mine = gameObject.AddComponent<Mine>();
                mine.building = this;
                mine.MiningTime = MiningTime;
                break;

            case (BuildingList.Types.Conveyor):
                break;

            case (BuildingList.Types.Splitter):
                splitter = gameObject.AddComponent<Splitter>();
                splitter.building = this;
                break;

            case (BuildingList.Types.Smelter):
                crafting = gameObject.AddComponent<Crafting>();
                crafting.building = this;
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

        OutputContainers = new ItemContainer[building.Outputs];
        InputContainers = new ItemContainer[building.Inputs];
        //CraftingContainers = new ItemContainer[1];

        //The two for loops fill out the Container array and seperates between outputs and inputs.

        for (int i = 0; i < OutputContainers.Length; i++)
        {
            OutputContainers[i] = new ItemContainer(ItemContainer.ContainerType.Output, ItemList.Nothing, i * 110);
        }
        for (int i = 0; i < InputContainers.Length; i++)
        {
            InputContainers[i] = new ItemContainer(ItemContainer.ContainerType.Input, ItemList.Nothing, i * 110);
        }
    }

    //This script controlls the output of the Building. It first scans to see if an item already exists, and if not then it creates
    //The output gameobject to be sent on conveyor belts to another building.
    Collider[] hit;
    public LayerMask onlyItems;
    public float inOverlapSphereRadius;
    public float outOverlapSphereRadius;
    IEnumerator CheckOuput()
    {
        //The position of the output (which is rounded to be the center of the block the output is placed on, aka in the building).
        int x = 0;

        if (outputs.Length > 1)
            x = Random.Range(0, OutputContainers.Length);
      
        Vector3 pos = outputs[x].transform.position;
        pos = new Vector3(Mathf.RoundToInt(pos.x), 0.666f , (Mathf.RoundToInt(pos.z)));

        //Scans for objects in the nearby, and if no one is nearby then create a new item and assign it some values. 
        hit = Physics.OverlapSphere(pos, outOverlapSphereRadius, onlyItems);
        if (hit.Length == 0)
        {
            for (int i = OutputContainers.Length - 1; i >= 0; i--)
            {
                if (OutputContainers[i].item.CurrentResourceType != ItemList.ResourceType.Nothing && OutputContainers[i].amount > 0)
                {
                    GameObject item = Instantiate(OutputContainers[i].item.ItemObject, pos, Quaternion.identity, HierarchyManager.IronOre);
                    item.GetComponent<ResourceScript>().Item = OutputContainers[i].item;
                    ItemContainer.UpdateValue(-1, OutputContainers[i]);
                }
            }
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckOuput());

        
    }

    //TO-DO: In the prefab, add all inputs and put it under one empty object as their parent
    public GameObject[] inupts;
    public GameObject[] outputs;

    IEnumerator CheckInputs()
    {
        LinkedList<int> test = new LinkedList<int>();
        

        foreach (GameObject input in inupts)
        {

            hit = Physics.OverlapSphere(input.transform.position, inOverlapSphereRadius, onlyItems);

            //if it spots an object near it's input (The red thing on the back of buildings)
            if (hit.Length > 0)
            {
                ResourceScript rs = hit[0].GetComponent<ResourceScript>();

                //Foreach 
                foreach (ItemContainer con in InputContainers)
                {

                    if (building.CurrentBuildingType == BuildingList.Types.Splitter && con.amount < con.item.MaxStack)
                    {
                        splitter.Split(rs, con);
                        break;
                    }
                    else if (gameObject.GetComponent<Crafting>() && con.amount < con.item.MaxStack)
                    {
                        crafting.Craft();
                    }

                    //If the item containers resource matches the one that's being fed in, increase the amount by one and destroy the game object.
                    if (con.item.CurrentResourceType == rs.Item.CurrentResourceType && con.amount < con.item.MaxStack)
                    {
                        ItemContainer.UpdateValue(1, con);
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
                Destroy(hit[0].gameObject);
            }
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckInputs());
    }
}