using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemContainer 
{
    
    public Sprite sprite;
    public ItemList item;
    public int amount;
    public Text text;
    public GameObject textGO;
    public enum ContainerType { Input, Output, Storage }
    public ContainerType CurrentContainerType { get; protected set; }


    public ItemContainer(ContainerType _currentContainerType, ItemList _itemList, float YOffset)
    {
        //Sets up basic values 
        CurrentContainerType = _currentContainerType;
        amount = 0;
        item = _itemList;

        //Important that these come in the right order!
        //Finds and creates a box that will contain the number and the sprite of the object
        textGO = GameObject.Find("Item Container");
        textGO = GameObject.Instantiate(textGO, HierarchyManager.ItemCanvas);

        //Shifts position
        Vector3 pos = new Vector3(0,YOffset);
        switch (CurrentContainerType)
        {
            case ContainerType.Input:
                pos += new Vector3(-750, 70);
                break;
            case ContainerType.Output:
                pos += new Vector3(-550, 70);
                break;
        }

        //Sets up the text for the item container
        textGO.GetComponent<RectTransform>().localPosition = pos;
        text = textGO.transform.GetChild(0).GetComponent<Text>();
        text.text = amount.ToString();
    }

    //Call this to update a value.
    public static void UpdateValue(int _amount, ItemContainer[] containers)
    {
        foreach (ItemContainer container in containers)
        {
            container.amount = _amount;
            container.text.text = container.amount.ToString();

        }
    }

    public static void UpdateValue(int _amount, ItemContainer container)
    {
        container.amount = _amount;
        container.text.text = container.amount.ToString();        
    }

    //Toggle visibility of the Containers.
    public static void ToggleVisibility(ItemContainer container, bool on)
    {        
            container.textGO.SetActive(on);
    }

    public static void ToggleVisibility(ItemContainer[] containers, bool on)
    {
        foreach (ItemContainer container in containers)
        {
            container.textGO.SetActive(on);
        }

    }

    public static void ToggleVisibilityAll(bool on)
    {
        for (int i = 1; i < HierarchyManager.ItemCanvas.childCount; i++)
        {
            HierarchyManager.ItemCanvas.GetChild(i).gameObject.SetActive(on);
        }
    }

    public static void Destroy(ItemContainer[] containers)
    {
        foreach (ItemContainer c in containers)
        {
            MonoBehaviour.Destroy(c.textGO.gameObject);
            c.item = ItemList.Nothing;
            c.amount = 0;
        }
    }
    public static void Destroy(ItemContainer c)
    {

        MonoBehaviour.Destroy(c.textGO.gameObject);
        c.item = ItemList.Nothing;
        c.amount = 0;
    }

    /// <summary>
    /// Searches for and returns the first valid spot in the array that was passed in based on what item you put it.
    /// </summary>
    /// <param name="itemcon"></param>
    /// <param name="item"></param>
    /// <returns></returns>
    public static ItemContainer FindItemContainer(ItemContainer[] itemcon, ItemList item)
    {

        foreach (ItemContainer itemc in itemcon)
        {
            if ((item.CurrentResourceType == itemc.item.CurrentResourceType
                            || itemc.item.CurrentResourceType == ItemList.ResourceType.Nothing) && itemc.amount < item.MaxStack)
                return itemc;
        }
        
        return null;
    }

    public delegate Tresult Func<in T, out Tresult>(T arg) where T : class;
}
