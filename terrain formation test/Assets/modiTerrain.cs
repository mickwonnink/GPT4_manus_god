using UnityEngine;
using System.Collections;

public class modiTerrain : MonoBehaviour
{
    public Terrain terrain;
    private TerrainData terrainData;


    void Awake()
    {
        terrainData = terrain.terrainData;
    }

    void Start()
    {
        EditTerrain();
    }

    private void EditTerrain()
    {
        int heightmapWidth = terrainData.heightmapWidth;
        int heightmapHeight = terrainData.heightmapHeight;

        float[,] heights = terrainData.GetHeights(0,0,heightmapWidth, heightmapHeight);

        for (int i = 0; i < heightmapHeight; i++)
        {
            for (int j = 0; j < heightmapWidth; j++)
            {
                heights[i, j] = 250;
            }
        }

        terrainData.SetHeights(0, 0, heights);
    }
	
}
