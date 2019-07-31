using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class World : MonoBehaviour {

    public int WorldSizeX { get; protected set; }
    public int WorldSizeZ { get; protected set; }
    public GameObject Cube;
    public int Seed;
    
    void Start()
    {
        WorldSizeX = 30;
        WorldSizeZ = 30;

        for (int x = 0; x < WorldSizeX; x++)
        {
            for (int z = 0; z < WorldSizeZ; z++)
            {
                //Instantiates a cube on the x and y coordinates specified by the for loops, and continues untill world size has been met.
                GameObject TheCube = Instantiate(Cube, new Vector3(x,0,z), Quaternion.identity, GameObject.Find("Tiles").transform);
                Tile tile = TheCube.GetComponent<Tile>();
                tile.x = x;
                tile.z = z;
                TheCube.name = "Cube " + tile.x.ToString() + " - " + tile.z.ToString();

                //Map Generation

                int ResourceType = Random.Range(0, 1001);

                if (ResourceType > 994)
                {
                    //0.1% chance that a tile will be Iron
                    tile.currentTileType = Tile.TileTypes.Iron;
                }
                else
                {
                    tile.currentTileType = Tile.TileTypes.Ground;
                }


            }
        }
    }
}
