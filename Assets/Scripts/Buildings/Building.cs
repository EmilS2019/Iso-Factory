using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    //TO-DO: SLOWLY phase these out
    public List<ItemList> outputResource = new List<ItemList>();
    public List<ItemList> inputResource1 = new List<ItemList>();
    public List<ItemList> inputResource2 = new List<ItemList>();
    public List<List<ItemList>> inputResources;


    Mine mine;
    public Vector3 direction;
    public BuildingList building;
    public MouseManager Mousemanager { get; protected set; }

    public ItemContainer[] itemContainerArrayOutput;
    public ItemContainer[] itemContainerArrayInput;

    public void Start()
    { 
        inputResources = new List<List<ItemList>>() { inputResource1, inputResource2 };
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
                break;

            default:
                break;
        }

        if (building.Outputs > 0)
            StartCoroutine(CheckOuput());

        if (building.Inputs > 0)
            StartCoroutine(CheckInputs());
    }

    //This script controlls the output of the Building. It first scans to see if an item already exists, and if not then it creates
    //The output gameobject to be sent on conveyor belts to another building.
    Collider[] hit;
    public LayerMask onlyItems;
    IEnumerator CheckOuput()
    {

        hit = Physics.OverlapSphere(transform.position, 0.20f, onlyItems);
        if (hit.Length == 0 && outputResource.Count > 0)
        {
            GameObject item = Instantiate(mine.resourceToMine.ItemObject, transform.position - new Vector3(0,0.25f), 
                                Quaternion.identity, HierarchyManager.IronOre);
            ResourceScript rScript = item.GetComponent<ResourceScript>();
            rScript.resourceType = outputResource[0];
            outputResource.Remove(outputResource[0]);
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
            hit = Physics.OverlapSphere(input.transform.position, 0.20f, onlyItems);

            if (hit.Length > 0)
            {
                ResourceScript rs = hit[0].GetComponent<ResourceScript>();

                for (int i = 0; i < inputResources.Count; i++)
                {
                    if (inputResources[i].Contains(rs.resourceType) || inputResources[i].Count == 0)
                    {
                        inputResources[i].Add(rs.resourceType);

                        print(inputResources[i].Count);
                        Destroy(hit[0].gameObject);

                        break;
                    }

                }                
            }
        }
        yield return new WaitForFixedUpdate();
        StartCoroutine(CheckInputs());
    }

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
                itemContainerArrayOutput[i] = new ItemContainer(ItemContainer.ContainerType.Input, ItemList.Nothing, i * 110);
        }


        int j = 0;
        for (int i = 0; i < itemContainerArrayOutput.Length;)
        {

            if (i < building.Outputs)
            {
                i++;
            }
            else
            {
                itemContainerArrayOutput[i] = new ItemContainer(ItemContainer.ContainerType.Input, ItemList.IronOre, j * 110);
                i++;
                j++;
            }

        }
    }



}