using UnityEngine;
using System.Collections;

public class MapTheTerrain : MonoBehaviour
{

    public Terrain terrain;
    public float maxDist = 1.85f; // distance to floor of terrain

    void Start()
    {
        if (terrain == null) terrain = Terrain.activeTerrain;
        if (terrain == null) return;

        int size = (int)terrain.terrainData.heightmapResolution;

        float[,] heights = new float[size, size];

        float delta = 1.0f / size;
        Vector2 testPos = new Vector2(-0.5f, -0.5f);

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                RaycastHit hit;
                Vector3 pos = transform.TransformPoint(testPos);
                if (Physics.Raycast(pos + 0.001f * Vector3.down, Vector3.down, out hit))
                {
                    float dist = Mathf.Clamp(hit.distance, 0.0f, maxDist);
                    heights[i, j] = (maxDist - dist) / maxDist;
                }
                else
                {
                    heights[i, j] = 0.0f;
                }
                testPos.x += delta;
            }
            testPos.x = -0.5f;
            testPos.y += delta;
            int row = i + 1;
        }
        terrain.terrainData.SetHeights(0, 0, heights);
    }
}