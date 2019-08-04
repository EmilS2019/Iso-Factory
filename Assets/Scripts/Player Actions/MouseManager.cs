using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class MouseManager : MonoBehaviour {

    [SerializeField]
    int rotation = 1;
    public GameObject SpotLight;
    public Text BuildingTypeText;
    Vector3 mousePosition;
    RaycastHit hit;
    BuildingList buildingtype;
    public LayerMask onlyGroundAndBuildings;
    Vector3 buildingPlacement;
  
    void Update()
    {

        Ray rayMousePosition = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(rayMousePosition, out hit, Mathf.Infinity,onlyGroundAndBuildings))
        {
            SpotLight.transform.position = new Vector3(Mathf.RoundToInt(hit.point.x), 2, Mathf.RoundToInt(hit.point.z));
            if(TheBuilding != null)
            {
                Building building = TheBuilding.GetComponent<Building>();
                buildingPlacement = new Vector3(Mathf.RoundToInt(hit.point.x), building.height, Mathf.RoundToInt(hit.point.z));
                switch (building.building.Width)
                {
                    case 2:
                        switch (rotation)
                        {
                            case 1:
                                buildingPlacement.x += 0.5f;
                                break;
                            case 2:
                                buildingPlacement.z += 0.5f;
                                break;
                            case 3:
                                buildingPlacement.x += 0.5f;
                                break;
                            case 4:
                                buildingPlacement.z += 0.5f;
                                break;
                        }
                        break;
                }
                TheBuilding.transform.position = buildingPlacement;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GroundClicked();
                //UI elements  
                ShowSelectedObjectUI(SelectedObject);

            }
        }

        //Cancel if right click.
        if (Input.GetMouseButtonDown(1))
        {
            buildingtype = BuildingList.Nothing;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            rotation++;
            if (rotation == 5)
                rotation = 1;
            TheBuilding.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    void Start()
    {
        SelectedObject = null;
    }

    public GameObject SelectedObject;
    Building buildingScript = null;
    GameObject TheBuilding;

    //This function is called by the building buttons. Changes what building is selected.
    public void ChangeBuilding(int build)
    {
        foreach (BuildingList building in BuildingList.buildinglist)
        {
            if (building.ID == build)
            {
                buildingtype = building;
            }
        }

        if (buildingtype != BuildingList.Nothing && buildingtype != null)
        {
            //Instantiates the building the player wants to construct where mouse was clicked
            switch (buildingtype.CurrentBuildingType)
            {
                //TO-DO: Use the dictionary with IDs instead to prevent too much expansion here and make it easier to add buildings
                case (BuildingList.Types.Mine):
                    InstantiateBuilding(buildingtype, 1f);
                    break;
                case (BuildingList.Types.Conveyor):
                    InstantiateBuilding(buildingtype, 0.666f);
                    break;
                case (BuildingList.Types.Splitter):
                    InstantiateBuilding(buildingtype, 0.8f);
                    break;
                case (BuildingList.Types.Smelter):
                    InstantiateBuilding(buildingtype, 1f);
                    break;
                default:
                    break;
            }

            SelectionMaterial.ChangeTransparency(TheBuilding, "Legacy Shaders/Transparent/Diffuse");
            TheBuilding.GetComponent<Collider>().enabled = enabled;
            TheBuilding.layer = 5;
            TheBuilding.transform.Rotate(new Vector3(0, 90, 0));
        }

        destroy = false;
    }

    bool IsValidPlacementSpot()
    {
        Ray ray;
        ray = new Ray(TheBuilding.transform.position + transform.up* 0.7f, -transform.up);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, Mathf.Infinity, onlyGroundAndBuildings))
        {
            GameObject hot = hit.transform.gameObject;

            if (hot.layer == 11)
            {
                return false;
            }

            switch (buildingtype.CurrentBuildingType)
            {
                case (BuildingList.Types.Mine):

                    if (hot.GetComponent<Tile>().currentTileType == Tile.Types.Iron)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                default:
                    return true;
            }
        }
        //else
        return false;
    }

    void GroundClicked ()
    {

        Transform selection = hit.transform;
        Tile hasComponentTile = selection.gameObject.GetComponent<Tile>();
        Building hasComponentBuilding = selection.gameObject.GetComponent<Building>();
        SelectedObject = selection.gameObject;
        //Do something to the tile you just clicked on.
        if (hasComponentTile != null)
        {
            if (buildingtype != BuildingList.Nothing && buildingtype != null && IsValidPlacementSpot())
            {
                //Instantiates the building the player wants to construct where mouse was clicked
                TheBuilding.GetComponent<Building>().enabled = enabled;
                TheBuilding.GetComponent<Collider>().enabled = enabled;
                TheBuilding.layer = 11;                                           
            }
            else
            {
                BuildingTypeText.text = selection.GetComponent<Tile>().currentTileType.ToString();
                Destroy(TheBuilding);
                if(!destroy && TheBuilding != null)
                Debug.LogWarning("Invalid Construction");
            }
        }
        //If you clicked a building
        else if (hasComponentBuilding != null)
        {
            //TheBuilding = SelectedObject;

            if (destroy)
            {
                ItemContainer.Destroy(hasComponentBuilding.InputContainers);
                ItemContainer.Destroy(hasComponentBuilding.OutputContainers);
                Destroy(selection.gameObject);
            }
            else
            {
                buildingScript = selection.GetComponent<Building>();

                //UI elements
                ShowSelectedObjectUI(SelectedObject);
            }
        }

        //Makes it so left shift keeps the building you last constructed
        Reset(hasComponentBuilding, hasComponentTile);


    }

    public void Reset(Building hasComponentBuilding, Tile hasComponentTile)
    {

        if (!Input.GetKey(KeyCode.LeftShift))
        {
            buildingtype = BuildingList.Nothing;
            TheBuilding = null;
            hasComponentBuilding = null;
            hasComponentTile = null;
            destroy = false;
        }
        else
        {
            ChangeBuilding(buildingtype.ID);
        }
    }

    //This function is called from line 79 and forward. 
    //It begins by finding the position of the mouse, then it places the building there and rotates the building based on how many
    //Times the button R has been pressed. After that it gives the building its proper building Type.
    private void InstantiateBuilding(BuildingList building, float height)
    {

        //Building Position
        //Vector3 buildingPlacement = new Vector3(Mathf.RoundToInt(hit.point.x), height, Mathf.RoundToInt(hit.point.z));
        //switch (building.Width)
        //{
        //    case 2:
        //        switch (rotation)
        //        {
        //            case 1:
        //                buildingPlacement.x += 0.5f;
        //                break;
        //            case 2:
        //                buildingPlacement.z += 0.5f;
        //                break;
        //            case 3:
        //                buildingPlacement.x += 0.5f;
        //                break;
        //            case 4:
        //                buildingPlacement.z += 0.5f;
        //                break;
        //        }
        //        break;
        //}

        //Create building
        TheBuilding = Instantiate(building.BuildingObject, buildingPlacement, Quaternion.Euler(0,rotation*90,0));

        //Assign building
        buildingScript = TheBuilding.GetComponent<Building>();
        buildingScript.building = buildingtype;
        buildingScript.enabled = !enabled;
        TheBuilding.GetComponent<Collider>().enabled = !enabled;
        

    }

    //This function instructs what will happen when the player clicks on a part of the ground or a building.
    //It should show basic information about the object the player clicked on, aswell as the input and output amount and resource type.
    public void ShowSelectedObjectUI(GameObject SelectedBuilding)
    {
        //Resets previous text
        ItemContainer.ToggleVisibilityAll(false);

        if (SelectedBuilding != null)
        {

            SelectedObject = SelectedBuilding;
            Building building = SelectedBuilding.GetComponent<Building>();

            if (building != null && building.isActiveAndEnabled)
            {

                //Type
                BuildingTypeText.gameObject.transform.parent.gameObject.SetActive(true);
                BuildingTypeText.text = building.building.CurrentBuildingType.ToString();

                if (building.OutputContainers.Length > 0)
                    ItemContainer.ToggleVisibility(building.OutputContainers, true);

                if (building.InputContainers.Length > 0)
                    ItemContainer.ToggleVisibility(building.InputContainers, true);

            }
            building = null;
        }
    }

    //This is called when the "destroy" button is pressed.
    bool destroy = false;
    public void Destroy()
    {
        destroy = true;
        buildingtype = BuildingList.Nothing;
    }


}