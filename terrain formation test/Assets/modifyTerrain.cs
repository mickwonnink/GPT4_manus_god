using UnityEngine;
using System.Collections;

public class modifyTerrain : MonoBehaviour
{
    public Terrain terrain;
    public Camera camera;

    public float Strength = 0.01f;

    private int heightMapWidth;
    private int heightMapHeight;
    private float[,] heights;
    private TerrainData terrainData;

    void Start()
    {
        terrainData = terrain.terrainData;
        heightMapWidth = terrainData.heightmapWidth;
        heightMapHeight = terrainData.heightmapHeight;
        heights = terrainData.GetHeights(0, 0, heightMapWidth, heightMapHeight);
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        //Raise terrain
        if(Input.GetMouseButton(0))
        {
            if (Physics.Raycast(ray, out hit))
            {
                RaiseTerrain(hit.point);
            }
        }


        //Lower terrain
        if (Input.GetMouseButton(1))
        {
            if (Physics.Raycast(ray, out hit))
            {
                LowerTerrain(hit.point);
            }
        }
    }

    private void RaiseTerrain(Vector3 point)
    {
        int mouseX = (int)((point.x / terrainData.size.x) * heightMapWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * heightMapHeight);
        float[,] modifiedHeights = new float[1, 1];

        float y = heights[mouseX, mouseZ];

        y += Strength * Time.deltaTime;

        if (y > terrainData.size.y)
        {
            y = terrainData.size.z;
        }

        modifiedHeights[0, 0] = y;
        heights[mouseX, mouseZ] = y;
        terrainData.SetHeights(mouseX, mouseZ, modifiedHeights);
    }

    private void LowerTerrain(Vector3 point)
    {
        int mouseX = (int)((point.x / terrainData.size.x) * heightMapWidth);
        int mouseZ = (int)((point.z / terrainData.size.z) * heightMapHeight);
        float[,] modifiedHeights = new float[1, 1];

        float y = heights[mouseX, mouseZ];

        y -= Strength * Time.deltaTime;

        if (y < 0 )
        {
            y = 0;
        }

        modifiedHeights[0, 0] = y;
        heights[mouseX, mouseZ] = y;
        for (int i = -5; i < 5; i++)
        {
            terrainData.SetHeights(mouseX + i, mouseZ - i, modifiedHeights);
        }
        
    }
     
	
}
