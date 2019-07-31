using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour{


    public enum TileTypes
    {
        Nothing,
        Ground,
        Iron
    }

    public TileTypes currentTileType;
    public int x, z;
    public Material[] materials;
    MeshRenderer MRmaterials;

    public void Start()
    {
        MRmaterials = GetComponent<MeshRenderer>();
        ChangeTileType();
    }

    public void ChangeTileType()
    {
        if (currentTileType == TileTypes.Ground)
        {
            MRmaterials.material = materials[0];
        }
        else if (currentTileType == TileTypes.Iron)
        {
            MRmaterials.material = materials[1];
        }
        else
        {
            MRmaterials.material = materials[0];
        }
    }
}