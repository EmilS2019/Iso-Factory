using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Conveyor : MonoBehaviour {

    //Is used by resources to know which direction to go in.
    public Vector3 direction;
    public Building building;

    //Determines the buildings rotation, which is used by items that travel.
    void Start()
    {
        print(transform.rotation.eulerAngles.y.ToString());
        //Because the rotation sometimes is 90.0001 is needs to be rounded.
        switch (Mathf.RoundToInt(transform.rotation.eulerAngles.y).ToString())
        {
            case "0":
                direction = new Vector3(1, 0, 0);
            break;
            case "90":
                direction = new Vector3(0, 0, -1);
            break;
            case "180":
                direction = new Vector3(-1, 0, 0);
            break;
            case "270":
                direction = new Vector3(0, 0, 1);
            break;
            
        }
    }
}
