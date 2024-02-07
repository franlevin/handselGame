using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static TerrainModule;

public class TerrainGeneratorV2 : MonoBehaviour
{
    [SerializeField] private GameObject startingModule;
    [SerializeField] private GameObject[] terrainModules; // The array with every possible terrain module.
    [SerializeField] private float minTerrainLength = 100f;
    private float terrainLength;
    private float lastModuleMidpoint;
    private GameObject terrain;
    
    void Awake(){
        // Subscription to static event for decreasing length of a destroyed module in full spawned length.
        TerrainModule.TerrainModuleDestroyed += DecreaseFullLength;
        terrain = GameObject.FindGameObjectWithTag("Terrain");
    }

    void Start()
    {
        StartTerrain();    
    }

    void Update()
    {
        AddTerrain();
    }

    // Initialize Terrain using the module asigned in the inspector
    private void StartTerrain(){
        GameObject instantiatedStartingModule = Instantiate(startingModule, terrain.transform);
        lastModuleMidpoint = instantiatedStartingModule.GetComponent<TerrainModule>().GetLength() / 2;
        UpdateTerrainLength(instantiatedStartingModule);
    }

    // Add a new module until the minTerrainLength is covered
    private void AddTerrain(){
        if(terrainLength < minTerrainLength){
            GameObject terrainModule = GetNextModule(); // Get a valid random prefab
            terrainModule = InstantiateNextModule(terrainModule); // Replace the prefab with the actual intantiated GameObject
            UpdateTerrainLength(terrainModule);
        }
    }

    private GameObject GetNextModule(){
        int randomIndex = UnityEngine.Random.Range(0, terrainModules.Length);
        return terrainModules[randomIndex];
    }

    private GameObject InstantiateNextModule(GameObject nextModule){

        // Instantiates a block and moves it to the end of the terrain
        nextModule = Instantiate(nextModule, terrain.transform);
        
        // Using midpoints for compatibility between different-length modules
        float nextModuleMidpoint = nextModule.GetComponent<TerrainModule>().GetLength() / 2;
        float nextBlockPosition = lastModuleMidpoint + nextModuleMidpoint;
        nextModule.transform.position = new Vector3(nextBlockPosition, terrain.transform.position.y);

        lastModuleMidpoint = nextBlockPosition + nextModuleMidpoint;
        
        return nextModule;
    }

    private void UpdateTerrainLength(GameObject terrainModule){
        // Adds spawned block's length to terrainLength
        float moduleLength = terrainModule.GetComponent<TerrainModule>().GetLength();
        terrainLength += moduleLength;
    }

    
    
    private void DecreaseFullLength (float length){
        terrainLength -= length;
    }

}
