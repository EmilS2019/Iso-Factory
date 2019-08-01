using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingReferenceList : MonoBehaviour {


    public GameObject _MineGO;
    public static GameObject MineGO;

    public GameObject _ConveyorGO;
    public static GameObject ConveyorGO;

    public GameObject _SplitterGO;
    public static GameObject SplitterGO;

    public GameObject _SmelterGO;
    public static GameObject SmelterGO;

	void Start ()
    {

        MineGO = _MineGO;
        ConveyorGO = _ConveyorGO;
        SplitterGO = _SplitterGO;
        SmelterGO = _SmelterGO;
    }

}
