using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TerrainModule;

public class TerrainGeneratorV2 : MonoBehaviour
{
    public GameObject[] terrainModules; // The array with every possible module.
    public TerrainModuleData Data;

    private float minTerrainLength = 300f;
    private float fullLength;
    private float lengthReached;
    private int modulesCreated; 

    //private ModuleType lastModuleType;
    TerrainModule.ModuleType lastModuleType;

    // Start is called before the first frame update
    void Start()
    {
        
        // Subscription to static event for decreasing length of a destroyed module in full spawned length.
        TerrainModule.TerrainModuleDestroyed += DecreaseFullLength;
        lastModuleType = TerrainModule.ModuleType.Void;
    }

    // Update is called once per frame
    void Update()
    {
        AddTerrain();
        Debug.Log("modulesCreated is: " + modulesCreated);
    }

    private void StartTerrain()
    {
        
    }

    private void DecreaseFullLength (float length)
    {
        fullLength -= length;
        //Debug.Log("Decrece la length");
    }

    private void AddTerrain()
    {
        if (fullLength < minTerrainLength)
        {

            GameObject terrainModule = CreateModule();
            modulesCreated++;
            lastModuleType = terrainModule.GetComponent<TerrainModule>().moduleType;

            // Determines the position for a new block and instantiates it
            float floorPosition = ((terrainModule.transform.localScale.y) / 2) * -1;
            Vector3 blockPosition = new Vector3(lengthReached, floorPosition, 0);
            Instantiate(terrainModule, blockPosition, Quaternion.identity);

            // Adds spawned block's length to "fullLength" & "lengthReached"
            fullLength += terrainModule.GetComponent<TerrainModule>().data.length;
            lengthReached += terrainModule.GetComponent<TerrainModule>().data.length;
        }
    }

    private GameObject CreateModule()
    {
        // New random TerrainModule from array
        GameObject module;
        int randomIndex = Random.Range(0, terrainModules.Length);
        module = terrainModules[randomIndex];
        TerrainModule.ModuleType moduleType = module.GetComponent<TerrainModule>().moduleType;
        bool willRepeatVoid = (lastModuleType == TerrainModule.ModuleType.Void && moduleType == TerrainModule.ModuleType.Void);
        bool willSpawnVoidInStartingLane = (modulesCreated < 10 && moduleType == TerrainModule.ModuleType.Void);

        if (!willRepeatVoid && !willSpawnVoidInStartingLane ) return module; else return CreateModule();
    }
}
