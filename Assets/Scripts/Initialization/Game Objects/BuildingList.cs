using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingList
{
    /*
    -----------------------------------------------------------------------------------
    |This file is for storing the different kinds of Buildings that exist in this game|
    -----------------------------------------------------------------------------------
    */
    public enum BuildingTypes
    {
        Nothing,
        Mine,
        Conveyor,
        Splitter,
        Storage
    }

    public BuildingTypes CurrentBuildingType { get; protected set; }
    public GameObject BuildingObject { get; protected set; }

    //Number of blocks wide and long
    public int Width { get; protected set; }
    public int Lenght { get; protected set; }

    public int Inputs { get; protected set; }
    public int Outputs { get; protected set; }

    public int ID { get; protected set; }


    /// <summary>
    /// This is for instantiating a new building class so that it becomes easy to add new buildings.
    /// </summary>
    /// <param name="id">The Buildings ID</param>
    /// <param name="_CurrentBuildingType"></param>
    private BuildingList(int id, BuildingTypes _CurrentBuildingType)
    {
        ID = id;
        CurrentBuildingType = _CurrentBuildingType;
        //This is used for the nothing building
    }

    /// <summary>
    /// This is for instantiating a new building class so that it becomes easy to add new buildings.
    /// </summary>
    /// <param name="CurrentBuildingType"></param>
    /// <param name="BuildingObject"></param>
    /// <param name="Width"></param>
    private BuildingList(int id, BuildingTypes _CurrentBuildingType, GameObject _BuildingObject)
    {
        ID = id;
        CurrentBuildingType = _CurrentBuildingType;
        BuildingObject = _BuildingObject;
        Width = 1;
        Lenght = 1;
        Inputs = 1;
        Outputs = 1;
    }

    /// <summary>
    /// This is for instantiating a new building class so that it becomes easy to add new buildings.
    /// </summary>
    /// <param name="CurrentBuildingType"></param>
    /// <param name="BuildingObject"></param>
    /// <param name="Width"></param>
    private BuildingList(int id, BuildingTypes _CurrentBuildingType, GameObject _BuildingObject, int _Inputs, int _Ouputs)
    {
        ID = id;
        CurrentBuildingType = _CurrentBuildingType;
        BuildingObject = _BuildingObject;
        Width = 1;
        Lenght = 1;
        Inputs = _Inputs;
        Outputs = _Ouputs;
    }

    /// <summary>
    /// This is for instantiating a new building class so that it becomes easy to add new buildings.
    /// </summary>
    /// <param name="CurrentBuildingType"></param>
    /// <param name="BuildingObject"></param>
    /// <param name="Width"></param>
    private BuildingList(int id, BuildingTypes _CurrentBuildingType, GameObject _BuildingObject, int _Width)
    {
        ID = id;
        CurrentBuildingType = _CurrentBuildingType;
        BuildingObject = _BuildingObject;
        Width = _Width;
        Lenght = 1;
        Inputs = 1;
        Outputs = 1;
    }

    /// <summary>
    /// This is for instantiating a new building class so that it becomes easy to add new buildings.
    /// </summary>
    /// <param name="CurrentBuildingType"></param>
    /// <param name="BuildingObject"></param>
    /// <param name="Width"></param>
    private BuildingList(int id, BuildingTypes _CurrentBuildingType, GameObject _BuildingObject, int _Width, int _Inputs, int _Ouputs)
    {
        ID = id;
        CurrentBuildingType = _CurrentBuildingType;
        BuildingObject = _BuildingObject;
        Width = _Width;
        Lenght = 1;
        Inputs = _Inputs;
        Outputs = _Ouputs;
    }



    //LIST OF BUILDINGS
    public static BuildingList Nothing = new BuildingList(0, BuildingTypes.Nothing);
    public static BuildingList Mine = new BuildingList(1, BuildingTypes.Mine, BuildingReferenceList.MineGO, 0, 2);
    public static BuildingList Conveyor = new BuildingList(2, BuildingTypes.Conveyor, BuildingReferenceList.ConveyorGO, 1, 0, 0);
    public static BuildingList Splitter = new BuildingList(3, BuildingTypes.Splitter, BuildingReferenceList.SplitterGO, 2, 2, 2);

    public static BuildingList[] buildinglist = new BuildingList[4] { Nothing, Mine, Conveyor, Splitter };
}
