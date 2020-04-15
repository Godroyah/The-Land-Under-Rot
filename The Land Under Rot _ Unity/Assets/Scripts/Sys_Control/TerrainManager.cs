using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainManager : MonoBehaviour
{
    private Terrain[] activeTerrains;
    private TerrainData terrainData;
    private int alphamapWidth;
    private int alphamapHeight;

    private float[,,] alphamapData;
    public int NumTextures { get; private set; }

    void Start()
    {
        activeTerrains = Terrain.activeTerrains;

        Vector3 playerPos = GameController.Instance.playerController.transform.position;

        foreach (Terrain terrain in activeTerrains)
        {

        }

        GetTerrainProps();
    }

    private void GetTerrainProps()
    {
        //terrainData = Terrain.activeTerrain.terrainData;
        alphamapWidth = terrainData.alphamapWidth;
        alphamapHeight = terrainData.alphamapHeight;

        alphamapData = terrainData.GetAlphamaps(0, 0, alphamapWidth, alphamapHeight);
        NumTextures = alphamapData.Length / (alphamapWidth * alphamapHeight);

        //terrainLayers = mTerrainData.terrainLayers;
        //mNumTextures = terrainLayers.Length / (alphamapWidth * alphamapHeight);
    }

    private Vector3 ConvertToSplatMapCoordinate(Vector3 playerPos)
    {
        Vector3 vecRet = new Vector3();
        Terrain ter = Terrain.activeTerrain;
        Vector3 terPosition = ter.transform.position;
        vecRet.x = ((playerPos.x - terPosition.x) / ter.terrainData.size.x) * ter.terrainData.alphamapWidth;
        vecRet.z = ((playerPos.z - terPosition.z) / ter.terrainData.size.z) * ter.terrainData.alphamapHeight;
        return vecRet;
    }


    private int GetActiveTerrainTextureIdx(Vector3 pos)
    {
        Vector3 TerrainCord = ConvertToSplatMapCoordinate(pos);
        int ret = 0;
        float comp = 0f;
        for (int i = 0; i < NumTextures; i++)
        {
            if (comp < alphamapData[(int)TerrainCord.z, (int)TerrainCord.x, i])
                ret = i;
        }
        return ret;
    }

    public void StartFalseUpdate()
    {
        StartCoroutine(FalseUpdate());
    }

    IEnumerator FalseUpdate()
    {
        while (true)
        {
            Vector3 playerPos = GameController.Instance.playerController.transform.position;
            yield return new WaitForFixedUpdate();
        }
    }


    public int GetTerrainAtPosition(Vector3 pos)
    {
        int terrainIdx = GetActiveTerrainTextureIdx(pos);
        return terrainIdx;
    }

    public TerrainLayer GetTerrainLayerAtPosition(Vector3 pos)
    {
        int terrainIdx = GetActiveTerrainTextureIdx(pos);

        return terrainData.terrainLayers[terrainIdx];
    }

    public bool CompareTerrainLayerDiffuse(Vector3 pos, Texture2D texture)
    {
        TerrainLayer terrainLayer = GetTerrainLayerAtPosition(pos);
        Texture2D terrainDiffuse = terrainLayer.diffuseTexture;

        if (terrainDiffuse == texture)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
