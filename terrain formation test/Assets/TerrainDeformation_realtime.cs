using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TerrainDeformation_realtime : MonoBehaviour
{
    public Terrain myTerrain;
    int xResolution;
    int zResolution;
    float[,] heights;

    public Slider slider;
    public Slider sliderWidth;

    void Start()
    {
        xResolution = myTerrain.terrainData.heightmapWidth;
        zResolution = myTerrain.terrainData.heightmapHeight;
        heights = myTerrain.terrainData.GetHeights(0, 0, xResolution, zResolution);
    }

    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            if (Input.GetMouseButton(0))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    raiseTerrain(hit.point);
                }
            }
            if (Input.GetMouseButton(1))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    lowerTerrain(hit.point);
                }
            }
        }
    }

    private void raiseTerrain(Vector3 point)
    {
        changeHeightTerrain(true, point);
    }

    private void lowerTerrain(Vector3 point)
    {
        changeHeightTerrain(false, point);
    }


    private void changeHeightTerrain(bool higher, Vector3 point)
    {
        int width = (int)sliderWidth.value;

        int terX = (int)((point.x / myTerrain.terrainData.size.x) * xResolution);
        int terZ = (int)((point.z / myTerrain.terrainData.size.z) * zResolution);
        float y = heights[terX, terZ];

        if (higher){
            y += slider.value;
        }else{
            y -= slider.value;
        }

        float[,] height = new float[1, 1];
        height[0, 0] = y;
        heights[terX, terZ] = y;

        for (int x = terX - width; x < terX + width; x++)
        {
            if (x >= 0){
                heights[x, terZ] = y;
                myTerrain.terrainData.SetHeightsDelayLOD(x, terZ, height);

                for (int z = terZ - width; z < terZ + width; z++)
                {
                    if(z >= 0)
                    {
                        heights[x, z] = y;
                        myTerrain.terrainData.SetHeightsDelayLOD(x, z, height);
                    }
                }
            }
        }
    }
}