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

    private float minTerrainLength = 1000f;
    private float fullLength;
    private float lengthReached = 0;
    private int modulesCreated;
    private int modulesUntilElevator = 15;

    private TerrainModule.ModuleType lastModuleType;
    
    // Static Event that invokes when an elevator spawns
    public static event Action ElevatorSpawned;

    // For destroying generator after elevator spawns
    private bool reachedEnd = false;

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
        if (reachedEnd) { Destroy(gameObject); }
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
            GameObject terrainModule = GetNextModule(); // Get a valid random prefab
            terrainModule = InstantiateNextModule(terrainModule); // Replace the prefab with the actual intantiated GameObject
            UpdateLengthRelatedVariables(terrainModule);
        }
    }

    private void UpdateLengthRelatedVariables(GameObject terrainModule)
    {
        // Adds spawned block's length to "fullLength" & "lengthReached"
        float moduleLength = terrainModule.GetComponent<TerrainModule>().GetLength();
        fullLength += moduleLength;
        lengthReached += moduleLength;
    }

    private GameObject InstantiateNextModule(GameObject terrainModule)
    {
        // Determines the position for a new block and instantiates it
        float floorPosition = terrainModule.transform.localScale.y / 2 * -1;
        Vector3 blockPosition = new Vector3(lengthReached, floorPosition, 0);
        return Instantiate(terrainModule, blockPosition, Quaternion.identity);
    }

    private GameObject GetNextModule()
    {
        GameObject terrainModule = CreateNewRandomModule();
        modulesCreated++;
        lastModuleType = terrainModule.GetComponent<TerrainModule>().moduleType;
        return terrainModule;
    }

    private GameObject CreateNewRandomModule()
    {
        GameObject module;
        TerrainModule.ModuleType moduleType;

        if (modulesCreated < modulesUntilElevator)
        {
            int randomIndex = UnityEngine.Random.Range(0, terrainModules.Length);
            module = terrainModules[randomIndex];
            moduleType = module.GetComponent<TerrainModule>().moduleType;
        }

        else // Spawns Elevator
        {
            int randomIndex = UnityEngine.Random.Range(0, elevatorModules.Length);
            module = elevatorModules[randomIndex];
            moduleType = TerrainModule.ModuleType.Normal;
            reachedEnd = true;
        } 

        // New random TerrainModule from array
        bool willRepeatVoid = (lastModuleType == TerrainModule.ModuleType.Void && moduleType == TerrainModule.ModuleType.Void);
        bool willSpawnVoidInStartingLane = (modulesCreated < 10 && moduleType == TerrainModule.ModuleType.Void);

        if (!willRepeatVoid && !willSpawnVoidInStartingLane ) return module; else return CreateNewRandomModule();
    }

    private void OnDisable()
    {
        TerrainModule.TerrainModuleDestroyed -= DecreaseFullLength;
        //Destroy(gameObject);
    }

    private void OnDestroy()
    {
        TerrainModule.TerrainModuleDestroyed -= DecreaseFullLength;
    }
}
