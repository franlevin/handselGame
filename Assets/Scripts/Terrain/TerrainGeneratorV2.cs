using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TerrainModule;

public class TerrainGeneratorV2 : MonoBehaviour
{
    public GameObject[] terrainModules; // The array with every possible terrain module.
    public GameObject[] elevatorModules; // The array with every possible elevator module.
    public TerrainModuleData Data;

    private float minTerrainLength = 300f;
    private float fullLength;
    private float lengthReached;
    private int modulesCreated;
    private int modulesUntilElevator = 15;

    //private ModuleType lastModuleType;
    TerrainModule.ModuleType lastModuleType;
    
    // Static Event that invokes when an elevator spawns
    public static event Action ElevatorSpawned;

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
        GameObject module;
        TerrainModule.ModuleType moduleType;

        if (modulesCreated < modulesUntilElevator)
        {
            int randomIndex = UnityEngine.Random.Range(0, terrainModules.Length);
            module = terrainModules[randomIndex];
            moduleType = module.GetComponent<TerrainModule>().moduleType;

        }

        else
        {
            int randomIndex = UnityEngine.Random.Range(0, elevatorModules.Length);
            module = elevatorModules[randomIndex];
            moduleType = TerrainModule.ModuleType.Normal;
        } 

        // New random TerrainModule from array
        bool willRepeatVoid = (lastModuleType == TerrainModule.ModuleType.Void && moduleType == TerrainModule.ModuleType.Void);
        bool willSpawnVoidInStartingLane = (modulesCreated < 10 && moduleType == TerrainModule.ModuleType.Void);

        if (!willRepeatVoid && !willSpawnVoidInStartingLane ) return module; else return CreateModule();
    }

    private void OnDisable()
    {
        TerrainModule.TerrainModuleDestroyed -= DecreaseFullLength;
        Destroy(gameObject);
    }
}
